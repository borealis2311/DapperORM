using Dapper;
using Domain.TableClass;
using Services.Context;
using Services.Dto.Create;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Repository.FuncInRole
{
    public class FirRepository : IFirRepository
    {
        private readonly DapperContext _context;
        public FirRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SAM_FuncInRole>> GetFirs()
        {
            var query = "SELECT * FROM SAM_FuncInRole";
            using var connection = _context.CreateConnection();
            var Firs = await connection.QueryAsync<SAM_FuncInRole>(query);
            return Firs.ToList();
        }
        public async Task<SAM_FuncInRole> GetFir(int id)
        {
            var query = "SELECT * FROM SAM_FuncInRole WHERE FID = @id";
            using var connection = _context.CreateConnection();
            var Fir = await connection.QuerySingleOrDefaultAsync<SAM_FuncInRole>(query, new { id });
            return Fir;
        }
        public async Task<SAM_FuncInRole> CreateFir(FirForCreationDto Fir)
        {
            var query = "INSERT INTO SAM_FuncInRole (RoleID, FuncID, IsBlocked, CreatedDate) OUTPUT INSERTED.FID " +
                "VALUES (@RoleID, @FuncID, @IsBlocked, @CreatedDate)";

            var parameters = new DynamicParameters();
            parameters.Add("RoleID", Fir.RoleID, DbType.Int32);
            parameters.Add("FuncID", Fir.FuncID, DbType.Int32);
            parameters.Add("IsBlocked", false, DbType.Boolean);
            parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);

            using var connection = _context.CreateConnection();
            var id = await connection.ExecuteScalarAsync<int>(query, parameters);

            var createdUser = new SAM_FuncInRole
            {
                FID = id,
                RoleID = Fir.RoleID,
                FuncID = Fir.FuncID,
                IsBlocked = Fir.IsBlocked,
                CreatedDate = DateTime.Now,
            };

            return createdUser;
        }
        public async Task DeleteFir(int id)
        {
            var query = "DELETE FROM SAM_FuncInRole WHERE FID = @id";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { id });
        }
    }
}
