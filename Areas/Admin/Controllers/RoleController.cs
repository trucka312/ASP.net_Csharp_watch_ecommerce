using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWatchOnline.Models;

namespace WebWatchOnline.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	public class RoleController : Controller
    {
		private ApplicationDbContext dbConect = new ApplicationDbContext();
		// GET: Admin/Role
		public ActionResult Index()
        {
			var items = dbConect.Roles.ToList();
			return View(items);
			
        }
		public ActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IdentityRole model)
		{
			if (ModelState.IsValid)
			{
				var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbConect));
				roleManager.Create(model);
				return RedirectToAction("Index");
			}
			return View(model);
		}
		public ActionResult Edit(string id)
		{
			var item = dbConect.Roles.Find(id);
			return View(item);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(IdentityRole model)
		{
			if (ModelState.IsValid)
			{
				var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbConect));
				roleManager.Update(model);
				return RedirectToAction("Index");
			}
			return View(model);
		}
		[HttpPost]
		public ActionResult Delete(string id)
		{
			var item = dbConect.Roles.Find(id);
			if (item != null)
			{
				dbConect.Roles.Remove(item);
				dbConect.SaveChanges();
				return Json(new { success = true });

			}
			return Json(new { success = false });
		}
	}
}