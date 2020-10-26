using _3012MVC.Common;
using _3012MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _3012MVC.Areas.Admin.Controllers
{
	[HasCredential(RoleID = "VIEW_USER")]
	public class ChartController : Controller
    {
		// GET: Admin/Chart
		Shopbanhang db = new Shopbanhang();
		public ActionResult Index()
        {
            return View();
        }
		public ActionResult GetData()
		{
			Shopbanhang db = new Shopbanhang();
			int ThanhCong = db.DONHANGs.Where(x => x.TINHTRANGDH == 3).Count();
			int DonMoi = db.DONHANGs.Where(x => x.TINHTRANGDH == 1).Count();
			int DangGiao = db.DONHANGs.Where(x => x.TINHTRANGDH == 2).Count();
			int Bihuy = db.DONHANGs.Where(x => x.TINHTRANGDH == 4).Count();
			Ratio tt = new Ratio();
			tt.ThanhCong = ThanhCong;
			tt.BiHuy = Bihuy;
			tt.DangGiao = DangGiao;
			tt.DonMoi = DonMoi;
			return Json(tt, JsonRequestBehavior.AllowGet);
		}
		public ActionResult GetHang()
		{
			int Apple = db.SPHAMs.Where(x => x.HANGSX.Contains("apple")).Count();
			int Samsung = db.SPHAMs.Where(x => x.HANGSX.Contains("samsung")).Count();
			int LG =db.SPHAMs.Where(x => x.HANGSX.Contains("LG")).Count();
			int Asus=db.SPHAMs.Where(x => x.HANGSX.Contains("asus")).Count();
			int Xiaomi= db.SPHAMs.Where(x => x.HANGSX.Contains("xiaomi")).Count();
			int Dell= db.SPHAMs.Where(x => x.HANGSX.Contains("dell")).Count();
			int Oppo= db.SPHAMs.Where(x => x.HANGSX.Contains("oppo")).Count();
			int Electronic= db.SPHAMs.Where(x => x.HANGSX.Contains("electronic")).Count();
			int Nokia= db.SPHAMs.Where(x => x.HANGSX.Contains("nokia")).Count();
			Ratio tt = new Ratio();
			tt.Apple = Apple;
			tt.SamSung = Samsung;
			tt.LG = LG;
			tt.Asus = Asus;
			tt.Xiaomi = Xiaomi;
			tt.Dell = Dell;
			tt.Oppo = Oppo;
			tt.ElecTronic = Electronic;
			tt.Nokia = Nokia;
			return Json(tt, JsonRequestBehavior.AllowGet);
		}
		public class Ratio
		{
			public int ThanhCong { get; set; }
			public int BiHuy { get; set; }
			public int DangGiao { get; set; }
			public int DonMoi { get; set; }
			public int Apple { get; set; }
			public int SamSung { get; set; }
			public int Asus { get; set; }
			public int LG { get; set; }
			public int Xiaomi { get; set; }
			public int Dell { get; set; }
			public int Oppo { get; set; }
			public int ElecTronic { get; set; }
			public int Nokia { get; set; }

		}
    }
}