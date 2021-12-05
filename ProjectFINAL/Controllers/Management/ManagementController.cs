using Project.Business;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectFINAL.Controllers.Management
{
    public class ManagementController : BaseController
    {
        private UserManager _userManager;
        public ManagementController()
        {
            _userManager = new UserManager();
        }
        [Authorize]
        public ActionResult AddUser()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddUser(User user)
        {
            var result = _userManager.Add(user);
            if(result.HasError)
            {
                TempData["ErrMsg"] = result.ResultMessage;
            }
            else
            {
                TempData["Success"] = "Kayıt Başarıyla Oluşturuldu";
            }
            return RedirectToAction("AddUser");
        }
        [Authorize]
        [HttpGet]
        public ActionResult ViewUsers()
        {
            return View();
        }
    }
}