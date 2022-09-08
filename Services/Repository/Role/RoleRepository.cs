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

namespace Services.Repository.Role
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DapperContext _context;

        public RoleRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SAM_Role>> GetRoles()
        {
            var query = "SELECT * FROM SAM_Role";

            using (var connection = _context.CreateConnection())
            {
                var Roles = await connection.QueryAsync<SAM_Role>(query);
                return Roles.ToList();
            }
        }

        public async Task<SAM_Role> GetRole(int id)
        {
            var query = "SELECT * FROM SAM_Role WHERE RoleID = @id";

            using (var connection = _context.CreateConnection())
            {
                var Role = await connection.QuerySingleOrDefaultAsync<SAM_Role>(query, new { id });

                return Role;
            }
        }

        public async Task<SAM_Role> CreateRole(RoleForCreationDto Role)
        {
            var query = "INSERT INTO SAM_Role (RoleCode, RoleName, RoleNotes, IsBlocked, CreatedDate) OUTPUT INSERTED.RoleID " +
                "VALUES (@RoleCode, @RoleName, @RoleNotes, @IsBlocked, @CreatedDate)";

            var parameters = new DynamicParameters();
            parameters.Add("RoleCode", Role.RoleCode, DbType.String);
            parameters.Add("RoleName", Role.RoleName, DbType.String);
            parameters.Add("RoleNotes", Role.RoleNotes, DbType.String);
            parameters.Add("IsBlocked", false, DbType.Boolean);
            parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.ExecuteScalarAsync<int>(query, parameters);

                var createdRole = new SAM_Role
                {
                    RoleID = id,
                    RoleCode = Role.RoleCode,
                    RoleName = Role.RoleName,
                    RoleNotes = Role.RoleNotes,
                    IsBlocked = Role.IsBlocked,
                    CreatedDate = DateTime.Now,
                };

                return createdRole;
            }
        }

        public async Task UpdateRole(int id, RoleForUpdateDto Role)
        {
            var query = "UPDATE SAM_Role SET RoleName = @RoleName, RoleNotes = @RoleNotes, IsBlocked = @IsBlocked, UpdatedDate = @UpdatedDate WHERE RoleID = @RoleID";

            var parameters = new DynamicParameters();
            parameters.Add("RoleID", id, DbType.Int32);
            parameters.Add("RoleName", Role.RoleName, DbType.String);
            parameters.Add("RoleNotes", Role.RoleNotes, DbType.String);
            parameters.Add("IsBlocked", Role.IsBlocked, DbType.Boolean);
            parameters.Add("UpdatedDate", DateTime.Now, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteRole(int id)
        {
            var query = "DELETE FROM SAM_Role WHERE RoleID = @id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
    }
}
