using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppECommerce.Areas.Admin.Controllers
{
    public class AdvController : Controller
    {
        // GET: Admin/Adv
        public ActionResult Index()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}