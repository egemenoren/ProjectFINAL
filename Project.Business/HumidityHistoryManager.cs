using Project.Business.Middleware;
using Project.Data.Repository;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business
{
    public class HumidityHistoryManager : BaseManager<HumidityHistory>
    {
        internal HumidityRateRepository _repoHumidity;
        public HumidityHistoryManager()
        {
            _repoHumidity = new HumidityRateRepository();
        }
        public ServiceResult<HumidityHistory> GetCurrentHumidityFromNodeMCU(double humidityRate, int plantId, double temperature)
        {
            try
            {
                var entity = new HumidityHistory
                {
                    HumidityRate = humidityRate,
                    PlantId = plantId,
                    Temperature = temperature
                };
                _repo.Insert(entity);

                return new ServiceResult<HumidityHistory>(entity);

            }
            catch (Exception ex)
            {
                return new ServiceResult<HumidityHistory>(ServiceResultCode.Generic, ex.Message);
            }
        }
        public ServiceResult<HumidityHistory> GetCurrentHumidityRateByPlantId(int plantId)
        {
            try
            {
                var entity = _repoHumidity.GetLastDataByParameter(x => x.PlantId == plantId);
                return new ServiceResult<HumidityHistory>(entity);
            }
            catch (Exception ex)
            {
                return new ServiceResult<HumidityHistory>(ServiceResultCode.Generic, ex.Message);
            }

        }
        public List<object> GetLastSixMonthsHumidity(int plantId)
        {
            var query = _repoHumidity.LastSixMonthsData(plantId);
            return query;
        }
    }
}
