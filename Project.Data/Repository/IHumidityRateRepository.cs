using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Repository
{
    public interface IHumidityRateRepository
    {
        HumidityHistory GetLastDataByParameter(Expression<Func<HumidityHistory, bool>> filter = null);
        List<object> LastSixMonthsData(int plantId);
    }
}
