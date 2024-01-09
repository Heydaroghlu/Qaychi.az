using App.Application.DTOs.CategoryDTOs;
using App.Application.UnitOfWorks;
using App.Domain.Entitites;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Qaychi.az.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		readonly IUnitOfWork _unitOfWork;
		readonly IMapper _mapper;
        public CategoriesController(IUnitOfWork unitOfWork,IMapper mapper)
        {
			_unitOfWork = unitOfWork;   
			_mapper = mapper;
        }
        [HttpGet("GetAll")]
		public async Task<IActionResult> GetAll()
		{
			var data =await _unitOfWork.RepositoryCategory.GetAllAsync(x => x.IsDeleted == false, false);
			List<CategoryReturnDTO> map = _mapper.Map<List<CategoryReturnDTO>>(data);
			return Ok(map);
		}
		[HttpPost("Create")]
		public async Task<IActionResult> Create(CategoryCReateDTO createDTO)
		{
			Category category = _mapper.Map<Category>(createDTO);
			await _unitOfWork.RepositoryCategory.InsertAsync(category);
			await _unitOfWork.CommitAsync();
			return Ok(category);
		}
		[HttpPost("Edit")]
		public async Task<IActionResult> Edit(CategoryEditDTO editDTO)
		{
			var data = await _unitOfWork.RepositoryCategory.GetAsync(x => x.Id == editDTO.Id, true);
			if(data==null)
			{
				return NotFound();
			}
			data.Name=editDTO.Name;
			data.Icon = editDTO.Icon;
			await _unitOfWork.CommitAsync();
			return Ok(data);
		}
		[HttpPost("Delete")]
		public async Task<IActionResult> Delete(int id)
		{
			var data =await _unitOfWork.RepositoryCategory.GetAsync(x => x.Id == id, true);
			if(data == null)
			{
				return NotFound();
			}
			data.IsDeleted = true;
			try
			{
				await _unitOfWork.CommitAsync();
			}
			catch
			{
				return BadRequest("Commit problem");
			}
			return Ok();

		}
	}
}
