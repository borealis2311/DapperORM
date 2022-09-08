using Domain.TableClass;
using Services.Dto;
using Services.Dto.Create;
using Services.Dto.Update;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repository.User
{
    public interface IUserRepository
    {
        public Task<IEnumerable<SAM_UserAccount>> GetUsers();
        public Task<SAM_UserAccount> GetUser(int id);
        public Task<SAM_UserAccount> CreateUser(UserForCreationDto user);
        public Task UpdateUser(int id, UserForUpdateDto user);
        public Task DeleteUser(int id);
        public Task<IEnumerable<Decentralization>> Decentralization(int id);
    }
}
