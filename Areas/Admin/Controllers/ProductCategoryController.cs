using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebWatchOnline.Models;
using WebWatchOnline.Models.Entity;

namespace WebWatchOnline.Areas.Admin.Controllers
{
    public class ProductCategoryController : Controller
    {
		private ApplicationDbContext dbConect = new ApplicationDbContext();
		// GET: Admin/ProductCategory
		public ActionResult Index()
        {
			var items = dbConect.ProductCategories.ToList();
			return View(items);
		}
        public ActionResult Add()
        {
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Add(ProductCategory model)
		{
			if (ModelState.IsValid)
			{
				model.CreatedDate = DateTime.Now;
				model.UpdateDate = DateTime.Now;
				model.Alias = WebWatchOnline.Models.Entity.Common.Filter.FilterChar(model.Title);
				dbConect.ProductCategories.Add(model);
				dbConect.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(model);
		}
		public ActionResult Edit(int id)
		{
			var item = dbConect.ProductCategories.Find(id);
			return View(item);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(ProductCategory model)
		{

			if (ModelState.IsValid)
			{
				model.CreatedDate = DateTime.Now;
				model.UpdateDate = DateTime.Now;
				model.Alias = WebWatchOnline.Models.Entity.Common.Filter.FilterChar(model.Title);
				dbConect.ProductCategories.Attach(model);
				dbConect.Entry(model).State = System.Data.Entity.EntityState.Modified;
				dbConect.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(model);
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			var item = dbConect.ProductCategories.Find(id);
			if (item != null)
			{
				dbConect.ProductCategories.Remove(item);
				dbConect.SaveChanges();
				return Json(new { success = true });

			}
			return Json(new { success = false });
		}

		
	}
}