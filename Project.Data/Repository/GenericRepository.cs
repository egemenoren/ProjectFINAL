using Project.Data.Context;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Project.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        internal DbSet<T> _dbSet;
        internal ProjectContext _context;
        
        public GenericRepository()
        {
            _context = new ProjectContext();
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> Filter = null)
        {
            if (Filter != null)
                return _dbSet.Where(Filter).ToList();
            return _dbSet.ToList();

        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T GetByParameter(Expression<Func<T, bool>> Filter = null)
        {
            var entity = _dbSet.Where(Filter);
            if (entity.Any())           
                return entity.FirstOrDefault();
            return null;
        }

        public void Insert(T Entity)
        {
            _dbSet.Add(Entity);
            SaveChanges();
            
        }

        public void Remove(int id)
        {
            var entity = GetById(id);
            _dbSet.Remove(entity);
            
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(T Entity)
        {
            _dbSet.Attach(Entity);
            _context.Entry(Entity).State = EntityState.Modified;
            SaveChanges();
            
        }

        public void BeginTransaction() {
            _context.Database.BeginTransaction();
        }
        public void CommitTransaction()
        {
            _context.Database.CurrentTransaction?.Commit();
        }
        public void RollbackTransaction()
        {
            _context.Database.CurrentTransaction?.Rollback();
        }
    }
}
