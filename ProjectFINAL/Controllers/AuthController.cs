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
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private UserService userService;
        public AuthController()
        {
            userService = new UserService();
        }
        
        public ActionResult Login()
        {
            if (Session["UserId"] != null)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpGet]
        public ActionResult ValidateIpAdress(int id)
        {
            if (Session["Id"] == null || Session["Id"].ToString() != id.ToString() || Session["UserId"] != null)
            {
                return RedirectToAction("Login");
            }
            var entity = userService.GetById(id);
            if (Session["Id"] == null || Session["Id"].ToString() != id.ToString())
                return RedirectToAction("Login");
            return View(entity.Data);
        }
        [HttpPost]
        public ActionResult ValidateIpAdress(User user, string securityAnswer)
        {
            var entity = userService.GetById(user.Id);
            if (userService.CheckSecurityAnswer(entity.Data, securityAnswer))
            {
                userService.SetNewIpAdress(entity.Data);
                TempData["Success"] = entity.ResultMessage;
                FormsAuthentication.SetAuthCookie(entity.Data.Mail, false);
                Session["UserId"] = entity.Data.Id;
                Session["NameSurname"] = entity.Data.Name + " " + entity.Data.Surname;
                BaseController.currentUser = user;
                return RedirectToAction("Index", "Home");
            }
            TempData["ErrMsg"] = "Girdiğiniz güvenlik sorusu cevabı yanlış.";
            return View(entity.Data);
        }
        [HttpGet]
        public ActionResult FirstLogin(int id)
        {
            var userId = Session["Id"].ToString();

            if (Session["Id"] == null || Session["Id"].ToString() != id.ToString())
            {
                return RedirectToAction("Login");
            }
            else
            {
                userService.GetById(int.Parse(userId));
                var entity = userService.GetById(id);
                if (entity.Data.IsFirstLogin == false)
                    return RedirectToAction("Index", "Home");
                return View(entity.Data);
            }
        }
        [HttpPost]
        public ActionResult FirstLogin(User user)
        {
            var result = userService.FirstLogin(user.Id, user.Password, user.SecurityAnswer);
            if (result.HasError)
            {
                TempData["ErrMsg"] = result.ResultMessage;
                return View();
            }
            TempData["Success"] = "Bilgileriniz başarıyla kaydedildi";
            FormsAuthentication.SetAuthCookie(result.Data.Mail, false);
            Session["UserId"] = result.Data.Id;
            Session["NameSurname"] = result.Data.Name + " " + result.Data.Surname;
            return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        public ActionResult Login(string Email, string password)
        {
            var result = userService.Login(Email, password);

            if (result.Data != null)
            {
                if (result.Data.IsFirstLogin == true)
                {
                    Session["Id"] = result.Data.Id;
                    TempData["Success"] = "İlk girişinizi başarıyla yaptınız. Bu alanda güvenlik için şifrenizi ve güvenlik sorunuzun cevabını belirleyin.";
                    return RedirectToAction("Firstlogin", new { id = result.Data.Id });

                }
            }
            if (result.HasError)
            {

                ViewBag.ErrMsg = result.ResultMessage;
                if (result.ResultCode == Project.Business.Middleware.ServiceResultCode.DifferentIPException)
                {
                    Session["Id"] = result.Data.Id;
                    return RedirectToAction("ValidateIpAdress", new { id = result.Data.Id });
                }

            }
            else
            {
                TempData["Success"] = result.ResultMessage;
                FormsAuthentication.SetAuthCookie(Email, true);
                BaseController.currentUser = result.Data;
                return RedirectToAction("Index", "Home");
            }
            return View();
            
        }
        [Authorize]
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            BaseController.currentUser = null;
            return RedirectToAction("Login");
        }
    }
}