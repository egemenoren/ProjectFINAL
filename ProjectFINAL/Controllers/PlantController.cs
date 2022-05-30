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

        [Authorize]
        public ActionResult Status(int id)
        {
            var result = plantService.GetById(id);
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
            model.WateringHour = model.WateringHour.Value;
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