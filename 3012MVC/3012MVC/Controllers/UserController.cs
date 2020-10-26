using _3012MVC.Common;
using _3012MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook;
using System.Configuration;

namespace _3012MVC.Controllers
{
    public class UserController : Controller
    {
		private Uri RedirectUri
		{
			get
			{
				var uriBuilder = new UriBuilder(Request.Url);
				uriBuilder.Query = null;
				uriBuilder.Fragment = null;
				uriBuilder.Path = Url.Action("FacebookCallback");
				return uriBuilder.Uri;
			}
		}
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
				var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password),false);
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
		 return RedirectToAction("Login", "User");
		}
		public ActionResult LoginFacebook()
		{
			var fb = new FacebookClient();
			var loginUrl = fb.GetLoginUrl(new
			{
				client_id = ConfigurationManager.AppSettings["FbAppId"],
				client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
				redirect_uri = RedirectUri.AbsoluteUri,
				response_type = "code",
				scope = "email",
			});

			return Redirect(loginUrl.AbsoluteUri);
		}
		public ActionResult FacebookCallback(string code)
		{
			var fb = new FacebookClient();
			dynamic result = fb.Post("oauth/access_token", new
			{
				client_id = ConfigurationManager.AppSettings["FbAppId"],
				client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
				redirect_uri = RedirectUri.AbsoluteUri,
				code = code
			});


			var accessToken = result.access_token;
			if (!string.IsNullOrEmpty(accessToken))
			{
				fb.AccessToken = accessToken;
				// Get the user's information, like email, first name, middle name etc
				dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
				string email = me.email;
				string userName = me.email;
				string firstname = me.first_name;
				string middlename = me.middle_name;
				string lastname = me.last_name;

				var user = new USSER();
				user.EMAIL = email;
				user.USERNAME = email;
				user.SATUS = true;
				user.NAME = firstname + " " + middlename + " " + lastname;
				user.CREATEDATE = DateTime.Now;
				var resultInsert = new userdao().InsertForFacebook(user);
				if (resultInsert > 0)
				{
					var userSession = new Userlogin();
					userSession.Name = user.USERNAME;
					userSession.UserId = user.ID;
					Session.Add(Commonconst.USER_SESSION, userSession);
				}
			}
			return Redirect("/");
		}
		public ActionResult LogOut()
		{
			Session[Commonconst.USER_SESSION] = null;
			return Redirect("/");
		}

	}
}