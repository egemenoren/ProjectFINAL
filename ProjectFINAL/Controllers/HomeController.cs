﻿using Project.Business;
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
        private UserManager userManager;
        public HomeController()
        {
            userManager = new UserManager();
        }
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult MyPlants()
        {
            return View();
        }
    }
}