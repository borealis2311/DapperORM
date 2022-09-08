using Domain.TableClass;
using Services.Dto.Create;
using Services.Dto.Update;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repository.Role
{
    public interface IRoleRepository
    {
        public Task<IEnumerable<SAM_Role>> GetRoles();
        public Task<SAM_Role> GetRole(int id);
        public Task<SAM_Role> CreateRole(RoleForCreationDto Role);
        public Task UpdateRole(int id, RoleForUpdateDto Role);
        public Task DeleteRole(int id);
    }
}
