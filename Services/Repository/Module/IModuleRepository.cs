using Domain.TableClass;
using Services.Dto.Create;
using Services.Dto.Update;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repository.Module
{
    public interface IModuleRepository
    {
        public Task<IEnumerable<SAM_Module>> GetModules();
        public Task<SAM_Module> GetModule(int id);
        public Task<SAM_Module> CreateModule(ModuleForCreationDto module);
        public Task UpdateModule(int id, ModuleForUpdateDto module);
        public Task DeleteModule(int id);
    }
}
