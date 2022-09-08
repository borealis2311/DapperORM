using Microsoft.AspNetCore.Mvc;
using Services.Dto.Create;
using Services.Repository.UserInRole;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
		[Route("api/uirs")]
		[ApiController]
		public class UirsController : ControllerBase
		{
			private readonly IUirRepository _uirRepo;

			public UirsController(IUirRepository uirRepo)
			{
				_uirRepo = uirRepo;
			}

			[HttpGet]
			public async Task<IActionResult> GetUirs()
			{
				try
				{
					var uirs = await _uirRepo.GetUirs();
					return Ok(uirs);
				}
				catch (Exception ex)
				{
					return StatusCode(500, ex.Message);
				}
			}

			[HttpGet("{id}", Name = "uirByUID")]
			public async Task<IActionResult> GetUir(int id)
			{
				try
				{
					var uir = await _uirRepo.GetUir(id);
					if (uir == null)
						return NotFound("Unavailable UID");

					return Ok(uir);
				}
				catch (Exception ex)
				{
					return StatusCode(500, ex.Message);
				}
			}

			[HttpPost]
			public async Task<IActionResult> Createuir(UirForCreationDto uir)
			{
				try
				{
					var createduir = await _uirRepo.CreateUir(uir);
					return CreatedAtRoute("uirByUID", new { id = createduir.UID }, createduir);
				}
				catch (Exception ex)
				{
					return StatusCode(500, ex.Message);
				}
			}

			[HttpDelete("{id}")]
			public async Task<IActionResult> DeleteUir(int id)
			{
				try
				{
					var dbuir = await _uirRepo.GetUir(id);
					if (dbuir == null)
						return NotFound("Unavailable UID");

					await _uirRepo.DeleteUir(id);
					return Ok();
				}
				catch (Exception ex)
				{
					return StatusCode(500, ex.Message);
				}
			}
		}
}
