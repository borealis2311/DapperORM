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

namespace Services.Repository.Module
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly DapperContext _context;

        public ModuleRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SAM_Module>> GetModules()
        {
            var query = "SELECT * FROM SAM_Module";

            using (var connection = _context.CreateConnection())
            {
                var modules = await connection.QueryAsync<SAM_Module>(query);
                return modules.ToList();
            }
        }

        public async Task<SAM_Module> GetModule(int id)
        {
            var query = "SELECT * FROM SAM_Module WHERE ModuleID = @id";

            using (var connection = _context.CreateConnection())
            {
                var module = await connection.QuerySingleOrDefaultAsync<SAM_Module>(query, new { id });

                return module;
            }
        }

        public async Task<SAM_Module> CreateModule(ModuleForCreationDto module)
        {
            var query = "INSERT INTO SAM_Module (ModuleCode, ModuleDesc, Icon, OrderNo, IsBlocked, CreatedDate) OUTPUT INSERTED.ModuleID " +
                "VALUES (@ModuleCode, @ModuleDesc, @Icon, @OrderNo, @IsBlocked, @CreatedDate)";

            var parameters = new DynamicParameters();
            parameters.Add("ModuleCode", module.ModuleCode, DbType.String);
            parameters.Add("ModuleDesc", module.ModuleDesc, DbType.String);
            parameters.Add("Icon", module.Icon, DbType.String);
            parameters.Add("OrderNo", 0, DbType.Int32);
            parameters.Add("IsBlocked", false, DbType.Boolean);
            parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.ExecuteScalarAsync<int>(query, parameters);

                var createdModule = new SAM_Module
                {
                    ModuleID = id,
                    ModuleCode = module.ModuleCode,
                    ModuleDesc = module.ModuleDesc,
                    Icon = module.Icon,
                    OrderNo = module.OrderNo,
                    IsBlocked = module.IsBlocked,
                    CreatedDate = DateTime.Now,
                };

                return createdModule;
            }
        }

        public async Task UpdateModule(int id, ModuleForUpdateDto module)
        {
            var query = "UPDATE SAM_Module SET ModuleDesc = @ModuleDesc, Icon = @Icon, IsBlocked = @IsBlocked, UpdatedDate = @UpdatedDate WHERE ModuleID = @ModuleID";

            var parameters = new DynamicParameters();
            parameters.Add("ModuleID", id, DbType.Int32);
            parameters.Add("ModuleDesc", module.ModuleDesc, DbType.String);
            parameters.Add("Icon", module.Icon, DbType.String);
            parameters.Add("IsBlocked", module.IsBlocked, DbType.Boolean);
            parameters.Add("UpdatedDate", DateTime.Now, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteModule(int id)
        {
            var query = "DELETE FROM SAM_Module WHERE ModuleID = @id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
    }
}
