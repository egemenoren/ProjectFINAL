using Newtonsoft.Json;
using Project.Business;
using Project.Entity;
using ProjectFINAL.ApiModel;
using ProjectFINAL.ViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectFINAL.Controllers
{
    public class HomeController : BaseController
    {
        private UserService userService;
        private WateringHistoryService wateringHistoryService;
        private HumidityHistoryService humidityHistoryService;
        private PlannedWateringService plannedWateringService;
        private PlantService plantService;
        public HomeController()
        {
            userService = new UserService();
            wateringHistoryService = new WateringHistoryService();
            humidityHistoryService = new HumidityHistoryService();
            plannedWateringService = new PlannedWateringService();
            plantService = new PlantService();
        }
        private async Task<WeatherResponseModel> SetWeatherCondition()
        {
            var url = "http://api.weatherapi.com";
            var client = new RestClient(url);
            var request = new RestRequest("/v1/current.json", Method.Get);
            request.AddQueryParameter("key", "bd711085065843fe8a883757220106");
            request.AddQueryParameter("q", "istanbul");
            request.AddQueryParameter("lang", "tr");
            var result = await client.ExecuteAsync(request);
            var model = new WeatherResponseModel();
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                model = JsonConvert.DeserializeObject<WeatherResponseModel>(result.Content);
            return model;
        }
        public async Task<ActionResult> Index()
        {
            

            var allPlants = plantService.GetPlantListById(currentUser.Id);
            var plantIdList = new List<int>();
            foreach(var item in allPlants.Data)
            {
                plantIdList.Add(item.Id);
            }
            var totalTodayWateringPlanCount = wateringHistoryService.FindBy(x => x.CreatedTime.Year == DateTime.Now.Year
            && x.CreatedTime.Month == DateTime.Now.Month && x.CreatedTime.Day == DateTime.Now.Day && plantIdList.Contains(x.PlantId)).Data;
            ViewBag.TodayWatered = totalTodayWateringPlanCount.Count();


            ViewBag.TotalPlantCount = allPlants.Data.Count;

            var totalWateringCount = 0;
            foreach (var item in allPlants.Data)
            {
                totalWateringCount += wateringHistoryService.FindBy(x => x.PlantId == item.Id).Data.Count();
            }
            ViewBag.TotalAutomaticWatering = totalWateringCount;


            var viewModel = new IndexViewModel
            {
                PlantInformation = new List<DTO.PlantDTO>()
            };

            var usersPlants = new PlantService().GetPlantListById(currentUser.Id);
            foreach (var item in usersPlants.Data)
            {
                var lastWatered = wateringHistoryService.GetLastWateringTime(item.Id);
                viewModel.PlantInformation.Add(new DTO.PlantDTO
                {
                    Plant = item,
                    LastWateredTime = lastWatered.Data ?? new DateTime?(),
                    CurrentHumidityRate = humidityHistoryService.GetCurrentHumidityRateByPlantId(item.Id).Data != null ? (decimal)humidityHistoryService.GetCurrentHumidityRateByPlantId(item.Id).Data.HumidityRate : 0,
                    WateringType = plannedWateringService.GetByPlantId(item.Id).Data.WateringType ?? WateringType.Not_Set
                });
            }

            viewModel.Weather = await SetWeatherCondition();

            return View(viewModel);
        }

        public ActionResult MyPlants()
        {
            return View();
        }
    }
}