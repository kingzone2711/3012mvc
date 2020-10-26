using _3012MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace _3012MVC.Areas.Admin.Models
{
	public class KhuyenMaiModel
	{
		private Shopbanhang db = new Shopbanhang();
        internal KMAI FindById(string id)
        {
            return db.KMAIs.Find(id);
        }

        internal void EditKhuyenMai(KMAI loai)
        {
            KMAI lsp = db.KMAIs.Find(loai.MAKM);

            lsp.NGAYBATDAU = loai.NGAYBATDAU;
            lsp.NGAYKETTHUC = loai.NGAYKETTHUC;
            lsp.NOIDUNG = loai.NOIDUNG;
            db.Entry(lsp).State = EntityState.Modified;
            db.SaveChanges();
        }


        internal string ThemKhuyenMai(KMAI loai)
        {
            loai.MAKM = TaoMa();
            loai.ANHCT = loai.MAKM + "1.jpg";
            db.KMAIs.Add(loai);
            db.SaveChanges();
            return loai.MAKM;
        }

        private string TaoMa()
        {
            string maID;
            Random rand = new Random();
            do
            {
                maID = "";
                for (int i = 0; i < 5; i++)
                {
                    maID += rand.Next(9);
                }
            }
            while (!KiemtraID(maID));
            return maID;
        }

        private bool KiemtraID(string maID)
        {
            var temp = db.KMAIs.Find(maID);
            if (temp == null)
                return true;
            return false;
        }

        internal IQueryable<KMAI> TimKhuyenMai(string key, DateTime? start, DateTime? end)
        {
            IQueryable<KMAI> lst = db.KMAIs;
            if (!string.IsNullOrEmpty(key))
                lst = db.KMAIs.Where(u => u.TENCT.Contains(key));
            if (start != null)
                lst = db.KMAIs.Where(u => u.NGAYBATDAU >= start);
            if (end != null)
                lst = db.KMAIs.Where(u => u.NGAYKETTHUC <= end);
            return lst;
        }
        internal bool KiemTraTen(string key)
        {
            var temp = db.KMAIs.Where(m => m.TENCT.Equals(key)).ToList();
            if (temp.Count == 0)
                return true;
            return false;
        }


    }
}