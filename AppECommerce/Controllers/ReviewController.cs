using AppECommerce.Models;
using AppECommerce.Models.EF;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppECommerce.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Review
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Review(int productId)
        {
            ViewBag.ProductId = productId;
            var item = new ReviewProduct();
            if (User.Identity.IsAuthenticated)
            {
                var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var userManager = new UserManager<ApplicationUser>(userStore);
                var user = userManager.FindByName(User.Identity.Name);
                if (user != null)
                {
                    item.Email = user.Email;
                    item.FullName = user.FullName;
                    item.UserName = user.UserName;
                }
                return PartialView(item);
            }
            return PartialView();
        }
        [AllowAnonymous]
        public ActionResult LoadReview(int productId)
        {
            var item = db.Reviews.Where(x => x.ProductId == productId).OrderByDescending(x => x.Id).ToList();
            ViewBag.Count = item.Count;
            return PartialView(item);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PostReview(ReviewProduct req)
        {
            if (ModelState.IsValid)
            {
                req.CreatedDate = DateTime.Now;
                req.ModifiedDate = DateTime.Now;
                db.Reviews.Add(req);
                db.SaveChanges();
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }

        public ActionResult LichSuDonHang()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var userManager = new UserManager<ApplicationUser>(userStore);
                var user = userManager.FindByName(User.Identity.Name);
                var items = db.Orders.Where(x => x.CustomerId == user.Id).ToList();
                return PartialView(items);
            }

            return PartialView();
        }
    }
}