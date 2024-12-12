using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppECommerce.Common
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var area = filterContext.Controller.ControllerContext.RouteData.Values["area"]?.ToString();

            if (area == "Admin")
            {
                // Redirect to Admin login page for Admin area
                filterContext.Result = new RedirectResult("~/Admin/Account/Login");
            }
            else
            {
                // Redirect to General login page for non-Admin area
                filterContext.Result = new RedirectResult("~/Account/Login");
            }
        }
    }

}