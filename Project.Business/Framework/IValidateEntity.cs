using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Framework
{
    public interface IValidateEntity<T>
    {
        bool IsNullOrEmpty(T Entity);
    }
}
