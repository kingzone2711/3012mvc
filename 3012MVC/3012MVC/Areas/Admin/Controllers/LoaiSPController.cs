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
    public class LoaiSPController : Controller
    {
        // GET: Admin/LoaiSP
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TimLoaiSP(string key, int? page)
        {
            LoaiSanPhamModel spm = new LoaiSanPhamModel();
            ViewBag.key = key;
            return PhanTrangSP(spm.SearchByName(key), page, null);
        }

        public ActionResult PhanTrangSP(IQueryable<LSANPHAM> lst, int? page, int? pagesize)
        {
            int pageSize = (pagesize ?? 10);
            int pageNumber = (page ?? 1);
            return PartialView("LoaiSPPartial", lst.OrderBy(m => m.TENLOAI).ToPagedList(pageNumber, pageSize));
        }
    }
}