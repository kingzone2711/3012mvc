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

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
		public ActionResult sendmail(string shipName, string mobile, string address, string email)
		{
			try {
				string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/admin/sendmail.html"));

				content = content.Replace("{{CustomerName}}", shipName);
				content = content.Replace("{{Phone}}", mobile);
				content = content.Replace("{{Email}}", email);
				content = content.Replace("{{Address}}", address);

				var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

				new MailHelper().SendMail(email, "Đơn hàng mới từ OnlineShop", content);
				new MailHelper().SendMail(toEmail, "Đơn hàng mới từ OnlineShop", content);
			}
			catch
			{
				return Redirect("About");
			}
			return Redirect("/");
			
		}
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
				return RedirectToAction("Index", "Login");
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
				return RedirectToAction("Index", "Home");
			}
			else
			{
				return RedirectToAction("Login");//xu li trang redirec
			}
			

		}
	}
}