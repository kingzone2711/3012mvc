using _3012MVC.Common;
using _3012MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _3012MVC.Controllers
{
    public class UserController : Controller
    {
        // GET: Userlogin
        public ActionResult Login()
        {
            return View();
        }
		[HttpPost]
		public ActionResult Login(Login model)
		{
			if (ModelState.IsValid)
			{
				var dao = new userdao();
				var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password), true);
				if (result == 1)
				{
					var user = dao.getbyid(model.UserName);
					var userSession = new Userlogin();
					userSession.Name = user.USERNAME;
					userSession.UserId = user.ID;
					userSession.Groupid = user.GROUPID;
					userSession.Phone = user.PHONE;
					userSession.Address = user.ADDRESS;
					var listCredentials = dao.getcredential(model.UserName);

					Session.Add(Commonconst.SESSION_CREDENTIALS, listCredentials);
					Session.Add(Commonconst.USER_SESSION, userSession);
					return RedirectToAction("Index", "Home");
				}
				else if (result == 0)
				{
					ModelState.AddModelError("", "Tài khoản không tồn tại.");
				}
				else if (result == -1)
				{
					ModelState.AddModelError("", "Tài khoản đang bị khoá.");
				}
				else if (result == -2)
				{
					ModelState.AddModelError("", "Mật khẩu không đúng.");
				}
				else if (result == -3)
				{
					ModelState.AddModelError("", "Tài khoản của bạn không có quyền đăng nhập.");
				}
				else
				{
					ModelState.AddModelError("", "đăng nhập không đúng.");
				}
			}
			return View("/");
		}
		public ActionResult LogOut()
		{
			Session[Commonconst.USER_SESSION] = null;
			return Redirect("/");
		}

	}
}