using _3012MVC.Areas.Admin.Models;
using _3012MVC.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace _3012MVC.Areas.Admin.Controllers
{
    public class KhuyenMaiController : Controller
    {
        // GET: Admin/KhuyenMai
        public ActionResult Index()
        {
            return View();
        }
        public bool UploadAnh(HttpPostedFileBase file, string tenfile)
        {
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                var name = Path.GetExtension(file.FileName);
                // extract only the filename
                if (!Path.GetExtension(file.FileName).Equals(".jpg"))
                {
                    return false;
                }
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/images/khuyenmai"), tenfile + ".jpg");
                file.SaveAs(path);
                return true;
            }
            // redirect back to the index action to show the form once again
            return false;
        }

        public bool DeleteAnh(string filename)
        {
            string fullPath = Request.MapPath("~/images/khuyenmai/" + filename);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                return true;
            }
            return false;
        }
        public ActionResult ThemKhuyenMai()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemKhuyenMai([Bind(Include = "TENCT,NGAYBATDAU,NGAYKETTHUC,NOIDUNG")] KMAI loai, HttpPostedFileBase ad)
        {
            KhuyenMaiModel spm = new KhuyenMaiModel();
            if (ModelState.IsValid && spm.KiemTraTen(loai.TENCT))
            {
                string makm = spm.ThemKhuyenMai(loai);
                UploadAnh(ad, makm + "1");
            }
            return View("Index", loai);
        }
        public ActionResult EditKhuyenMai(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhuyenMaiModel lm = new KhuyenMaiModel();
            KMAI sp = lm.FindById(id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditKhuyenMai([Bind(Include = "MAKM,TENCT,NGAYBATDAU,NGAYKETTHUC,NOIDUNG")] KMAI loai, HttpPostedFileBase ad)
        {
            KhuyenMaiModel spm = new KhuyenMaiModel();
            if (ModelState.IsValid)
            {
                spm.EditKhuyenMai(loai);
                UploadAnh(ad, loai.MAKM + "1");
                return RedirectToAction("Index");
            }
            return View(loai);
        }
        public ActionResult TimKhuyenMai(string key, DateTime? start, DateTime? end, int? page)
        {
            KhuyenMaiModel spm = new KhuyenMaiModel();
            ViewBag.key = key;
            ViewBag.start = start;
            ViewBag.end = end;
            return PhanTrangKhuyenMai(spm.TimKhuyenMai(key, start, end), page, null);
        }

        public ActionResult PhanTrangKhuyenMai(IQueryable<KMAI> lst, int? page, int? pagesize)
        {
            int pageSize = (pagesize ?? 10);
            int pageNumber = (page ?? 1);
            return PartialView("~/Areas/Admin/Views/KhuyenMai/_KhuyenMaiPartial.cshtml", lst.OrderBy(m => m.NGAYBATDAU).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult SlideShowView()
        {
            KhuyenMaiModel km = new KhuyenMaiModel();
            return PartialView("~/Areas/Admin/Views/KhuyenMai/_SlideShowView.cshtml", km.TimKhuyenMai(null, null, null).Where(m => m.NGAYBATDAU <= DateTime.Today && m.NGAYKETTHUC >= DateTime.Today));
        }
        //[AllowAnonymous]
        //public ActionResult KhuyenMaiPost(string id)
        //{
        //    KhuyenMaiModel km = new KhuyenMaiModel();
        //    return View("KhuyenMaiPostView", km.FindById(id));
        //}
    }
}