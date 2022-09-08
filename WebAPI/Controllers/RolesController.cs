using Microsoft.AspNetCore.Mvc;
using Services.Dto.Create;
using Services.Dto.Update;
using Services.Repository.Role;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/roles")]
    [ApiController]
	public class RolesController : ControllerBase
	{
		private readonly IRoleRepository _RoleRepo;
		public RolesController(IRoleRepository RoleRepo)
		{
			_RoleRepo = RoleRepo;
		}

		[HttpGet]
		public async Task<IActionResult> GetRoles()
		{
			try
			{
				var Roles = await _RoleRepo.GetRoles();
				return Ok(Roles);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}", Name = "RoleByRoleID")]
		public async Task<IActionResult> GetRole(int id)
		{
			try
			{
				var Role = await _RoleRepo.GetRole(id);
				if (Role == null)
					return NotFound("Unavailable RoleID");

				return Ok(Role);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> CreateRole(RoleForCreationDto Role)
		{
			try
			{
				var createdRole = await _RoleRepo.CreateRole(Role);
				return CreatedAtRoute("RoleByRoleID", new { id = createdRole.RoleID }, createdRole);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateRole(int id, RoleForUpdateDto Role)
		{
			try
			{
				var dbRole = await _RoleRepo.GetRole(id);
				if (dbRole == null)
					return NotFound("Unavailable RoleID");

				await _RoleRepo.UpdateRole(id, Role);
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteRole(int id)
		{
			try
			{
				var dbRole = await _RoleRepo.GetRole(id);
				if (dbRole == null)
					return NotFound("Unavailable RoleID");

				await _RoleRepo.DeleteRole(id);
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
