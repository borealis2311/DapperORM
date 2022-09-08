using Domain.TableClass;
using Services.Dto.Create;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repository.UserInRole
{
    public interface IUirRepository
    {
        public Task<IEnumerable<SAM_UserInRole>> GetUirs();
        public Task<SAM_UserInRole> GetUir(int id);
        public Task<SAM_UserInRole> CreateUir(UirForCreationDto uir);
        public Task DeleteUir(int id);

    }
}