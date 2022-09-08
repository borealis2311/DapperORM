using Domain.TableClass;
using Services.Dto.Create;
using Services.Dto.Update;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repository.Customer
{
    public interface ICustomerRepository
	{
		public Task<IEnumerable<MD_Customer>> GetCustomers();
		public Task<MD_Customer> GetCustomer(int id);
		public Task<MD_Customer> CreateCustomer(CustomerForCreationDto customer);
		public Task UpdateCustomer(int id, CustomerForUpdateDto customer);
		public Task DeleteCustomer(int id);
		public Task<List<MD_Customer>> GetAccountForCustomers();
	}
}
