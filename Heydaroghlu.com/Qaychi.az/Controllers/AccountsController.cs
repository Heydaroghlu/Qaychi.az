using App.Application.Abstractions.Token;
using App.Application.DTOs.AccountDTOs;
using App.Application.Enums.forUser;
using App.Application.UnitOfWorks;
using App.Domain.Entitites;
using App.Infrastructure.Services.Email;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Data;

namespace Qaychi.az.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ITokenHandler _tokenHandler;
		private readonly IMapper _mapper;
		readonly IEmailService _emailService;
        public AccountsController(IMapper mapper,IEmailService emailService,IUnitOfWork unitOfWork,ITokenHandler tokenHandler,UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
			_tokenHandler = tokenHandler;
			_emailService = emailService;
			_mapper=mapper;
        }
		[HttpGet("UserTypes")]
		public async Task<IActionResult> UserTypes()
		{
			var data = Enum.GetNames(typeof(UserType));
			return Ok(data);
		}
        [HttpGet("Users")]
        public async Task<IActionResult> Users()
        {
			var data = await _unitOfWork.RepositoryAppUser.GetAllAsync(x => x.UserType!="Admin");
			return Ok(data);
        }
		[HttpGet("Delete")]
		public async Task<IActionResult> Delete()
		{
			try
			{
				await _unitOfWork.RepositoryAppUser.Remove(x => x.OTP != 1);
				await _unitOfWork.CommitAsync();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			return Ok();
		}
		[HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
			AppUser user = await CheckLogin(login);

			if (user == null)
			{
				return StatusCode(401, "Ad və ya şifrə yanlışdır !");
			}
			if (user.LockoutEnabled)
			{
				return StatusCode(403, "User blocklanib");
			}
			if (user.EmailConfirmed == false)
			{
				Random random = new Random();
				int Code = random.Next(100000, 999999);
				string emailBody = await EmailBody(Code);
				user.OTP = Code;
				await _unitOfWork.CommitAsync();
				_emailService.Send(user.Email, "Qayçı", emailBody);
				return Ok("OTP sent");
			}
			var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
			if (!result.Succeeded)
			{
				return StatusCode(401, "Ad və ya şifrə yanlışdır !");
			}
			//var token = _tokenHandler.CreateAccessToken(user, 20);
			return Ok(user.Id);

		}
		[HttpPost("Register")]
		public async Task<IActionResult> Register(RegisterDTO register)
		{
			AppUser exist = await _userManager.FindByNameAsync(register.Email);
			Random random = new Random();
			int Code = random.Next(100000, 999999);
			string emailBody = await EmailBody(Code);
			if (exist != null && exist.EmailConfirmed == true)
			{ return BadRequest("This user is exist"); }
			if(exist !=null && exist.EmailConfirmed==false)
			{
				exist.OTP = Code;
				await _unitOfWork.CommitAsync();
				_emailService.Send(exist.Email, "Qayçı", emailBody);
				return Ok("OTP sent");
			}
			if (register.Password != register.RepeatPassword)
			{
				return BadRequest("PassowordError");
			}
			AppUser user = new()
			{
				Name = register.Name,
				Surname = register.Surname,
				FatherName=register.FatherName,
				Gender=register.Gender,
				Email = register.Email,
				PhoneNumber = register.Phone,
				UserName = register.Email,
				ProfileImg = register.ProfileImage,
				Address = register.Address,
				CreatedTime = DateTime.UtcNow.AddHours(4),
				ModifiedTime = DateTime.UtcNow.AddHours(4),
				UserType=register.UserType,
				Latitude=register.Latitude,
				Longitude=register.Longitude,
				OTP=Code
			};
			var result = await _userManager.CreateAsync(user, register.Password);
			var addToRoleResult = await _userManager.AddToRoleAsync(user, "Client");
			try
			{
				await _unitOfWork.CommitAsync();
			}
			catch (Exception ex)
			{
				return Ok(ex.InnerException);
			}
			if (!addToRoleResult.Succeeded)
			{
				return BadRequest("User can not create");
			}
			_emailService.Send(user.Email, "Qayçı",emailBody);
			return Ok(user);
		}
		[HttpGet("MyAccount")]
		public async Task<IActionResult> MyAccount(string Id)
		{
			AppUser user = await _unitOfWork.RepositoryAppUser.GetAsync(x => x.Id == Id, false);
			MyAccountDTO myAccount=_mapper.Map<MyAccountDTO>(user);
			return Ok(myAccount);
		}
		[HttpPost("EditProfile")]
		public async Task<IActionResult> EditProfile(string AppUserId,EditProfileDTO edit)
		{
			AppUser user = await _unitOfWork.RepositoryAppUser.GetAsync(x => x.Id == AppUserId,true);
			if(user == null)
			{
				return NotFound();
			}
			user.Name=edit.Name;
			user.Surname=edit.Surname;
			user.FatherName = edit.FatherName;
			user.Gender = edit.Gender;
			user.Address = edit.Address;
			user.PhoneNumber = edit.Phone;
			user.ProfileImg = edit.ProfileImage;
			user.UserType = edit.UserType;
			user.Longitude = edit.Longitude;
			user.Latitude = edit.Latitude;
			if(edit.Password != null && edit.Password==edit.RepeatPassword)
			{
				await _userManager.ChangePasswordAsync(user,edit.CurrentPassword,edit.Password);
			}
			await _unitOfWork.CommitAsync();
			return Ok();
		}
		[HttpPost("Forgot")]
		public async Task<IActionResult> Forgot(string Email)
		{
			AppUser user=await _unitOfWork.RepositoryAppUser.GetAsync(x=>x.Email==Email,true);
			if(user == null)
			{
				return BadRequest("User is not exist");
			}
			Random random = new Random();
			int Code = random.Next(100000, 999999);
			string emailBody = await EmailBody(Code);
			user.OTP = Code;
			await _unitOfWork.CommitAsync();
			_emailService.Send(user.Email, "Qayçı-Texniki Dəstək *7710", emailBody);
			return Ok("OTP sent");

		}
		[HttpPost("ConfirmForgot")]
		public async Task<IActionResult> ConfirmForgot(string Email,int code)
		{
			AppUser user = await _unitOfWork.RepositoryAppUser.GetAsync(x => x.Email == Email,false);
			if (user == null)
			{
				return NotFound();
			}
			if (user.OTP == code)
			{
				_emailService.Send(user.Email, "Qayçı-Texniki Dəstək *7710", "Hesabınıza şifrə dəyişdirilmə ilə bağlı müraciət olundu. Əgər bu siz deyilsinizsə, bizimlə əlaqə saxlayın.");
				await _unitOfWork.CommitAsync();
				return Ok(user.Id);
			}
			return BadRequest("OTP is wrong");
		}
		[HttpPost("ResetPassword")]
		public async Task<IActionResult> ResetPassword(ResetPassDTO resetPass)
		{
			AppUser user=await _unitOfWork.RepositoryAppUser.GetAsync(x=>x.Id==resetPass.Id);
			if(user==null)
			{
				return BadRequest("User is not exist");
			}
			try
			{
				var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
				await _userManager.ResetPasswordAsync(user, resetToken,resetPass.Password);
				await _unitOfWork.CommitAsync();
			}
			catch
			{
				return BadRequest("It is impossible");
			}
			return Ok("Changed");
		}
		[HttpPost("ConfirmAccount")]
		public async Task<IActionResult> ConfirmAccount(string Email,int code)
		{
			AppUser user=await _unitOfWork.RepositoryAppUser.GetAsync(x=>x.Email==Email,true);
			if(user==null)
			{
				return NotFound();
			}
			if(user.OTP==code)
			{
				user.EmailConfirmed = true;
				_emailService.Send(user.Email,"Qayçı","Hesabınız təsdiqlənmişdir.");
				await _unitOfWork.CommitAsync();
				return Ok();
			}

			return BadRequest("OTP is wrong");
			
		}
		[HttpGet("CreateRoles")]
		public async Task<IActionResult> CreateRoles(string Name)
		{
			var role1 = new IdentityRole($"{Name}");
			await _roleManager.CreateAsync(role1);
			return Ok();
		}
		private async Task<AppUser> CheckLogin(LoginDTO login)
        {
			var userManager = HttpContext.RequestServices.GetService<UserManager<AppUser>>();
            AppUser user =await userManager.FindByEmailAsync(login.Email);
			if (user != null)
			{
				var result = await userManager.CheckPasswordAsync(user, login.Password);
				if (result)
				{
					await userManager.ResetAccessFailedCountAsync(user);
					return user;
				}
				else
				{
					await userManager.AccessFailedAsync(user);

					if (await userManager.GetAccessFailedCountAsync(user) >= 3)
					{
						await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
					}
				}
			}
            return null;
		}
		private async Task<string> EmailBody(int Code)
		{
			string emailBody = $@"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <meta http-equiv='X-UA-Compatible' content='IE=edge'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <style>
                    .card {{
                        background: #ff8c00;
                        width: 180px;
                        height: 150px;
                        padding: 20px;
                        position: relative;
                        border-radius: 6px;
                        color: white;
                        text-align: center;
                        font-size: 1.5em;
                        box-shadow: rgba(0, 0, 0, 0.17) 0px -23px 25px 0px inset, rgba(0, 0, 0, 0.15) 0px -36px 30px 0px inset, rgba(0, 0, 0, 0.1) 0px -79px 40px 0px inset, rgba(0, 0, 0, 0.06) 0px 2px 1px, rgba(0, 0, 0, 0.09) 0px 4px 2px, rgba(0, 0, 0, 0.09) 0px 8px 4px, rgba(0, 0, 0, 0.09) 0px 16px 8px, rgba(0, 0, 0, 0.09) 0px 32px 16px;
                    }}

                    .scissors {{
                        font-size: 1em;
                        margin-top: 2px;
                    }}
                </style>
            </head>
            <body>
                <div class='card'>
                    <div class='scissors'>✂️</div>
                    Təsdiq kodu:
                    <br>
                    <strong>{Code}</strong>
                </div>
            </body>
            </html>
        ";
			return emailBody;
		}

	}
}
