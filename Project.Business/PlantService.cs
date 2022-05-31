using Project.Business.Middleware;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business
{
    public class PlantService:BaseService<Plant>
    {
        public ServiceResult<List<Plant>> GetPlantListById(int UserId)
        {
            try
            {
                var entity = _repo.GetAll(x => x.UserId == UserId).ToList();
                return new ServiceResult<List<Plant>>(entity);
            }
            catch(Exception ex)
            {
                return new ServiceResult<List<Plant>>(ServiceResultCode.Generic, ex.Message);
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
