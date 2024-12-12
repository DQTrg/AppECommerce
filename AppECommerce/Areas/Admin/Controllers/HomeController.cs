using AppECommerce.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppECommerce.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    [AdminAuthorize]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}