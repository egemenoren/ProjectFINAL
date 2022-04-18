using Project.Business.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business
{
    public interface IBaseManager<T>
    {
        ServiceResult<IEnumerable<T>> GetAll();
        ServiceResult<T> GetById(int Id);
        ServiceResult Remove(int Id);
        ServiceResult Add(T Entity);
        ServiceResult Update(T Entity);
        ServiceResult SaveChanges();
    }
}
