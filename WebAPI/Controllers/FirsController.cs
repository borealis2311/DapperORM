using Microsoft.AspNetCore.Mvc;
using Services.Dto.Create;
using Services.Repository.FuncInRole;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
	[Route("api/firs")]
	[ApiController]
	public class FirsController : ControllerBase
	{
		private readonly IFirRepository _firRepo;
		public FirsController(IFirRepository firRepo)
		{
			_firRepo = firRepo;
		}

		[HttpGet]
		public async Task<IActionResult> GetFirs()
		{
			try
			{
				var firs = await _firRepo.GetFirs();
				return Ok(firs);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}", Name = "firByFID")]
		public async Task<IActionResult> GetFir(int id)
		{
			try
			{
				var fir = await _firRepo.GetFir(id);
				if (fir == null)
					return NotFound("Unavailable FID");

				return Ok(fir);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Createfir(FirForCreationDto fir)
		{
			try
			{
				var createdfir = await _firRepo.CreateFir(fir);
				return CreatedAtRoute("firByFID", new { id = createdfir.FID }, createdfir);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteFir(int id)
		{
			try
			{
				var dbfir = await _firRepo.GetFir(id);
				if (dbfir == null)
					return NotFound("Unavailable FID");

				await _firRepo.DeleteFir(id);
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
