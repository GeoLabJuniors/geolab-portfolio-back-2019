using GeolabPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GeolabPortfolio.Filters
{
    public class AdminLoginFilter:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            User user = (User)context.HttpContext.Session["Login-User"];

            if (user == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary { { "controller", "Account" }, { "action", "Login" } });
            }
        }
    }
}