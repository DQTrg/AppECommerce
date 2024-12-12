using AppECommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppECommerce.Areas.Admin.Controllers
{
    public class StatisticalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Statistical
        public ActionResult Index()
        {
            return View();
        }
        //[HttpGet]
        //public ActionResult GetStatistical(string fromDate, string toDate)
        //{
        //    var query = from o in db.Orders
        //                join od in db.OrderDetails
        //                on o.Id equals od.OrderId
        //                join p in db.Products
        //                on od.ProductId equals p.Id
        //                select new
        //                {
        //                    CreatedDate = o.CreatedDate,
        //                    Quantity = od.Quantity,
        //                    Price = od.Price,
        //                    OriginalPrice = p.OriginalPrice
        //                };
        //    if (!string.IsNullOrEmpty(fromDate))
        //    {
        //        DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
        //        query = query.Where(x => x.CreatedDate >= startDate);
        //    }
        //    if (!string.IsNullOrEmpty(toDate))
        //    {
        //        DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
        //        query = query.Where(x => x.CreatedDate < endDate);
        //    }

        //    var result = query.GroupBy(x => DbFunctions.TruncateTime(x.CreatedDate)).Select(x => new
        //    {
        //        Date = x.Key.Value,
        //        TotalBuy = x.Sum(y => y.Quantity * y.OriginalPrice),
        //        TotalSell = x.Sum(y => y.Quantity * y.Price),
        //    }).Select(x => new
        //    {
        //        Date = x.Date,
        //        DoanhThu = x.TotalSell,
        //        LoiNhuan = x.TotalSell - x.TotalBuy
        //    });
        //    return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        //}
        [HttpGet]
        public ActionResult GetStatistical(string fromDate, string toDate)
        {
            const string dateFormat = "dd/MM/yyyy"; // Avoid magic strings
            DateTime? startDate = string.IsNullOrEmpty(fromDate)
                ? (DateTime?)null
                : DateTime.ParseExact(fromDate, dateFormat, null);
            DateTime? endDate = string.IsNullOrEmpty(toDate)
                ? (DateTime?)null
                : DateTime.ParseExact(toDate, dateFormat, null);

            var query = db.Orders
                          .Join(db.OrderDetails, o => o.Id, od => od.OrderId, (o, od) => new { o, od })
                          .Join(db.Products, temp => temp.od.ProductId, p => p.Id, (temp, p) => new
                          {
                              temp.o.CreatedDate,
                              temp.od.Quantity,
                              temp.od.Price,
                              p.OriginalPrice,
                              p.OriginalQuantity
                          });

            if (startDate.HasValue)
                query = query.Where(x => x.CreatedDate >= startDate.Value);
            if (endDate.HasValue)
                query = query.Where(x => x.CreatedDate < endDate.Value);

            var result = query.GroupBy(x => DbFunctions.TruncateTime(x.CreatedDate))
                              .Select(g => new
                              {
                                  Date = g.Key.Value,
                                  TotalBuy = g.Sum(item => item.OriginalQuantity * item.OriginalPrice),
                                  TotalSell = g.Sum(item => item.Quantity * item.Price)
                              })
                              .Select(summary => new
                              {
                                  Date = summary.Date,
                                  DoanhThu = summary.TotalSell,
                                  LoiNhuan = summary.TotalSell - summary.TotalBuy
                              })
                              .ToList(); // Execute the query at this point to improve efficiency

            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }

    }
}