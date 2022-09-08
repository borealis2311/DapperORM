using Microsoft.AspNetCore.Mvc; 
using Services.Dto.Create;
using Services.Dto.Update;
using Services.Repository.Customer;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/customers")]
	[ApiController]
	public class CustomersController : ControllerBase
	{
		private readonly ICustomerRepository _customerRepo;
		public CustomersController(ICustomerRepository customerRepo)
		{
			_customerRepo = customerRepo;
		}

		[HttpGet]
		public async Task<IActionResult> GetCustomers()
		{
			try
			{
				var customers = await _customerRepo.GetCustomers();
				return Ok(customers);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}", Name = "CustomerByCustomerID")]
		public async Task<IActionResult> GetCustomer(int id)
		{
			try
			{
				var customer = await _customerRepo.GetCustomer(id);
				if (customer == null)
					return NotFound("Unavailable CustomerID");

				return Ok(customer);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> CreateCustomer(CustomerForCreationDto customer)
		{
			try
			{
				var createdCustomer = await _customerRepo.CreateCustomer(customer);
				return CreatedAtRoute("CustomerByCustomerID", new { id = createdCustomer.CustomerID }, createdCustomer);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCustomer(int id, CustomerForUpdateDto customer)
		{
			try
			{
				var dbCustomer = await _customerRepo.GetCustomer(id);
				if (dbCustomer == null)
					return NotFound("Unavailable CustomerID");

				await _customerRepo.UpdateCustomer(id, customer);
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCustomer(int id)
		{
			try
			{
				var dbCustomer = await _customerRepo.GetCustomer(id);
				if (dbCustomer == null)
					return NotFound("Unavailable CustomerID");

				await _customerRepo.DeleteCustomer(id);
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("MultipleMapping")]
        public async Task<IActionResult> GetAccountForCustomers()
        {
            try
            {
                var customer = await _customerRepo.GetAccountForCustomers();
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
	}
}
