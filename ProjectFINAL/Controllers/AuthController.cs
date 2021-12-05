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
    public class AuthController : Controller
    {
        private UserManager userManager;
        public AuthController()
        {
            userManager = new UserManager();
        }
        public ActionResult Login()
        {
            if (Session["UserId"] != null)
                return RedirectToAction("Index","Home");
            return View();
        }
        [HttpGet]
        public ActionResult ValidateIpAdress(int id)
        {
            if (Session["Id"] == null || Session["Id"].ToString() != id.ToString() || Session["UserId"] != null)
            {
                return RedirectToAction("Login");
            }
            var entity = userManager.GetById(id);
            if (Session["Id"] == null || Session["Id"].ToString() != id.ToString())
                return RedirectToAction("Login");
            return View(entity.Data);
        }
        [HttpPost]
        public ActionResult ValidateIpAdress(User user, string securityAnswer)
        {
            var entity = userManager.GetById(user.Id);
            if (userManager.CheckSecurityAnswer(entity.Data, securityAnswer))
            {
                userManager.SetNewIpAdress(entity.Data);
                TempData["Success"] = entity.ResultMessage;
                FormsAuthentication.SetAuthCookie(entity.Data.Mail, false);
                Session["UserId"] = entity.Data.Id;
                Session["NameSurname"] = entity.Data.Name + " " + entity.Data.Surname;
                return RedirectToAction("Index","Home");
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
                userManager.GetById(int.Parse(userId));
                var entity = userManager.GetById(id);
                if (entity.Data.IsFirstLogin == false)
                    return RedirectToAction("Index","Home");
                return View(entity.Data);
            }
        }
        [HttpPost]
        public ActionResult FirstLogin(User user)
        {
            var result = userManager.FirstLogin(user.Id, user.Password, user.SecurityAnswer);
            if (result.HasError)
            {
                TempData["ErrMsg"] = result.ResultMessage;
                return View();
            }
            TempData["Success"] = "Bilgileriniz başarıyla kaydedildi";
            FormsAuthentication.SetAuthCookie(result.Data.Mail, false);
            Session["UserId"] = result.Data.Id;
            Session["NameSurname"] = result.Data.Name + " " + result.Data.Surname;
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public ActionResult Login(string Email, string password)
        {
            var result = userManager.Login(Email, password);
            if (result.Data.IsFirstLogin == true)
            {
                Session["Id"] = result.Data.Id;
                TempData["Success"] = "İlk girişinizi başarıyla yaptınız. Bu alanda güvenlik için şifrenizi ve güvenlik sorunuzun cevabını belirleyin.";
                return RedirectToAction("Firstlogin", new { id = result.Data.Id });

            }
            if (result.HasError)
            {
                ViewBag.ErrMsg = result.ResultMessage;
                if (result.ResultCode == Project.Business.Middleware.ServiceResultCode.DifferentIPException)
                {
                    Session["Id"] = result.Data.Id;
                    return RedirectToAction("ValidateIpAdress", new { id = result.Data.Id });
                }

                return View();
            }
            TempData["Success"] = result.ResultMessage;
            FormsAuthentication.SetAuthCookie(Email, false);
            Session["UserId"] = result.Data.Id;
            Session["NameSurname"] = result.Data.Name + " " + result.Data.Surname;
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}