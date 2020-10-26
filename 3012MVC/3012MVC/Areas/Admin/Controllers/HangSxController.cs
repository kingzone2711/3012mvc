using _3012MVC.Areas.Admin.Models;
using _3012MVC.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _3012MVC.Areas.Admin.Controllers
{
    public class HangSxController : Controller
    {
        // GET: Admin/HangSx
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TimHangSX(string key, int? page)
        {
            HangSanXuaModel spm = new HangSanXuaModel();
            ViewBag.key = key;
            return PhanTrangHangSX(spm.SearchByName(key), page, null);
        }

        public ActionResult PhanTrangHangSX(IQueryable<HSANXUAT> lst, int? page, int? pagesize)
        {
            int pageSize = (pagesize ?? 10);
            int pageNumber = (page ?? 1);
            return PartialView("HangSXPartial", lst.OrderBy(m => m.TENHANG).ToPagedList(pageNumber, pageSize));
        }
    }
}