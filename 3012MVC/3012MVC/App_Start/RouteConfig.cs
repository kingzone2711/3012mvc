using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace _3012MVC
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				namespaces: new[] { "_3012MVC.Controllers" }
			);

					routes.MapRoute(
				name: "Login",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
				namespaces: new[] { "_3012MVC.Controllers" });

			//routes.MapRoute(
			//		name: "Admin-defaut",
			//		url: "Admin/{controller}/{action}/{id}",
			//		defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional },
			//		namespaces: new[] { "_3012MVC.Areas.Admin.Controllers" }
			//	);
			//routes.MapRoute(
			//		name: "Admin",
			//		url: "Admin/{controller}/{action}/{id}",
			//		defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional },
			//		namespaces: new[] { "_3012MVC.Areas.Admin.Controllers" }
			//	);
		}
	}
}
