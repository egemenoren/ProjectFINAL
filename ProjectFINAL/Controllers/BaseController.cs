using Project.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace ProjectFINAL.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public BaseController()
        {
   
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session["User"] == null && this.ControllerContext.RouteData.Values["controller"].ToString().ToLower() != "auth")
            {
                Session.Abandon();
                FormsAuthentication.SignOut();
                new RedirectResult(Url.Action("Login", "Auth")).ExecuteResult(this.ControllerContext);
            }
        }
    }
}