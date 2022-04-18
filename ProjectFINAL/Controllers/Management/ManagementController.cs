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
        private UserService _userService;
        public ManagementController()
        {
            _userService = new UserService();
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
            var result = _userService.Add(user);
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
        public ActionResult UserList()
        {
            var result = _userService.GetAll();
            return View(result.Data);
        }
    }
}