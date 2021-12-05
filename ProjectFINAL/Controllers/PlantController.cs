using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectFINAL.Controllers
{
    public class PlantController : Controller
    {
        // GET: Plant
        private PlantManager plantManager;
        private HumidityHistoryManager humidityHistoryManager;
        public PlantController()
        {
            plantManager = new PlantManager();
            humidityHistoryManager = new HumidityHistoryManager();
        }
        [HttpGet]
        public JsonResult GetCurrentHumidityRate(int plantId)
        {
            var entity = humidityHistoryManager.GetCurrentHumidityRateByPlantId(plantId);
            return Json(entity.Data.HumidityRate, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetHumidityAndTemperatureToDb(double temperature, double humidityRate, int plantId)
        {
            var result = humidityHistoryManager.GetCurrentHumidityFromNodeMCU(humidityRate, plantId, temperature);
            var data = result.Data;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetRequiredHumidityRate(int plantId)
        {
            var result = plantManager.GetRequiredHumidityRateById(plantId);
            return Json(result.Data.RequiredHumidityRate, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUsersPlants(int userId)
        {
            var result = plantManager.GetUsersPlantsByUserId(userId);
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