using _3012MVC.Common;
using _3012MVC.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _3012MVC.Areas.Admin.Controllers
{
	[HasCredential(RoleID = "VIEW_USER")]
	public class DonKhController : Controller
    {
        // GET: Admin/DonKh
        public ActionResult Index()
        {
            return View();
        }
		
		public ActionResult Timdonhang(string key,string phone,DateTime? date,int? status,int? page)
		{
			
			DonHangModel dh = new DonHangModel();
			ViewBag.Key = key;
			ViewBag.Phone = phone;
			ViewBag.Time = date;
			ViewBag.status = status;
			return PhanTrangDH(dh.Timdonhang(key,phone,date,status),page,null);
		}
		public ActionResult gettenkhach(long ten)
		{
			var name = Gettenkh(ten);
			return PartialView("Laynamekhachhang",name);
		}
		public IQueryable<USSER> Gettenkh(long makh)
		{
			Shopbanhang db = new Shopbanhang();
			IQueryable<USSER> tenkhach = db.USSERs;
			tenkhach = from p in tenkhach where p.ID==makh select p;
			return tenkhach;
		}
		public ActionResult PhanTrangDH(IQueryable<DONHANG> lst, int? page, int? pagesize)
		{
		
			int pageSize = (pagesize ?? 4);
			int pageNumber = (page ?? 1);
			return PartialView("~/Areas/Admin/Views/DonKh/_DonKhpartial.cshtml", lst.OrderBy(m => m.NGAYDATMUA).ToPagedList(pageNumber, pageSize));//fail when juist -donkhpatial
		}
		[HttpPost]
		public ActionResult Updatetinhtrang(string madh,int? tt)
		{
			DonHangModel dh = new DonHangModel();
			dh.Updatetinhtrangdh(madh, tt);
			return RedirectToAction("Timdonhang");
		}
		[HttpPost]
		public ActionResult MultibleUpdate(List<string> lst, int? tt)
		{
			foreach (var item in lst)
			{
				Updatetinhtrang(item, tt);
			}
			return RedirectToAction("Timdonhang");
		}
		public ActionResult DonHangDetail(string madh)
		{
			DonHangModel dh = new DonHangModel();
			var model = dh.Chitietdonhang(madh);
			return View("DonHangDetail", model);
		}
		[HttpGet]
		public ActionResult ChiTietDonHang(string madh)
		{
			DonHangModel dh = new DonHangModel();

			return PartialView("_DonHangDetail", dh.Chitietdonhang(madh));
		}
	}
}