using Project.Business;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectFINAL.Controllers
{
    public class HomeController : BaseController
    {
        private UserService userService;
        public HomeController()
        {
            userService = new UserService();
        }
       
        public ActionResult Index()
        {
            return View();
        }
    
        public ActionResult MyPlants()
        {
            return View();
        }
    }
}