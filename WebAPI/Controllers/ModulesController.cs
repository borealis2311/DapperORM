using Microsoft.AspNetCore.Mvc;
using Services.Dto.Create;
using Services.Dto.Update;
using Services.Repository.Module;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/modules")]
    [ApiController]
	public class ModulesController : ControllerBase
	{
		private readonly IModuleRepository _moduleRepo;

		public ModulesController(IModuleRepository moduleRepo)
		{
			_moduleRepo = moduleRepo;
		}

		[HttpGet]
		public async Task<IActionResult> GetModules()
		{
			try
			{
				var modules = await _moduleRepo.GetModules();
				return Ok(modules);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}", Name = "ModuleByModuleID")]
		public async Task<IActionResult> GetModule(int id)
		{
			try
			{
				var module = await _moduleRepo.GetModule(id);
				if (module == null)
					return NotFound("Unavailable ModuleID");

				return Ok(module);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> CreateModule(ModuleForCreationDto module)
		{
			try
			{
				var createdModule = await _moduleRepo.CreateModule(module);
				return CreatedAtRoute("ModuleByModuleID", new { id = createdModule.ModuleID }, createdModule);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateModule(int id, ModuleForUpdateDto module)
		{
			try
			{
				var dbModule = await _moduleRepo.GetModule(id);
				if (dbModule == null)
					return NotFound("Unavailable ModuleID");

				await _moduleRepo.UpdateModule(id, module);
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteModule(int id)
		{
			try
			{
				var dbModule = await _moduleRepo.GetModule(id);
				if (dbModule == null)
					return NotFound("Unavailable ModuleID");

				await _moduleRepo.DeleteModule(id);
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
