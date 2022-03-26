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
        public List<object> LastSixMonthsData(int plantId)
        {
            var currentDate = DateTime.Now;
            var lastSixMonth = DateTime.Now.AddMonths(-6);
            var list = _dbSet.Where(x => x.CreatedTime < currentDate && x.CreatedTime > lastSixMonth).ToList();
            var query = _dbSet.Where(x => x.PlantId == plantId && x.CreatedTime > lastSixMonth).GroupBy(g =>g.CreatedTime.Month , c=>new {c.HumidityRate,c.PlantId,c.CreatedTime.Year }).Select(g => new
            {
                Month = Convert.ToDateTime(g.Key.ToString("MMMM")),
                Year = g.Select(x=>x.Year).Distinct().FirstOrDefault(),
                Humdity = g.Select(x=>x.HumidityRate).Average(),
                PlantId = g.Select(x=>x.PlantId).FirstOrDefault()   
            }).ToList<object>();
            return query;
        }


    }
}
