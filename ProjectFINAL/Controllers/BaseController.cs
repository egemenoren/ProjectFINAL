﻿using Project.Business;
using Project.Entity;
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
            var userId = -1;
            if(Session["Id"] != null)
            {

                userId = (int)Session["Id"];
            }
            var currentUser = userId != -1 ? new UserService().GetById(userId) : null;
            if (currentUser == null && this.ControllerContext.RouteData.Values["controller"].ToString().ToLower() != "auth")
            {
                Session.Abandon();
                FormsAuthentication.SignOut();
                new RedirectResult(Url.Action("Login", "Auth")).ExecuteResult(this.ControllerContext);
                requestContext.HttpContext.Response.Clear();
                requestContext.HttpContext.Response.Redirect(Url.Action("Login", "Auth"));
                requestContext.HttpContext.Response.End();
            }
        }
    }
}