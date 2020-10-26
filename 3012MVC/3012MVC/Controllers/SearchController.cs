using _3012MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using _3012MVC.Areas.Admin.Models;

namespace _3012MVC.Controllers
{
	
	public class SearchController : Controller
    {
		// GET: Seach
		
        public ActionResult Search()
        {
            return PartialView("_SearchFormPartial");
        }
		[HttpPost]
		public JsonResult SeachWithname(string name)
		{
			SanphamModel list = new SanphamModel();
			IQueryable<SPHAM> kq = list.Searchwithname(name);
			var js = (from p in kq orderby p.MASP descending select new { p.MASP, p.TENSP, p.ANHDAIDIEN, p.GIA }).Take(5);
			return Json(js, JsonRequestBehavior.AllowGet);

		}
		public ActionResult CategoryList()
		{
			Loaispmodel lsp = new Loaispmodel();
			var cart = lsp.getsanpham().ToList();
			return PartialView("_CategoryListPartial", cart);
		}
		public ActionResult Addvendsearch(string ten,string loai,string hangsx,int? max,int? min)
		{
			ViewBag.Theoten = ten;
			ViewBag.loai = loai;
			ViewBag.hangsx = hangsx;
			ViewBag.Giamax = max;
			ViewBag.Giamin = min;
			return View("Addvendsearch");
		}
		
		public ActionResult Addvendsearchpt(string ten, string loai, string hangsx, int? max, int? min,int? page,string typeview)
		{
			ViewBag.Theoten = ten;
			ViewBag.Theoloai = loai;
			ViewBag.Theohang = hangsx;
			ViewBag.Giamax = max;
			ViewBag.Giamin = min;
			ViewBag.type = typeview;
			SanphamModel list = new SanphamModel();
			IQueryable<SPHAM> sp = list.Searchaddvend(ten, loai, hangsx, max, min);
			return Phantrang(sp,page);
		}
		public ActionResult Phantrang(IQueryable<SPHAM>sp,int? page)
		{
			int pageSize = 6;
			int pageNumber =(page ?? 1);
			sp = sp.OrderByDescending(x => x.MASP);
			return View("_AdvancedSearchPartial", sp.ToPagedList(pageNumber, pageSize));
		}
	}
}