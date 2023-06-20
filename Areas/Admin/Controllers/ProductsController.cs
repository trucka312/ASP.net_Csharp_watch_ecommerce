﻿using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWatchOnline.Models;
using WebWatchOnline.Models.Entity;

namespace WebWatchOnline.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
		private ApplicationDbContext dbConect = new ApplicationDbContext();
		// GET: Admin/Products
		public ActionResult Index(string Searchtext, int? page)
        {
			var pageSize = 10;
			if (page == null)
			{
				page = 1;
			}
			IEnumerable<Product> items = dbConect.Products.OrderByDescending(x => x.Id);
			if (!string.IsNullOrEmpty(Searchtext))
			{
				items = items.Where(x => x.ProductCode.Contains(Searchtext) || x.Title.Contains(Searchtext));
			}
			var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
			items = items.ToPagedList(pageIndex, pageSize);
			ViewBag.PageSize = pageSize;
			ViewBag.Page = page;
			return View(items);

		}
		public ActionResult Add()
		{
			ViewBag.ProductCategory = new SelectList(dbConect.ProductCategories.ToList(), "Id", "Title");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Add(Product model, List<string> Images, List<int> rDefault)
		{
			if(ModelState.IsValid)
			{
				if(Images != null && Images.Count > 0)
				{
					for(int i=0; i<Images.Count; i++)
					{
						if(i+1 == rDefault[0])
						{
							model.Image = Images[i];
							model.ProductImage.Add(new ProductImage
							{
								ProductId = model.Id,
								Image = Images[i],
								IsDefault = true
							});
						}
						else
						{
							model.ProductImage.Add(new ProductImage
							{
								ProductId = model.Id,
								Image = Images[i],
								IsDefault = false
							});
						}
					}
				}
				model.CreatedDate = DateTime.Now;
				model.UpdateDate = DateTime.Now;
				if (string.IsNullOrEmpty(model.SeoTitle))
				{
					model.SeoTitle = model.Title;
				}
				if (string.IsNullOrEmpty(model.Alias))
					model.Alias = WebWatchOnline.Models.Entity.Common.Filter.FilterChar(model.Title);
				dbConect.Products.Add(model);
				dbConect.SaveChanges();			
				return RedirectToAction("Index");
			}
			ViewBag.ProductCategory = new SelectList(dbConect.ProductCategories.ToList(), "Id", "Title");
			return View(model);

		}

		public ActionResult Edit(int id)
		{
			ViewBag.ProductCategory = new SelectList(dbConect.ProductCategories.ToList(), "Id", "Title");
			var item = dbConect.Products.Find(id);
			return View(item);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Product model)
		{

			if (ModelState.IsValid)
			{
				model.CreatedDate = DateTime.Now;
				model.UpdateDate = DateTime.Now;
				model.Alias = WebWatchOnline.Models.Entity.Common.Filter.FilterChar(model.Title);
				dbConect.Products.Attach(model);
				dbConect.Entry(model).State = System.Data.Entity.EntityState.Modified;
				dbConect.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(model);
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			var item = dbConect.Products.Find(id);
			if (item != null)
			{
				dbConect.Products.Remove(item);
				dbConect.SaveChanges();
				return Json(new { success = true });

			}
			return Json(new { success = false });
		}

		[HttpPost]
		public ActionResult IsActive(int id)
		{
			var item = dbConect.Products.Find(id);
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
		public ActionResult IsHome(int id)
		{
			var item = dbConect.Products.Find(id);
			if (item != null)
			{
				item.IsHome = !item.IsHome;
				dbConect.Entry(item).State = System.Data.Entity.EntityState.Modified;
				dbConect.SaveChanges();
				return Json(new { success = true, isHome = item.IsHome });

			}
			return Json(new { success = false });
		}
		[HttpPost]
		public ActionResult IsSale(int id)
		{
			var item = dbConect.Products.Find(id);
			if (item != null)
			{
				item.IsSale = !item.IsSale;
				dbConect.Entry(item).State = System.Data.Entity.EntityState.Modified;
				dbConect.SaveChanges();
				return Json(new { success = true, isSale = item.IsSale });

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
						var obj = dbConect.Products.Find(Convert.ToInt32(item));
						dbConect.Products.Remove(obj);
						dbConect.SaveChanges();
					}
				}
				return Json(new { success = true });
			}
			return Json(new { success = false });
		}
	}
}