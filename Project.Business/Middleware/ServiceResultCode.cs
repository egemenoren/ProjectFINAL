using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Middleware
{
    public enum ServiceResultCode:int
    {
        Success = 0,
        Generic = 1,
        RecordNotFound = 2,
        AlreadyExists = 3,
        NullException = 4,
        DifferentIPException = 5
    }
}
