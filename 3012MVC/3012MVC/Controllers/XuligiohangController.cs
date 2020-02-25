using _3012MVC.Areas.Admin.Models;
using _3012MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _3012MVC.Controllers
{
    public class XuligiohangController : Controller
    {
		// GET: Xuligiohang
		Shopbanhang db = new Shopbanhang();
		public ActionResult Addcart(string sp,int quantity)
		{
			try
			{
				var temp = db.SPHAMs.Find(sp);
				int index = Kiemtratontai(sp);
				if(index==-1)
				{
					ChiTietGioHang tam = new ChiTietGioHang();
					tam.sanpham = temp;
					tam.soluong = quantity;
					ManagerObject.getIntance().giohang.addCart(tam);
				}
				else
				{
					ManagerObject.getIntance().giohang.getGiohang()[index].soluong += quantity;
				}
				return PartialView("Cart", ManagerObject.getIntance().giohang);
			}
			catch(Exception)
			{ return Json("fail"); }
		}
		public ActionResult CartTitle()
		{
			return PartialView("Cart", ManagerObject.getIntance().giohang);
		}
		public int Kiemtratontai(string id)
		{
			for (int i = 0; i < ManagerObject.getIntance().giohang.getGiohang().Count; i++)
			{
				if (ManagerObject.getIntance().giohang.getGiohang()[i].sanpham.MASP == id)
					return i;
			}
			return -1;
		}
	}
}