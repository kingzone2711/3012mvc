using _3012MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using _3012MVC.Areas.Admin.Models;

namespace _3012MVC.Areas.Admin.Controllers
{
	
	public class SanphamController : Controller
    {
		
		Shopbanhang db = new Shopbanhang();
		public ActionResult Loadsanpham()
		{
			return View();
		}
		[HttpGet]
		public JsonResult sanpham()
		{
			SanphamModel sanpham = new SanphamModel();
			var model = sanpham.Getsp();

			return this.Json(
			  new
			  {
				 data = (from obj in db.SPHAMs select new { MASP = obj.MASP, TENSP = obj.TENSP,
					 LOAISP=obj.LOAISP,
					 HANGSX=obj.HANGSX,
					 GIA= obj.GIA,
					 SOLUONG=obj.SOLUONG,
					 MOTA=obj.MOTA,
					 ANHDAIDIEN=obj.ANHDAIDIEN
				 })
			  }
			  , JsonRequestBehavior.AllowGet
			  );

		}
		[HttpPost]
		public JsonResult deletesanpham(string MASP)
		{
			new SanphamModel().deletesp(MASP);
			return Json(new
			{
				status = true
			}, JsonRequestBehavior.AllowGet);
		}
		public ActionResult themsp()
		{
			 SanphamModel sp = new SanphamModel();
			ViewBag.HANGSX = new SelectList(sp.Gethangsanxuat(), "HANGSX", "TENHANG ");
			ViewBag.LOAISP = new SelectList(sp.Getloaisp(), "MALOAI", "TENLOAI");
			return View();
		}
		public bool UploadAnh(HttpPostedFileBase file, string tenfile)
		{
			// Verify that the user selected a file
			if (file != null && file.ContentLength > 0)
			{
				var name = Path.GetExtension(file.FileName);
				// extract only the filename
				if (!Path.GetExtension(file.FileName).Equals(".jpg"))
				{
					return false;
				}
				// store the file inside ~/App_Data/uploads folder
				var path = Path.Combine(Server.MapPath("~/images/products"), tenfile + ".jpg");
				file.SaveAs(path);
				return true;
			}
			// redirect back to the index action to show the form once again
			return false;
		}
		[HttpPost]
		public ActionResult Addsanpham([Bind(Include = "TENSP,LOAISP,HANGSX,GIA,MOTA,SOLUONG,ISNEW")] SPHAM sanpham, HttpPostedFileBase an)
		{
			SanphamModel spm = new SanphamModel();
			if(ModelState.IsValid){
				string masp = spm.themsp(sanpham);
				UploadAnh(an, masp + "1");//waring masp+1 not get picture up to list 
			}
			ViewBag.HANGSX = new SelectList(spm.Gethangsanxuat(), "HANGSX", "TENHANG ");
			ViewBag.LOAISP = new SelectList(spm.Getloaisp(), "MALOAI", "TENLOAI");
			return View("themsp",sanpham);
		}
	}
}