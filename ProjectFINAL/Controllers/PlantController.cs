using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project.Business;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectFINAL.Controllers
{
    public class PlantController : BaseController
    {
        // GET: Plant
        private PlantService plantService;
        private HumidityHistoryService humidityHistoryService;
        private PlannedWateringService plannedWateringService;
        public PlantController()
        {
            plantService = new PlantService();
            humidityHistoryService = new HumidityHistoryService();
            plannedWateringService = new PlannedWateringService();
        }

        public ActionResult AddPlant()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPlant(Plant plant)
        {
            var result = plantService.Add(new Plant
            {
                Name = plant.Name,
                UserId = plant.UserId
            });
            if (result.HasError)
            {
                ViewBag.ErrMsg = result.ResultMessage;
                return View();
            }
            TempData["Success"] = "Bitki başarıyla eklendi";
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Status(int id)
        {
            var result = plantService.GetById(id);
            ViewBag.LastWateredTime = new WateringHistoryService().GetLastWateringTime(id);
            var last24hour = DateTime.Now.AddHours(-24);
            ViewBag.WateringTimeInToday = new WateringHistoryService().FindBy(x => x.CreatedTime > last24hour && x.CreatedTime < DateTime.Now && x.PlantId == id);
            return View(result.Data);
        }
        public ActionResult WateringOptions(int id)
        {
            var plannedWatering = plannedWateringService.GetByPlantId(id);

            if (plannedWatering.Data == null)
            {
                var result = plannedWateringService.Add(new Project.Entity.PlannedWaterings
                {
                    PlantId = id
                });
                if (!result.HasError)
                    plannedWatering = plannedWateringService.GetByPlantId(id);
            }
            return View(plannedWatering.Data);

        }
        [HttpPost]
        public ActionResult WateringOptions(PlannedWaterings model)
        {
            var result = plannedWateringService.Update(model);
            if (!result.HasError)
            {
                TempData["Success"] = "Başarıyla kaydedildi";
            }
            else
            {
                TempData["ErrMsg"] = result.ResultMessage;
            }
            return RedirectToAction("Index", "Home");

        }
    }
}