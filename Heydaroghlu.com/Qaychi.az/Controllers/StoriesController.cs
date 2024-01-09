using App.Application.DTOs.StoreDTOs;
using App.Application.UnitOfWorks;
using App.Domain.Entitites;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Qaychi.az.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StoriesController : ControllerBase
	{
		readonly IUnitOfWork _unitOfWork;
		readonly IMapper _mapper;
        public StoriesController(IUnitOfWork unitOfWork,IMapper mapper)
        {
			_unitOfWork = unitOfWork;
			_mapper = mapper;
        }
		[HttpGet("Stories")]
		public async Task<IActionResult> GetAll(string? search,int CategoryId=0)
		{
			var data = await _unitOfWork.RepositoryStore.GetAllAsync(x => x.IsDeleted == false,false,"Images");
			if(search!=null && string.IsNullOrEmpty(search) && string.IsNullOrWhiteSpace(search))
			{
				data=data.Where(x=>x.Name.ToLower().Contains(search.ToLower()) || x.Title.ToLower().Contains(search.ToLower()));
			
			}
			if(CategoryId>0)
			{
				data = data.Where(x => x.CategoryId == CategoryId);
			}
			List<StoreReturnDTO> returnData = _mapper.Map<List<StoreReturnDTO>>(data.ToList());
			return Ok(returnData);	
		}
		[HttpPost("Create")]
		public async Task<IActionResult> Create(StoreCreateDTO storeCreate)
		{
			Category category = await _unitOfWork.RepositoryCategory.GetAsync(x => x.Id == storeCreate.CategoryId,false);
			if(category==null)
			{
				return BadRequest("Category is not exist");
			}
			Store store=_mapper.Map<Store>(storeCreate);
			await _unitOfWork.RepositoryStore.InsertAsync(store);
			await _unitOfWork.CommitAsync();
			return Ok(storeCreate);
		}
		[HttpPost("Edit")]
		public async Task<IActionResult> Edit(StoreEditDTO storeEdit)
		{
			Category category = await _unitOfWork.RepositoryCategory.GetAsync(x => x.Id == storeEdit.CategoryId, false);
			if (category == null)
			{
				return BadRequest("Category is not exist");
			}
			var exist = await _unitOfWork.RepositoryStore.GetAsync(x => x.Id == storeEdit.Id, true,"Images");
		   if(exist==null)
			{
				return NotFound();
			}
		    exist.Name= storeEdit.Name;
	        exist.Title= storeEdit.Title;
			exist.Description=storeEdit.Description;
			exist.Phone= storeEdit.Phone;
			exist.Address= storeEdit.Address;
			exist.atWeek = storeEdit.atWeek;
			exist.Weekend=storeEdit.Weekend;
			exist.Instagram= storeEdit.Instagram;
			exist.WebSite=storeEdit.WebSite;
			exist.PosterUrl= storeEdit.PosterUrl;
			exist.Voen= storeEdit.Voen;
			exist.CategoryId= storeEdit.CategoryId;
			if(storeEdit.Images!=null && storeEdit.Images.Count>0)
			{
				foreach (var item in exist.Images)
				{
					await _unitOfWork.RepositoryStoreImage.Remove(x=>x.Id==item.Id);
				}
				List<StoreImage> storeImages = _mapper.Map<List<StoreImage>>(storeEdit.Images);
				storeImages.ForEach(x=>x.StoreId=exist.Id);
				await _unitOfWork.RepositoryStoreImage.InsertRangeAsync(storeImages);
			}
			await _unitOfWork.CommitAsync();
			return Ok(storeEdit);
		}
		[HttpPost("Delete")]
		public async Task<IActionResult> Delete(int id)
		{
			var exist = await _unitOfWork.RepositoryStore.GetAsync(x => x.Id ==id, true, "Images");
			if (exist == null)
			{
				return NotFound();
			}
			exist.IsDeleted= true;	
			await _unitOfWork.CommitAsync();
			return Ok();
		}

	}
}
