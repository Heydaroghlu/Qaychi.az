using App.Application.DTOs.ServiceDTOs;
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
	public class ServicesController : ControllerBase
	{
		readonly IUnitOfWork _unitOfWork;
		readonly IMapper _mapper;
        public ServicesController(IUnitOfWork unitOfWork,IMapper mapper)
        {
			_unitOfWork = unitOfWork;
			_mapper = mapper;
        }
		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAll(string? search, int CategoryId=0)
		{
			var data = await _unitOfWork.RepositoryService.GetAllAsync(x => x.IsDeleted == false);
			if (search != null && string.IsNullOrEmpty(search) && string.IsNullOrWhiteSpace(search))
			{
				data = data.Where(x => x.Name.ToLower().Contains(search.ToLower()));

			}
			if (CategoryId > 0)
			{
				data = data.Where(x => x.CategoryId == CategoryId);
			}
			List<ServicePostDTO> returnData = _mapper.Map<List<ServicePostDTO>>(data.ToList());
			return Ok(returnData);
		}
		[HttpPost("Create")]
		public async Task<IActionResult> Create(ServicePostDTO servicePost)
		{
			Category category = await _unitOfWork.RepositoryCategory.GetAsync(x => x.Id == servicePost.CategoryId, false);
			if (category == null)
			{
				return BadRequest("Category is not exist");
			}
			Service service=_mapper.Map<Service>(servicePost);
			await _unitOfWork.RepositoryService.InsertAsync(service);
			await _unitOfWork.CommitAsync();
			return Ok(service);
		}
    }
}
