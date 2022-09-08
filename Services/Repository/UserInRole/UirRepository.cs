using Dapper;
using Domain.TableClass;
using Services.Context;
using Services.Dto.Create;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Repository.UserInRole
{
    public class UirRepository : IUirRepository
    {
        private readonly DapperContext _context;

        public UirRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SAM_UserInRole>> GetUirs()
        {
            var query = "SELECT * FROM SAM_UserInRole";

            using (var connection = _context.CreateConnection())
            {
                var uirs = await connection.QueryAsync<SAM_UserInRole>(query);
                return uirs.ToList();
            }
        }

        public async Task<SAM_UserInRole> GetUir(int id)
        {
            var query = "SELECT * FROM SAM_UserInRole WHERE UID = @id";

            using (var connection = _context.CreateConnection())
            {
                var uir = await connection.QuerySingleOrDefaultAsync<SAM_UserInRole>(query, new { id });

                return uir;
            }
        }

        public async Task<SAM_UserInRole> CreateUir(UirForCreationDto uir)
        {
            var query = "INSERT INTO SAM_UserInRole (RoleID, AccountID, ValidDateFrom, IsBlocked, CreatedDate) OUTPUT INSERTED.UID " +
                "VALUES (@RoleID, @AccountID, @ValidDateFrom, @IsBlocked, @CreatedDate)";

            var parameters = new DynamicParameters();
            parameters.Add("RoleID", uir.RoleID, DbType.Int32);
            parameters.Add("AccountID", uir.AccountID, DbType.Int32);
            parameters.Add("ValidDateFrom", DateTime.Now, DbType.DateTime);
            parameters.Add("IsBlocked", false, DbType.Boolean);
            parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.ExecuteScalarAsync<int>(query, parameters);

                var createdUser = new SAM_UserInRole
                {
                    UID = id,
                    RoleID = uir.RoleID,
                    AccountID = uir.AccountID,
                    ValidDateFrom = DateTime.Now,
                    IsBlocked = uir.IsBlocked,
                    CreatedDate = DateTime.Now,
                };

                return createdUser;
            }
        }

        public async Task DeleteUir(int id)
        {
            var query = "DELETE FROM SAM_UserInRole WHERE UID = @id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
    }
}
