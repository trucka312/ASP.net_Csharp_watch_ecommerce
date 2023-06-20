using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWatchOnline.Models;
using WebWatchOnline.Models.Entity;

namespace WebWatchOnline.Controllers
{
    public class ProductsController : Controller
    {
		private ApplicationDbContext dbContext = new ApplicationDbContext();
		// GET: Products
		//public ActionResult Index(string Searchtext, int? page)
		//{
		//	var pageSize = 10;
		//	if (page == null)
		//	{
		//		page = 1;
		//	}
		//	IEnumerable<Product> items = dbConect.Products.OrderByDescending(x => x.Id);
		//	if (!string.IsNullOrEmpty(Searchtext))
		//	{
		//		items = items.Where(x => x.ProductCode.Contains(Searchtext) || x.Title.Contains(Searchtext));
		//	}
		//	var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
		//	items = items.ToPagedList(pageIndex, pageSize);
		//	ViewBag.PageSize = pageSize;
		//	ViewBag.Page = page;
		//	return View(items);

		//}
		public ActionResult Index(string Searchtext, int? page)
        {
			var pageSize = 10;
			if (page == null)
			{
				page = 1;
			}
			IEnumerable<Product> items = dbContext.Products.OrderByDescending(x => x.Id);
			if (!string.IsNullOrEmpty(Searchtext))
			{
				items = items.Where(x => x.ProductCode.Contains(Searchtext) || x.Title.Contains(Searchtext));
			}
			var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
			items = items.ToPagedList(pageIndex, pageSize);
			ViewBag.PageSize = pageSize;
			ViewBag.Page = page;
			return View(items);
			//var items = dbContext.Products.ToList();		

			//         return View(items);
		}
		public ActionResult ProductCategory(string alias, int? id)
		{
			var items = dbContext.Products.ToList();
			if (id > 0)
			{
				items = items.Where(x => x.ProductCategoryId == id).ToList();
			}
			var category = dbContext.ProductCategories.Find(id);
			if(category != null)
			{
				ViewBag.CategoryName = category.Title;
			}
			ViewBag.CategoryId = id;
			return View(items);
		}
		public ActionResult Detail(string alias, int id)
		{
			var item = dbContext.Products.Find(id);
			if(item != null)
			{
				//lượt xem
				dbContext.Products.Attach(item);
				item.ViewCount = item.ViewCount + 1;
				dbContext.Entry(item).Property(x=>x.ViewCount).IsModified = true;				
				dbContext.SaveChanges();
			}
			return View(item);
		}

		public ActionResult Partial_ItemByCategoryId()
        {
			var items = dbContext.Products.Where(x => x.IsHome && x.IsActive).Take(12).ToList();
			return PartialView(items);
		}

		public ActionResult Partial_ProductSales()
		{
			var items = dbContext.Products.Where(x => x.IsSale && x.IsActive).Take(12).ToList();
			return PartialView(items);
		}
	}
}