using Domain.TableClass;
using Services.Dto.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository.FuncInRole
{
    public interface IFirRepository
    {
        public Task<IEnumerable<SAM_FuncInRole>> GetFirs();
        public Task<SAM_FuncInRole> GetFir(int id);
        public Task<SAM_FuncInRole> CreateFir(FirForCreationDto Fir);
        public Task DeleteFir(int id);
    }
}
