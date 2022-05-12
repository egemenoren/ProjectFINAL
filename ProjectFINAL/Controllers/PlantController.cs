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
    public class PlantController : BaseController
    {
        // GET: Plant
        private PlantService plantService;
        private HumidityHistoryService humidityHistoryService;
        public PlantController()
        {
            plantService = new PlantService();
            humidityHistoryService = new HumidityHistoryService();
        }

        [Authorize]
        public ActionResult Status(int id)
        {    
            var result = plantService.GetById(id);
            return View(result.Data);
        }
    }
}