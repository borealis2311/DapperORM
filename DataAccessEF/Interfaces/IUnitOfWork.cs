using System;
using System.Threading.Tasks;

namespace DataAccessEF.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> RepositoryAsync<TEntity>() where TEntity : class;
        int Save();
        Task<int> SaveChangesAsync();
    }
}
