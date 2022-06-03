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
            if (_dbSet.Where(filter).OrderByDescending(x => x.CreatedTime).Any())
                return _dbSet.Where(filter).OrderByDescending(x => x.CreatedTime).First();
            return null;
        }
        public List<object> LastSixMonthsData(int plantId)
        {
            var currentDate = DateTime.Now;
            var lastSixMonth = DateTime.Now.AddMonths(-6);
            return _dbSet.Where(x => x.PlantId == plantId && x.CreatedTime > lastSixMonth && x.CreatedTime < currentDate)
                .GroupBy(g =>g.CreatedTime.Month , c=>new {c.HumidityRate,c.PlantId,c.CreatedTime.Year })
                .Select(g => new
            {
                Month = g.Key,
                Year = g.Select(x=>x.Year).Distinct().FirstOrDefault(),
                Humdity = g.Select(x=>x.HumidityRate).Average(),
                PlantId = g.Select(x=>x.PlantId).FirstOrDefault()   
            }).OrderBy(x=>x.Year).ToList<object>();
        }


    }
}
