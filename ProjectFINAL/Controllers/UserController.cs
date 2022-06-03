using Project.Business;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectFINAL.Controllers
{
    public class UserController : BaseController
    {
        private UserService _userService;
        private Crypto _crypto;
        public UserController()
        {
            _crypto = new Crypto();
            _userService = new UserService();
        }
        // GET: User
        public ActionResult Profile(int id)
        {
            var result = _userService.GetById(id);
            result.Data.SecurityAnswer = _crypto.Decrypt(result.Data.SecurityAnswer);
            return View(result.Data);
        }
        [HttpPost]
        public ActionResult Profile(User user)
        {
            user.SecurityAnswer = _crypto.Encrypt(user.SecurityAnswer);
            var result = _userService.Update(user);
            if (result.ResultCode == Project.Business.Middleware.ServiceResultCode.Success)
            {
                TempData["Success"] = "Başarıyla düzenlendi";
                currentUser = user;
                return View(user);
            }
            else
            {
                TempData["ErrMsg"] = result.ResultMessage;
                return View(user);
            }

                

        }
    }
}