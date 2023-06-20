using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWatchOnline.Models;
using WebWatchOnline.Models.Entity;

namespace WebWatchOnline.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
		private ApplicationDbContext dbConect = new ApplicationDbContext();
		// GET: Admin/Category
		public ActionResult Index()
        {
            var item = dbConect.Categories;
            return View(item);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category model)
        {
            if(ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                model.Alias = WebWatchOnline.Models.Entity.Common.Filter.FilterChar(model.Title);
				dbConect.Categories.Add(model);
                dbConect.SaveChanges();
				return RedirectToAction("Index");

			}
            return View(model);
		}
        public ActionResult Edit(int id)
        {
            var item = dbConect.Categories.Find(id);
            return View(item);
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Category model)
		{
			if (ModelState.IsValid)
			{
                dbConect.Categories.Attach(model);			
				model.UpdateDate = DateTime.Now;
				model.Alias = WebWatchOnline.Models.Entity.Common.Filter.FilterChar(model.Title);
                dbConect.Entry(model).Property(x=>x.Title).IsModified = true;
				dbConect.Entry(model).Property(x => x.Description).IsModified = true;
				dbConect.Entry(model).Property(x => x.Alias).IsModified = true;
				dbConect.Entry(model).Property(x => x.SeoDescription).IsModified = true;
				dbConect.Entry(model).Property(x => x.SeoKeywords).IsModified = true;
				dbConect.Entry(model).Property(x => x.SeoTitle).IsModified = true;
				dbConect.Entry(model).Property(x => x.Position).IsModified = true;
				dbConect.Entry(model).Property(x => x.UpdateDate).IsModified = true;
				dbConect.Entry(model).Property(x => x.UpdateBy).IsModified = true;

				dbConect.SaveChanges();
				return RedirectToAction("Index");

			}
			return View(model);
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			var item = dbConect.Categories.Find(id);
			if(item != null)
			{
				//var DeleteItem = dbConect.Categories.Attach(item);
				dbConect.Categories.Remove(item);
				dbConect.SaveChanges();
				return Json(new { success = true });
			}
			return Json(new { success = false });
		}
	}
}