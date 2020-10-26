using _3012MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace _3012MVC.Controllers
{
    public class QuenMatKhauController : Controller
    {
		// GET: QuenMatKhau
		Shopbanhang db = new Shopbanhang();
		public ActionResult GuiMail()
		{
			return View();
		}
		[HttpPost]
		public ActionResult GuiMail(QuenMatKhau qmk)
		{
			try
			{
				// Định cấu hình lớp webMail để gửi email  
				// máy chủ gmail smtp  
				WebMail.SmtpServer = "smtp.gmail.com";
				// cổng gmail để gửi email  
				WebMail.SmtpPort = 587;
				WebMail.SmtpUseDefaultCredentials = true;
				// gửi email với giao thức bảo mật  
				WebMail.EnableSsl = true;
				// EmailId được sử dụng để gửi email từ ứng dụng  
				WebMail.UserName = "vuongminhm10@gmail.com";
				WebMail.Password = "38c109096";

				// Địa chỉ email người gửi.  
				WebMail.From = "vuongminhm10@gmail.com";
				USSER nd = db.USSERs.FirstOrDefault(t => t.EMAIL == qmk.EmailNhan);
				qmk.ChuDe = "Xác nhận đổi mật khẩu ";
				qmk.NoiDung = "Xác nhận:'https://localhost:44348/QuenMatKhau/Thaydoimatkhau/" + nd.ID + "?Token=" + nd.Token;

				//Gửi email  
				WebMail.Send(to: qmk.EmailNhan, subject: qmk.ChuDe, body: qmk.NoiDung, cc: qmk.Cc, bcc: qmk.Bcc, isBodyHtml: true);
				ViewBag.Status = "Email được gửi thành công.";
			}
			catch (Exception)
			{
				ViewBag.Status = "Sự cố trong khi gửi email, vui lòng kiểm tra chi tiết.";
			}
			return View();
		}
		[HttpGet]
		public ActionResult Thaydoimatkhau()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Thaydoimatkhau(string matkhaumoi, string nhaplaimatkhau, int? id, string Token)
		{
			if (matkhaumoi != nhaplaimatkhau)
			{
				ViewBag.TB = "Mật khẩu không đúng! ";
				return View();
			}
			else
			{
				db.USSERs.Find(id).PASS = matkhaumoi;
				db.SaveChanges();
				return Redirect("/");
			}
		}
	}
}