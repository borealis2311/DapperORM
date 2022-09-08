using DataAccessEF.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccessEF.UnitOfWork
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly TrainingContext context;
        private DbSet<T> _entities;
        public GenericRepository(TrainingContext context)
        {
            this.context = context;
        }
        /// <summary>
        ///     Gets an entity set
        /// </summary>
        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = context.Set<T>();

                return _entities;
            }
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            context.Set<T>().AddRange(entities);
        }
        public virtual async Task AddAsync(T entity)
        {
            await Entities.AddAsync(entity);
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return context.Set<T>().Where(expression);
        }
        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }
        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }
        public void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
        }
        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }
        public IQueryable<T> Queryable()
        {
            return Entities;
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return context.Set<T>().Where(expression).AsNoTracking();
        }
    }
}
