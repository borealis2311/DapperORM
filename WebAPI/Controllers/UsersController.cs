using Microsoft.AspNetCore.Mvc;
using Services.Dto.Create;
using Services.Dto.Update;
using Services.Repository.User;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/users")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserRepository _userRepo;
		public UsersController(IUserRepository userRepo)
		{
			_userRepo = userRepo;
		}

		[HttpGet]
		public async Task<IActionResult> GetUsers()
		{
			try
			{
				var user = await _userRepo.GetUsers();
				return Ok(user);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}", Name = "UserByAccountID")]
		public async Task<IActionResult> GetUser(int id)
		{
			try
			{
				var user = await _userRepo.GetUser(id);
				if (user == null)
					return NotFound("Unavailable AccountID");

				return Ok(user);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> CreateUser(UserForCreationDto user)
		{
			try
			{
				var createdUser = await _userRepo.CreateUser(user);
				return CreatedAtRoute("UserByAccountID", new { id = createdUser.AccountID }, createdUser);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto user)
		{
			try
			{
				var dbUser = await _userRepo.GetUser(id);
				if (dbUser == null)
					return NotFound("Unavailable AccountID");

				await _userRepo.UpdateUser(id, user);
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			try
			{
				var dbUser = await _userRepo.GetUser(id);
				if (dbUser == null)
					return NotFound("Unavailable AccountID");

				await _userRepo.DeleteUser(id);
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("ByAccountID/{id}")]
		public async Task<IActionResult> Decentralization(int id)
		{
			try
			{
				var user = await _userRepo.Decentralization(id);
				if (user == null)
					return NotFound("Unavailable AccountID");

				return Ok(user);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
