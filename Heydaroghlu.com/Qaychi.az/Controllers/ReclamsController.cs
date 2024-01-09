using App.Application.DTOs.CategoryDTOs;
using App.Application.DTOs.ReclamDTOs;
using App.Application.UnitOfWorks;
using App.Domain.Entitites;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Qaychi.az.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReclamsController : ControllerBase
	{
		readonly IUnitOfWork _unitOfWork;
		readonly IMapper _mapper;
		public ReclamsController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAll()
		{
			var data =await _unitOfWork.RepositoryReclam.GetAllAsync(x => x.IsDeleted == false, false);
			List<ReclamReturnDTO> map = _mapper.Map<List<ReclamReturnDTO>>(data);
			return Ok(map);
		}
		[HttpPost("Create")]
		public async Task<IActionResult> Create(ReclamCreateDTO createDTO)
		{
			Reclam Reclam = _mapper.Map<Reclam>(createDTO);
			await _unitOfWork.RepositoryReclam.InsertAsync(Reclam);
			await _unitOfWork.CommitAsync();
			return Ok(Reclam);
		}
		[HttpPost("Edit")]
		public async Task<IActionResult> Edit(ReclamEditDTO editDTO)
		{
			var data = await _unitOfWork.RepositoryReclam.GetAsync(x => x.Id == editDTO.Id, true);
			if (data == null)
			{
				return NotFound();
			}
			data.Title = editDTO.Title;
			data.Title2 = editDTO.Title2;
			data.Description = editDTO.Description;
			data.DiscountPercent = editDTO.DiscountPercent;

			await _unitOfWork.CommitAsync();
			return Ok(data);
		}
		[HttpPost("Delete")]
		public async Task<IActionResult> Delete(int id)
		{
			var data = await _unitOfWork.RepositoryReclam.GetAsync(x => x.Id == id, true);
			if (data == null)
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
			return Ok(data);

		}
	}

}
