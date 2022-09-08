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

namespace Services.Repository.Function
{
    public class FunctionRepository : IFunctionRepository
    {
        private readonly DapperContext _context;

        public FunctionRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SAM_Function>> GetFunctions()
        {
            var query = "SELECT * FROM SAM_Function";

            using (var connection = _context.CreateConnection())
            {
                var functions = await connection.QueryAsync<SAM_Function>(query);
                return functions.ToList();
            }
        }

        public async Task<SAM_Function> GetFunction(int id)
        {
            var query = "SELECT * FROM SAM_Function WHERE FuncID = @id";

            using (var connection = _context.CreateConnection())
            {
                var Function = await connection.QuerySingleOrDefaultAsync<SAM_Function>(query, new { id });

                return Function;
            }
        }

        public async Task<SAM_Function> CreateFunction(FunctionForCreationDto Function)
        {
            var query = "INSERT INTO SAM_Function (FuncCode, FuncDesc, URL, Icon, OrderNo, ModuleID, IsBlocked, CreatedDate) OUTPUT INSERTED.FuncID " +
                "VALUES (@FuncCode, @FuncDesc, @URL, @Icon, @OrderNo, @ModuleID, @IsBlocked, @CreatedDate)";

            var parameters = new DynamicParameters();
            parameters.Add("FuncCode", Function.FuncCode, DbType.String);
            parameters.Add("FuncDesc", Function.FuncDesc, DbType.String);
            parameters.Add("URL", Function.URL, DbType.String);
            parameters.Add("Icon", Function.Icon, DbType.String);
            parameters.Add("OrderNo", 0, DbType.Int32);
            parameters.Add("ModuleID", Function.ModuleID, DbType.Int32);
            parameters.Add("IsBlocked", false, DbType.Boolean);
            parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.ExecuteScalarAsync<int>(query, parameters);

                var createdFunction = new SAM_Function
                {
                    FuncID = id,
                    FuncCode = Function.FuncCode,
                    FuncDesc = Function.FuncDesc,
                    URL = Function.URL,
                    Icon = Function.Icon,
                    OrderNo = Function.OrderNo,
                    ModuleID = Function.ModuleID,
                    IsBlocked = Function.IsBlocked,
                    CreatedDate = DateTime.Now,
                };

                return createdFunction;
            }
        }

        public async Task UpdateFunction(int id, FunctionForUpdateDto Function)
        {
            var query = "UPDATE SAM_Function SET FuncDesc = @FuncDesc, URL = @URL, Icon = @Icon, IsBlocked = @IsBlocked, UpdatedDate = @UpdatedDate WHERE FuncID = @FuncID";

            var parameters = new DynamicParameters();
            parameters.Add("FuncID", id, DbType.Int32);
            parameters.Add("FuncDesc", Function.FuncDesc, DbType.String);
            parameters.Add("URL", Function.URL, DbType.String);
            parameters.Add("Icon", Function.Icon, DbType.String);
            parameters.Add("IsBlocked", Function.IsBlocked, DbType.Boolean);
            parameters.Add("UpdatedDate", DateTime.Now, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteFunction(int id)
        {
            var query = "DELETE FROM SAM_Function WHERE FuncID = @id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
    }
}
