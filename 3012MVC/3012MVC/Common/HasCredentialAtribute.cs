﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Core.Common;
using System.Web.Routing;

namespace _3012MVC.Common
{
	public class HasCredentialAttribute : AuthorizeAttribute
	{
		public string RoleID { set; get; }
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			var session = (Userlogin)HttpContext.Current.Session[Commonconst.USER_SESSION];
			if (session == null)
			{
				return false;
			}

			List<string> privilegeLevels = this.GetCredentialByLoggedInUser(session.Name); // Call another method to get rights of the user from DB

			if (privilegeLevels.Contains(this.RoleID) || session.Groupid == Commonuser.ADMIN)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		//protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		//{
		//	filterContext.Result = new ViewResult
		//	{
		//		ViewName = "~/Areas/Admin/Views/Shared/401.cshtml"
		//	};
		//}
		private List<string> GetCredentialByLoggedInUser(string userName)
		{
			var credentials = (List<string>)HttpContext.Current.Session[Commonconst.SESSION_CREDENTIALS];
			return credentials;
		}
	}
}