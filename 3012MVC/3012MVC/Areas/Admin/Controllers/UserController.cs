using _3012MVC.Common;
using _3012MVC.Controllers;
using _3012MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace _3012MVC.Areas.Admin.Controllers
{
	//[HasCredential(RoleID = "VIEW_USER")]
	public class UserController : Controller
    {
		// GET: Admin/User
		Shopbanhang db = new Shopbanhang();
        public ActionResult Index()
        {
			var model = db.USSERs.ToList();
            return View(model);
        }
		public ActionResult loadajax()
		{
			return View();
		}
		[HttpGet]
		public JsonResult loaddata()
		{
			var listuser = db.USSERs.ToList();
			return Json(new { data = listuser },JsonRequestBehavior.AllowGet);
		}
		[HttpDelete]
		public ActionResult Delete(int id)
		{
			new userdao().Delete(id);
			return RedirectToAction("Index");
		}
		[HttpPost]
		public JsonResult savedata(string struser)
		{
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			USSER user = serializer.Deserialize<USSER>(struser);
			bool status = false;
			if(user.ID==0)
			{
				db.USSERs.Add(user);
				db.SaveChanges();
				status = true;
			}
			return Json(new
			{
				status = status
			});
		}
		[HttpGet]
		public JsonResult loaddetai(int id)
		{
			var model = db.USSERs.Find(id);
			return Json(new
			{
				data = model,
				status = true
			}, JsonRequestBehavior.AllowGet); 
		}

		[HttpPost]
		public JsonResult deleteuser(int id)
		{			
			new userdao().Delete(id);
			return Json(new
			{
				status = true
			}, JsonRequestBehavior.AllowGet);
		}
		[HttpPost]
		public JsonResult Updatepermision(List<long> id, string status)
		{
			foreach (var i in id)
			{
				new userdao().UpdateGroupid(i, status);
			}
			return Json(new
			{
				status = true
			}, JsonRequestBehavior.AllowGet);
		}

	}
}