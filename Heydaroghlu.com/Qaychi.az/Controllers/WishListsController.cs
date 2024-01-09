using App.Application.DTOs.Wishlist;
using App.Application.UnitOfWorks;
using App.Domain.Entitites;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Qaychi.az.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WishListsController : ControllerBase
	{
		readonly IUnitOfWork _unitOfWork;
		readonly IMapper _mapper;
        public WishListsController(IUnitOfWork unitOfWork,IMapper mapper)
        {
			_unitOfWork = unitOfWork;
			_mapper = mapper;
        }
		[HttpGet("UserOfWishlist")]
		public async Task<IActionResult> UserOfWishlist(string Id)
		{
			var data = await _unitOfWork.RepositoryWishlist.GetAllAsync(x => x.UserId == Id);
			List<WishlistPostDTO> wishlistPosts=_mapper.Map<List<WishlistPostDTO>>(data.ToList());
			return Ok(wishlistPosts);
		}
		[HttpPost("Add")]
		public async Task<IActionResult> Add(string UserId,int StoreId)
		{
			AppUser user=await _unitOfWork.RepositoryAppUser.GetAsync(x=>x.Id== UserId);	
			if (user==null)
			{
				return BadRequest("User is not exist");
			}
			Store store=await _unitOfWork.RepositoryStore.GetAsync(x=>x.Id==StoreId);
			if(store==null)
			{
				return BadRequest("Store is not exist");
			}
			Wishlist wishlist = new Wishlist()
			{
				Id = StoreId,
				StoreId = StoreId
			};
			await _unitOfWork.RepositoryWishlist.InsertAsync(wishlist);
			await _unitOfWork.CommitAsync();
			return Ok(wishlist);
		}
		[HttpPost("Delete")]
		public async Task<IActionResult> Delete(string UserId,int StoreId)
		{
			AppUser user = await _unitOfWork.RepositoryAppUser.GetAsync(x => x.Id == UserId);
			if (user == null)
			{
				return BadRequest("User is not exist");
			}
			Store store = await _unitOfWork.RepositoryStore.GetAsync(x => x.Id == StoreId);
			if (store == null)
			{
				return BadRequest("Store is not exist");
			}
			await _unitOfWork.RepositoryWishlist.Remove(x => x.UserId == UserId && x.StoreId == StoreId);
			await _unitOfWork.CommitAsync();
			return Ok();
		}

	}
}
