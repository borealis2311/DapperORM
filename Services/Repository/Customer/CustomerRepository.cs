using Dapper;
using Domain.TableClass;
using Services.Context;
using Services.Dto.Create;
using Services.Dto.Update;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Repository.Customer
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DapperContext _context;
        public CustomerRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MD_Customer>> GetCustomers()
        {
            var query = "SELECT * FROM MD_Customer";
            using var connection = _context.CreateConnection();
            var customers = await connection.QueryAsync<MD_Customer>(query);
            return customers.ToList();
        }

        public async Task<MD_Customer> GetCustomer(int id)
        {
            var query = "SELECT * FROM MD_Customer WHERE CustomerID = @id";
            using var connection = _context.CreateConnection();
            var customer = await connection.QuerySingleOrDefaultAsync<MD_Customer>(query, new { id });
            return customer;
        }

        public async Task<MD_Customer> CreateCustomer(CustomerForCreationDto customer)
        {
            var query = "INSERT INTO MD_Customer (CustomerCode, FullName, TaxCode, Address, IsBlocked, CreatedDate) OUTPUT INSERTED.CustomerID " +
            "VALUES (@CustomerCode, @FullName, @TaxCode, @Address, @IsBlocked, @CreatedDate)";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerCode", customer.CustomerCode, DbType.String);
            parameters.Add("FullName", customer.FullName, DbType.String);
            parameters.Add("TaxCode", customer.TaxCode, DbType.String);
            parameters.Add("Address", customer.Address, DbType.String);
            parameters.Add("IsBlocked", false, DbType.Boolean);
            parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);

            using var connection = _context.CreateConnection();
            var id = await connection.ExecuteScalarAsync<int>(query, parameters);

            var createdCustomer = new MD_Customer
            {
                CustomerID = id,
                CustomerCode = customer.CustomerCode,
                FullName = customer.FullName,
                TaxCode = customer.TaxCode,
                Address = customer.Address,
                IsBlocked = customer.IsBlocked,
                CreatedDate = DateTime.Now,
            };

            return createdCustomer;
        }

        public async Task UpdateCustomer(int id, CustomerForUpdateDto customer)
        {
            var query = "UPDATE MD_Customer SET FullName = @FullName, TaxCode = @TaxCode, Address = @Address, IsBlocked = @IsBlocked, UpdatedDate = @UpdatedDate WHERE CustomerID = @CustomerID";

            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", id, DbType.Int32);
            parameters.Add("FullName", customer.FullName, DbType.String);
            parameters.Add("TaxCode", customer.TaxCode, DbType.String);
            parameters.Add("Address", customer.Address, DbType.String);
            parameters.Add("IsBlocked", customer.IsBlocked, DbType.Boolean);
            parameters.Add("UpdatedDate", DateTime.Now, DbType.DateTime);

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteCustomer(int id)
        {
            var query = "DELETE FROM MD_Customer WHERE CustomerID = @id";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { id });
        }
        public async Task<List<MD_Customer>> GetAccountForCustomers()
        {
            var query = "SELECT * FROM MD_Customer c JOIN SAM_UserAccount u ON c.CustomerID = u.CustomerID";
            using var connection = _context.CreateConnection();
            var customerDict = new Dictionary<int, MD_Customer>();

            var customers = await connection.QueryAsync<MD_Customer, SAM_UserAccount, MD_Customer>(
                query, (customer, user) =>
                {
                    if (!customerDict.TryGetValue(customer.CustomerID, out var currentCustomer))
                    {
                        currentCustomer = customer;
                        customerDict.Add(currentCustomer.CustomerID, currentCustomer);
                    }

                    currentCustomer.Users.Add(user);
                    return currentCustomer;
                }, splitOn: "CustomerID, AccountID"
            );
            return customers.Distinct().ToList();
        }
    }
}
