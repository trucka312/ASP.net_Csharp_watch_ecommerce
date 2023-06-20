using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWatchOnline.Models;

namespace WebWatchOnline.Areas.Admin.Controllers
{
    public class SubscribeController : Controller
    {
		private ApplicationDbContext dbConect = new ApplicationDbContext();

		// GET: Admin/Posts
		public ActionResult Index()
		{
			var items = dbConect.Subscribes.ToList();
			return View(items);
		}
	}
}