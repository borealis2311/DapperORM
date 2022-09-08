using Domain.TableClass;
using Services.Dto.Create;
using Services.Dto.Update;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repository.Function
{
    public interface IFunctionRepository
    {
        public Task<IEnumerable<SAM_Function>> GetFunctions();
        public Task<SAM_Function> GetFunction(int id);
        public Task<SAM_Function> CreateFunction(FunctionForCreationDto Function);
        public Task UpdateFunction(int id, FunctionForUpdateDto Function);
        public Task DeleteFunction(int id);
    }
}
