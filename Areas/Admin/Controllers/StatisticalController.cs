using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWatchOnline.Models;

namespace WebWatchOnline.Areas.Admin.Controllers
{
    public class StatisticalController : Controller
    {
		private ApplicationDbContext db = new ApplicationDbContext();
		// GET: Admin/Statistical
		public ActionResult Index()
        {
            return View();
        }
		public ActionResult Refresh()
		{
			var item = new ThongKeModel();

			ViewBag.Visitors_online = HttpContext.Application["visitors_online"];
			var hn = HttpContext.Application["HomNay"];
			item.HomNay = HttpContext.Application["HomNay"].ToString();
			item.HomQua = HttpContext.Application["HomQua"].ToString();
			item.TuanNay = HttpContext.Application["TuanNay"].ToString();
			item.TuanTruoc = HttpContext.Application["TuanTruoc"].ToString();
			item.ThangNay = HttpContext.Application["ThangNay"].ToString();
			item.ThangTruoc = HttpContext.Application["ThangTruoc"].ToString();
			item.TatCa = HttpContext.Application["TatCa"].ToString();
			return PartialView(item);
		}
		[HttpGet]
		public ActionResult GetStatistical(string fromDate, string toDate)
		{
			var query = from o in db.Orders
						join od in db.OrderDetails
						on o.Id equals od.OrderId
						join p in db.Products
						on od.ProductId equals p.Id
						select new
						{
							CreatedDate = o.CreatedDate,
							Quantity = od.Quantity,
							Price = od.Price,
							OriginalPrice = p.OriginalPrice
						};
			if (!string.IsNullOrEmpty(fromDate))
			{
				DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
				query = query.Where(x => x.CreatedDate >= startDate);
			}
			if (!string.IsNullOrEmpty(toDate))
			{
				DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
				query = query.Where(x => x.CreatedDate < endDate);
			}

			var result = query.GroupBy(x => DbFunctions.TruncateTime(x.CreatedDate)).Select(x => new
			{
				Date = x.Key.Value,
				TotalBuy = x.Sum(y => y.Quantity * y.OriginalPrice),
				TotalSell = x.Sum(y => y.Quantity * y.Price),
			}).Select(x => new
			{
				Date = x.Date,
				DoanhThu = x.TotalSell,
				LoiNhuan = x.TotalSell - x.TotalBuy
			});
			return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
		}
	}
}