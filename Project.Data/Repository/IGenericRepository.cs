using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Repository
{
    public interface IGenericRepository<T> where T:BaseEntity
    {
        void Insert(T Entity);
        void Remove(int id);
        void Update(T Entity);
        T GetById(int id);
        T GetByParameter(Expression<Func<T, bool>> Filter = null);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> Filter = null);
        void SaveChanges();
    }
}
