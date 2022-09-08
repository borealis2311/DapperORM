using Dapper;
using Domain.TableClass;
using Services.Context;
using Services.Dto;
using Services.Dto.Create;
using Services.Dto.Update;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SAM_UserAccount>> GetUsers()
        {
            var query = "SELECT * FROM SAM_UserAccount";

            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<SAM_UserAccount>(query);
                return users.ToList();
            }
        }

        public async Task<SAM_UserAccount> GetUser(int id)
        {
            var query = "SELECT * FROM SAM_UserAccount WHERE AccountID = @id";

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<SAM_UserAccount>(query, new { id });

                return user;
            }
        }

        public async Task<SAM_UserAccount> CreateUser(UserForCreationDto user)
        {
            var query = "INSERT INTO SAM_UserAccount (AccountName, AccPwd, IsActivated, AccountEmail, RecoveryEmail, PhoneNumber, CustomerID, IsBlocked, CreatedDate) OUTPUT INSERTED.AccountID " +
                "VALUES (@AccountName, @AccPwd, @IsActivated, @AccountEmail, @RecoveryEmail, @PhoneNumber, @CustomerID, @IsBlocked, @CreatedDate)";

            var parameters = new DynamicParameters();
            parameters.Add("AccountName", user.AccountName, DbType.String);
            parameters.Add("AccPwd", user.AccPwd, DbType.String);
            parameters.Add("IsActivated", true, DbType.Boolean);
            parameters.Add("AccountEmail", user.AccountEmail, DbType.String);
            parameters.Add("RecoveryEmail", user.RecoveryEmail, DbType.String);
            parameters.Add("PhoneNumber", user.PhoneNumber, DbType.String);
            parameters.Add("CustomerID", user.CustomerID, DbType.Int32);
            parameters.Add("IsBlocked", false, DbType.Boolean);
            parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.ExecuteScalarAsync<int>(query, parameters);

                var createdUser = new SAM_UserAccount
                {
                    AccountID = id,
                    AccountName = user.AccountName,
                    AccPwd = user.AccPwd,
                    IsActivated = !user.IsBlocked,
                    AccountEmail = user.AccountEmail,
                    RecoveryEmail = user.RecoveryEmail,
                    PhoneNumber = user.PhoneNumber,
                    CustomerID = user.CustomerID,
                    IsBlocked = user.IsBlocked,
                    CreatedDate = DateTime.Now,
                };

                return createdUser;
            }
        }

        public async Task UpdateUser(int id, UserForUpdateDto user)
        {
            var query = "UPDATE SAM_UserAccount SET AccPwd = @AccPwd, AccountEmail = @AccountEmail, RecoveryEmail = @RecoveryEmail, PhoneNumber = @PhoneNumber, IsActivated = @IsActivated, IsBlocked = @IsBlocked, UpdatedDate = @UpdatedDate WHERE AccountID = @AccountID";

            var parameters = new DynamicParameters();
            parameters.Add("AccountID", id, DbType.Int32);
            parameters.Add("AccPwd", user.AccPwd, DbType.String);
            parameters.Add("AccountEmail", user.AccountEmail, DbType.String);
            parameters.Add("RecoveryEmail", user.RecoveryEmail, DbType.String);
            parameters.Add("PhoneNumber", user.PhoneNumber, DbType.String);
            parameters.Add("IsActivated", !user.IsBlocked, DbType.Boolean);
            parameters.Add("IsBlocked", user.IsBlocked, DbType.Boolean);
            parameters.Add("UpdatedDate", DateTime.Now, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteUser(int id)
        {
            var query = "DELETE FROM SAM_UserAccount WHERE AccountID = @id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<IEnumerable<Decentralization>> Decentralization(int id)
        {
            var procedureName = "UserDecentralization";
            var parameters = new DynamicParameters();
            parameters.Add("AccountID", id, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryAsync<Decentralization>
                    (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return user;
            }
        }
    }
}
