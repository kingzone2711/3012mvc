using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Common;
using _3012MVC.Areas.Admin.Models;
using _3012MVC.Models;
using _3012MVC.Common;


namespace _3012MVC.Controllers
{
	public class HomeController : Controller
	{
	
		public ActionResult Index()
		{
			return View();
		}

		
		public ActionResult sendmail(string shipName, string mobile, string address, string email)
		{
			try {
				string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/SendEmail.html"));

				content = content.Replace("{{CustomerName}}", shipName);
				content = content.Replace("{{Phone}}", mobile);
				content = content.Replace("{{Email}}", email);
				content = content.Replace("{{Address}}", address);

				var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

				new MailHelper().SendMail(email, "Đơn hàng mới từ cửa hàng thiết bị điện tử", content);
				new MailHelper().SendMail(toEmail, "Đơn hàng mới từ cửa hàng thiết bị điện tử", content);
			}
			catch
			{
				return Redirect("view");
			}
			return Redirect("/");
			
		}
		
		public ActionResult Xemdonhang()
		{
			List<DonHangModel> dh = new List<DonHangModel>();
			var userid = (Userlogin)Session[Commonconst.USER_SESSION];
			long makh = userid.UserId;
			if (makh>0)
			{

				DonHangModel temp = new DonHangModel();
				dh = temp.xemdonhang(makh);
			}
			else
			{
				return Redirect("/");
				
			}
			return View(dh);
		}
		[HttpPost]
		public ActionResult Huydonhang(string maDH)
		{
			DonHangModel dh = new DonHangModel();
			dh.HuyDH(maDH);
			var userid = (Userlogin)Session[Commonconst.USER_SESSION];
			long makh = userid.UserId;
			var item= dh.xemdonhang(makh);
			return RedirectToAction("Xemdonhang");
		}
		//send email
		public ActionResult view()
		{
			return View();
		}
		public ActionResult Sanphamnoibat(int? skip)
		{
			SanphamModel sp = new SanphamModel();
			int skipnum = (skip ?? 0);
			IQueryable<SPHAM> splist = sp.Getsp();
			splist = splist.OrderBy(r => r.MASP).Skip(skipnum).Take(8);
			if (splist.Any())
			{
				return PartialView("_ProductTabLoadMorePartial", splist);
			}
			else
				return null;
		}
		public ActionResult Sanphammoinhat(int? skip)
		{
			SanphamModel sp = new SanphamModel();
			int skipnum = (skip ?? 0);
			IQueryable<SPHAM> splist = sp.SPMoiNhap();
			splist = splist.OrderBy(r => r.MASP).Skip(skipnum).Take(8);
			if (splist.Any())
			{
				return PartialView("_ProductTabLoadMorePartial", splist);
			}
			else
				return null;
		}
		public ActionResult Cart()
		{
			return View(ManagerObject.getIntance().giohang);

		 }
		public ActionResult checkout()
		{
			if(Session.Count>0)
			{
				
				DonHangModel dh = new DonHangModel();
				var name =(Userlogin)Session[Commonconst.USER_SESSION];
				DonHangTongQuan dhtq = new DonHangTongQuan()
				{
					buyer =name.Name,
					address=name.Address,
					phoneNumber=name.Phone
					
				};
				return View(dhtq);
			}
			else
			{
				return RedirectToAction("Login", "User");
			}
		}
		[HttpPost]
		public ActionResult checkout(DonHangTongQuan dhtq)
		{
			if (Session.Count > 0)
			{
				DonHangModel dh = new DonHangModel();
				var userid = (Userlogin)Session[Commonconst.USER_SESSION];
				var id = userid.UserId;
				dh.Luudonhang(dhtq, id,ManagerObject.getIntance().giohang);
				return RedirectToAction("Xemdonhang", "Home");
			}
			else
			{
				return RedirectToAction("Login","User");//xu li trang redirec
			}
		}
		public ActionResult EditUser()
		{
			Shopbanhang db = new Shopbanhang();
			if (Session.Count > 0)
			{
				var userid = (Userlogin)Session[Commonconst.USER_SESSION];
				var id = userid.UserId;
				var user = db.USSERs.Find(id);
				return View(user);
			}
			return RedirectToAction("Login", "User");
		}
		[HttpPost]
		public ActionResult UpdateUser(USSER user)
		{
			Shopbanhang db = new Shopbanhang();
			if (ModelState.IsValid)
			{
				try
				{
					var edituser = db.USSERs.Find(user.ID);
					edituser.USERNAME = user.USERNAME;
					edituser.NAME = user.NAME;
					edituser.ADDRESS = user.ADDRESS;
					edituser.EMAIL = user.EMAIL;
					edituser.PHONE = user.PHONE;
					edituser.MODDIFIEDDATE = DateTime.Now;
					edituser.CREATEBY = "User";
					db.SaveChanges();
					return View("EditUser");
				}
				catch
				{
					ModelState.AddModelError("", "Cập nhật user không thành công");

				}
				
			}
			return View("EditUser");
		}
		public ActionResult MainMenu()
		{
			MainMenuModel mnmodel = new MainMenuModel();
			var menulist = mnmodel.GetMenuList();
			return PartialView("_MainMenuPartial", menulist);
		}
	}
}