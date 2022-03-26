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

        [Authorize]
        public ActionResult Status(int id)
        {
            int userId = int.Parse(Session["UserId"].ToString());
            var result = plantManager.GetById(id);
            return View(result.Data);
        }
    }
}