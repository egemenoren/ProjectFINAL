using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Repository
{
    public class HumidityRateRepository : GenericRepository<HumidityHistory>
    {
        public HumidityHistory GetLastDataByParameter(Expression<Func<HumidityHistory, bool>> filter = null)
        {
            return _dbSet.Where(filter).OrderByDescending(x => x.CreatedTime).First();
        }


    }
}
