using Project.Business.Middleware;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business
{
    public class PlannedWateringService : BaseService<PlannedWaterings>
    {
        public ServiceResult<PlannedWaterings> GetByPlantId(int plantId)
        {
            return  new ServiceResult<PlannedWaterings>(_repo.GetAll(x => x.PlantId == plantId).FirstOrDefault());
        }
        public override ServiceResult Update(PlannedWaterings model)
        {
            if(model.WateringType == WateringType.Humidity)
            {
                model.Period = null;
                model.WateringHour = null;
            }
            else
            {
                model.LimitHumidityRate = null;
            }
            try
            {
                _repo.Update(model);
                return new ServiceResult(ServiceResultCode.Success);
            }
            catch(Exception ex)
            {
                return new ServiceResult(ServiceResultCode.Generic);
            }
            
        }
    }
}
