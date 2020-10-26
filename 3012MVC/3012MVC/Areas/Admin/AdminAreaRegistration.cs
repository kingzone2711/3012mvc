using System.Web.Mvc;

namespace _3012MVC.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Login", action = "Index", id = UrlParameter.Optional },
				new[] { "_3012MVC.Areas.Admin.Controllers" }
			);
			context.MapRoute(
				"Admin_Login",
				"Admin/{controller}/{action}/{id}",
				new { controller = "Login", action = "Login", id = UrlParameter.Optional },
				new[] { "_3012MVC.Areas.Admin.Controllers" }
			);
			context.MapRoute(
				"Admin_QlDonhang",
				"Admin/{controller}/{action}/{id}",
				new { controller = "DonKh", action = "Index", id = UrlParameter.Optional },
				new[] { "_3012MVC.Areas.Admin.Controllers" }
			);
			context.MapRoute(
				"Admin_QlKhuyemai",
				"Admin/{controller}/{action}/{id}",
				new { controller = "KhuyenMai", action = "Index", id = UrlParameter.Optional },
				new[] { "_3012MVC.Areas.Admin.Controllers" }
			);
		}
    }
}