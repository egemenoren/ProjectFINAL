using Project.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectFINAL.Controllers.API
{
    public class ApiController : Controller
    {
        private readonly HumidityHistoryService humidityHistoryService;
        private readonly PlannedWateringService plannedWateringService;
        private readonly PlantService plantService;
        private readonly WateringHistoryService wateringHistoryService;
        public ApiController()
        {
            plantService = new PlantService();
            humidityHistoryService = new HumidityHistoryService();
            plannedWateringService = new PlannedWateringService();
            wateringHistoryService = new WateringHistoryService();
        }
        [HttpGet]
        public JsonResult GetCurrentHumidityRate(int plantId)
        {
            var entity = humidityHistoryService.GetCurrentHumidityRateByPlantId(plantId);
            return Json(entity.Data.HumidityRate, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAverageHumidityLastSixMonths(int plantId)
        {
            var result = humidityHistoryService.GetLastSixMonthsHumidity(plantId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRealTimeData(int plantId)
        {
            var result = humidityHistoryService.GetLastHoundredHumidityData(plantId);
            var list = new List<List<double>>();
            int i = 0;
            foreach (var item in result.Data)
            {
                list.Add(new List<double> { i, item.HumidityRate });
                i++;
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetHumidityAndTemperatureToDb(double temperature, double humidityRate, int plantId)
        {
            humidityHistoryService.GetCurrentHumidityFromNodeMCU(humidityRate, plantId, temperature);
            var wateringTime = plannedWateringService.GetByPlantId(plantId);
            var checkNeedsToBeWatered = plannedWateringService.CheckNeedsToBeWatered(plantId, humidityRate);
            if(checkNeedsToBeWatered.Data)
            {
                var wateringHistory = wateringHistoryService.Add(new Project.Entity.WateringHistory
                {
                    CreatedTime = DateTime.Now,
                    PlantId = plantId,
                    Status = Project.Entity.DataStatus.Active,
                    LastHumidityRate = humidityRate
                });
                if (wateringHistory.HasError)
                    return Json(new { Error = wateringHistory.ResultMessage });
            }
            
            if (checkNeedsToBeWatered.ResultCode == Project.Business.Middleware.ServiceResultCode.Success)
                return Json(new { isNeedWater = checkNeedsToBeWatered.Data, waterTime = wateringTime.Data.WateringSecond }, JsonRequestBehavior.AllowGet);
            return Json(new { isNeedWater = false, waterTime = 0 }, JsonRequestBehavior.AllowGet);
            //TODO: Eğer bitkinin sulama saati gelmiş ise veya nem oranı gerekenin altında ise sulamayı başlat.

        }

        [HttpGet]
        public JsonResult GetUsersPlants(int userId)
        {
            var result = plantService.GetUsersPlantsByUserId(userId);
            int i = 0;
            string jsonStr = "";
            foreach (var item in result.ListData)
            {
                i++;
                jsonStr += item.Id.ToString();
                if (i != result.ListData.Count())
                    jsonStr += ",";
            }
            return Json(jsonStr, JsonRequestBehavior.AllowGet);
        }

    }
}