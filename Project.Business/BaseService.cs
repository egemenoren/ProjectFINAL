using Project.Business.Framework;
using Project.Business.Middleware;
using Project.Data.Repository;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Project.Business
{
    public abstract class BaseService<T> where T:BaseEntity
    {
        internal readonly GenericRepository<T> _repo;
        internal readonly ValidateEntity<T> _validation;
        public BaseService()
        {
            _repo = new GenericRepository<T>();
            _validation = new ValidateEntity<T>();
            
            
        }
        public virtual ServiceResult<IEnumerable<T>> FindBy(Expression<Func<T,bool>> filter=null)
        {
            if (filter != null)
                return new ServiceResult<IEnumerable<T>>(_repo.GetAll(filter));
            return new ServiceResult<IEnumerable<T>>(_repo.GetAll());
            
        }
        public virtual ServiceResult<IEnumerable<T>> GetAll()
        {
            var list = _repo.GetAll();
            return new ServiceResult<IEnumerable<T>>(list);
        }
        public virtual ServiceResult<T> GetById(int Id)
        {
            var returnEntity = _repo.GetById(Id);
            if (_validation.IsNullOrEmpty(returnEntity))
                return new ServiceResult<T>(ServiceResultCode.RecordNotFound);
            return new ServiceResult<T>(returnEntity);

        }
        public virtual ServiceResult Remove(int Id)
        {
            var entityToRemove = _repo.GetById(Id);
            if (_validation.IsNullOrEmpty(entityToRemove))
                return new ServiceResult(ServiceResultCode.NullException);
            try
            {
                _repo.Remove(Id);
                return SaveChanges();
            }
            catch (Exception ex)
            {
                return new ServiceResult(ServiceResultCode.Generic, ex.Message);
            }
        }
        public virtual ServiceResult Add(T Entity)
        {
            if (Entity != null)
                try
                {
                    _repo.Insert(Entity);
                    return SaveChanges();
                }
                catch(Exception ex)
                {
                    return new ServiceResult(ServiceResultCode.Generic);
                }
                
            else
            {
                return new ServiceResult(ServiceResultCode.NullException);
            }
               
        }
        public virtual ServiceResult Update(T Entity)
        {
            if (_validation.IsNullOrEmpty(Entity))
                return new ServiceResult(ServiceResultCode.NullException);
            try
            {
                _repo.Update(Entity);
                return SaveChanges();
            }
            catch (Exception ex)
            {
                return new ServiceResult(ServiceResultCode.Generic,ex.Message);
            }
        }
        public ServiceResult SaveChanges()
        {
            try
            {
                _repo.SaveChanges();
                return new ServiceResult(ServiceResultCode.Success, "Successfully Done");
            }
            catch (Exception ex)
            {
                return new ServiceResult(ServiceResultCode.Generic, ex.Message);
            }
        }
    }
}
