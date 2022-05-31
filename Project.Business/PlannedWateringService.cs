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
        private readonly HumidityHistoryService _humidityHistoryService;
        private readonly WateringHistoryService _wateringHistoryService;
        public PlannedWateringService()
        {
            _humidityHistoryService = new HumidityHistoryService();
            _wateringHistoryService = new WateringHistoryService();
        }
        public ServiceResult<PlannedWaterings> GetByPlantId(int plantId)
        {
            return new ServiceResult<PlannedWaterings>(_repo.GetAll(x => x.PlantId == plantId).FirstOrDefault());
        }
        public override ServiceResult Update(PlannedWaterings model)
        {
            if (model.WateringType == WateringType.Humidity)
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
            catch (Exception ex)
            {
                return new ServiceResult(ServiceResultCode.Generic);
            }

        }
        public ServiceResult<bool> CheckNeedsToBeWatered(int plantId, double currentHumidityRate)
        {
            var entity = _repo.GetByParameter(x => x.PlantId == plantId);
            if (entity.WateringType == WateringType.Humidity)
                if (currentHumidityRate <= entity.LimitHumidityRate)
                    return new ServiceResult<bool>(true);
                else
                    return new ServiceResult<bool>(false);
            else
            {
                //Check has the plant watered previous day
                if(DateTime.Now.Hour == entity.WateringHour.Value.Hour && DateTime.Now.Minute > entity.WateringHour.Value.Minute-5 && DateTime.Now.Minute < entity.WateringHour.Value.Minute + 5)
                {
                    var checkIfWatered = _wateringHistoryService.CheckIfHasBeenWatered(plantId);
                    if (checkIfWatered.Data == true)
                        return new ServiceResult<bool>(false);
                    return new ServiceResult<bool>(true);
                }
                return new ServiceResult<bool>(false);
                
            }

            //var currentHumidityRate = _humidityHistoryService.GetCurrentHumidityRateByPlantId(plantId).Data.HumidityRate;
        }
    }
}
