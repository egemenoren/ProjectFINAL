using Project.Business;
using ProjectFINAL.DTO;
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
        private readonly UserService userService;
        public ApiController()
        {
            plantService = new PlantService();
            humidityHistoryService = new HumidityHistoryService();
            plannedWateringService = new PlannedWateringService();
            wateringHistoryService = new WateringHistoryService();
            userService = new UserService();
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
        public JsonResult DeactiveUser(int id)
        {
            var result = userService.DeactiveUser(id);
            return Json(new { json = result }, JsonRequestBehavior.AllowGet);
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
            var plant = plantService.GetById(plantId);
            if (checkNeedsToBeWatered.Data)
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
                return Json(new { isNeedWater = checkNeedsToBeWatered.Data, waterTime = wateringTime.Data.WateringSecond,pumpAddress = plant.Data.PumpCardAddress }, JsonRequestBehavior.AllowGet);
            return Json(new { isNeedWater = false, waterTime = 0 , pumpAddress = plant.Data.PumpCardAddress }, JsonRequestBehavior.AllowGet);
            //TODO: Eğer bitkinin sulama saati gelmiş ise veya nem oranı gerekenin altında ise sulamayı başlat.

        }

        [HttpGet]
        public JsonResult GetUsersPlants(int userId)
        {
            var result = plantService.GetUsersPlantsByUserId(userId);
            var list = new List<PlantApiResponse>();
            foreach (var item in result.ListData)
            {
                list.Add(new PlantApiResponse
                {
                    PlantId = item.Id,
                    HumidityCardAddress =  item.HumidityCardAddress,
                    PumpCardAddress = item.PumpCardAddress
                });
            }
            return Json(new { list = list }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUsers()
        {
            var result = userService.GetActiveUsers();
            return Json(new { json = result }, JsonRequestBehavior.AllowGet);
        }

    }
}