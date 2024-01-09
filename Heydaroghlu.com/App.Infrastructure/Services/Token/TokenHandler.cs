using App.Application.Abstractions.Token;
using App.Domain.Entitites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Services.Token
{
	public class TokenHandler : ITokenHandler
	{
		readonly IConfiguration _configuration;
		readonly UserManager<AppUser> _userManager;
		public TokenHandler(IConfiguration configuration, UserManager<AppUser> userManager)
		{
			_configuration = configuration;
			_userManager = userManager;
		}
		public string CreateAccessToken(AppUser user, int minute)
		{
			string token = "";
			SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
			SigningCredentials signingCredentials=new (securityKey,SecurityAlgorithms.HmacSha256);
			List<Claim> claims = CreateClaims(user);
			JwtSecurityToken securityToken = new(
				claims: claims,
				audience: _configuration["Token:Audience"],
				issuer: _configuration["Token:Issuer"],
				expires: DateTime.UtcNow.AddHours(24).AddMinutes(minute),
				signingCredentials: signingCredentials
				);
			JwtSecurityTokenHandler tokenHandler = new();
			token = tokenHandler.WriteToken(securityToken);
			return token;
		}

		public List<Claim> CreateClaims(AppUser user)
		{
			List<Claim> claims = new List<Claim>()
			{
				new Claim("Id",user.Id),
				new Claim(ClaimTypes.NameIdentifier, user.UserName),
				new Claim(ClaimTypes.Name, user.Name+user.Surname),
				new Claim("UserType",user.UserType.ToString()),

			};
			var roles = _userManager.GetRolesAsync(user).Result;
			var CRoles = roles.Select(x => new Claim(ClaimTypes.Role, x));
			claims.AddRange(CRoles);
			return claims;
		}
	}
}
