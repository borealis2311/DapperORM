using DataAccessEF.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessEF.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TrainingContext _context;
        private Dictionary<string, dynamic> _repositories;
        public UnitOfWork(TrainingContext context)
        {
            this._context = context;
          
        }
        public IGenericRepository<TEntity> RepositoryAsync<TEntity>() where TEntity : class
        {
          

            if (_repositories == null)
            {
                _repositories = new Dictionary<string, dynamic>();
            }

            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IGenericRepository<TEntity>)_repositories[type];
            }

            var repositoryType = typeof(GenericRepository<>);
            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context));

            return _repositories[type];
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
        #region Dispose
        //https://msdn.microsoft.com/library/ms244737.aspx

        // Dispose() calls Dispose(true)
        public void Dispose()
        {
            _context.Dispose();
        }
        // NOTE: Leave out the finalizer altogether if this class doesn't 
        // own unmanaged resources itself, but leave the other methods
        // exactly as they are. 
   
        #endregion
    }
}
