using Project.Business.Middleware;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business
{
    public class PlantManager:BaseManager<Plant>
    {
        public ServiceResult GetPlantListById(int UserId)
        {
            try
            {
                var entity = _repo.GetByParameter(x => x.UserId == UserId);
                return new ServiceResult<Plant>(entity);
            }
            catch(Exception ex)
            {
                return new ServiceResult(ServiceResultCode.Generic, ex.Message);
            }
        }
        public ServiceResult<Plant> GetRequiredHumidityRateById(int id)
        {
            try
            {
                var entity = _repo.GetById(id);
                return new ServiceResult<Plant>(entity);
            }
            catch(Exception ex)
            {
                return new ServiceResult<Plant>(ServiceResultCode.Generic, ex.Message);
            }
            
        }
        public ServiceResult<Plant> GetUsersPlantsByUserId(int userId)
        {
            try
            {
                var entity = _repo.GetAll(x => x.UserId == userId).ToList();

                return new ServiceResult<Plant>(entity);
            }
            catch (Exception ex)
            {
                return new ServiceResult<Plant>(ServiceResultCode.Generic, ex.Message);
            }
        }
        
    }
}
