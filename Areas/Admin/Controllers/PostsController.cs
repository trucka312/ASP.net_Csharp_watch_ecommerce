using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWatchOnline.Models;
using WebWatchOnline.Models.Entity;

namespace WebWatchOnline.Areas.Admin.Controllers
{
    public class PostsController : Controller
    {
		private ApplicationDbContext dbConect = new ApplicationDbContext();

		// GET: Admin/Posts
		public ActionResult Index()
        {
			var items = dbConect.Posts.ToList();
            return View(items);
        }
		public ActionResult Add()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Add(Posts model)
		{
			if (ModelState.IsValid)
			{
				model.CreatedDate = DateTime.Now;
				model.UpdateDate = DateTime.Now;
				model.CategoryId = 10;
				model.Alias = WebWatchOnline.Models.Entity.Common.Filter.FilterChar(model.Title);
				dbConect.Posts.Add(model);
				dbConect.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(model);
		}

		public ActionResult Edit(int id)
		{
			var item = dbConect.Posts.Find(id);
			return View(item);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Posts model)
		{

			if (ModelState.IsValid)
			{
				model.CreatedDate = DateTime.Now;
				model.UpdateDate = DateTime.Now;
				model.Alias = WebWatchOnline.Models.Entity.Common.Filter.FilterChar(model.Title);
				dbConect.Posts.Attach(model);
				dbConect.Entry(model).State = System.Data.Entity.EntityState.Modified;
				dbConect.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(model);
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			var item = dbConect.Posts.Find(id);
			if (item != null)
			{
				dbConect.Posts.Remove(item);
				dbConect.SaveChanges();
				return Json(new { success = true });

			}
			return Json(new { success = false });
		}

		[HttpPost]
		public ActionResult IsActive(int id)
		{
			var item = dbConect.Posts.Find(id);
			if (item != null)
			{
				item.IsActive = !item.IsActive;
				dbConect.Entry(item).State = System.Data.Entity.EntityState.Modified;
				dbConect.SaveChanges();
				return Json(new { success = true, isAcive = item.IsActive });

			}
			return Json(new { success = false });
		}

		[HttpPost]
		public ActionResult DeleteAll(string ids)
		{
			if (!string.IsNullOrEmpty(ids))
			{

				var items = ids.Split(',');
				if (items != null && items.Any())
				{
					foreach (var item in items)
					{
						var obj = dbConect.Posts.Find(Convert.ToInt32(item));
						dbConect.Posts.Remove(obj);
						dbConect.SaveChanges();
					}
				}
				return Json(new { success = true });
			}
			return Json(new { success = false });
		}
	}
}