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
        private readonly PlantService plantService;
        public ApiController()
        {
            plantService = new PlantService();
            humidityHistoryService = new HumidityHistoryService();
        }
        [HttpGet]
        public JsonResult GetCurrentHumidityRate(int plantId)
        {
            var entity = humidityHistoryService.GetCurrentHumidityRateByPlantId(plantId);
            return Json(entity.Data.HumidityRate, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetAverageHumidityLastSixMonths(int plantId)
        {
            return null;
        }

        [HttpGet]
        public JsonResult GetHumidityAndTemperatureToDb(double temperature, double humidityRate, int plantId)
        {
            var result = humidityHistoryService.GetCurrentHumidityFromNodeMCU(humidityRate, plantId, temperature);
            var data = result.Data;
            var requiredHumidityRate = GetRequiredHumidityRate(plantId);
            if (humidityRate < requiredHumidityRate)
                return null; //TODO
                //TODO:Sulamayı başlat.

            //TODO: Eğer bitkinin sulama saati gelmiş ise veya nem oranı gerekenin altında ise sulamayı başlat.
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        private double GetRequiredHumidityRate(int plantId)
        {
            //TODO:M
            var result = plantService.GetRequiredHumidityRateById(plantId);
            return result.Data;
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