using Microsoft.AspNetCore.Mvc;
using Services.Dto.Create;
using Services.Dto.Update;
using Services.Repository.Function;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/functions")]
	[ApiController]
	public class FunctionsController : ControllerBase
	{
		private readonly IFunctionRepository _FunctionRepo;

		public FunctionsController(IFunctionRepository FunctionRepo)
		{
			_FunctionRepo = FunctionRepo;
		}

		[HttpGet]
		public async Task<IActionResult> GetFunctions()
		{
			try
			{
				var Functions = await _FunctionRepo.GetFunctions();
				return Ok(Functions);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}", Name = "FunctionByFuncID")]
		public async Task<IActionResult> GetFunction(int id)
		{
			try
			{
				var Function = await _FunctionRepo.GetFunction(id);
				if (Function == null)
					return NotFound("Unavailable FuncID");

				return Ok(Function);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> CreateFunction(FunctionForCreationDto Function)
		{
			try
			{
				var createdFunction = await _FunctionRepo.CreateFunction(Function);
				return CreatedAtRoute("FunctionByFuncID", new { id = createdFunction.FuncID }, createdFunction);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateFunction(int id, FunctionForUpdateDto Function)
		{
			try
			{
				var dbFunction = await _FunctionRepo.GetFunction(id);
				if (dbFunction == null)
					return NotFound("Unavailable FuncID");

				await _FunctionRepo.UpdateFunction(id, Function);
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteFunction(int id)
		{
			try
			{
				var dbFunction = await _FunctionRepo.GetFunction(id);
				if (dbFunction == null)
					return NotFound("Unavailable FuncID");

				await _FunctionRepo.DeleteFunction(id);
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
