using _3012MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3012MVC.Areas.Admin.Models
{
	public class LoaiSanPhamModel
	{
		Shopbanhang db = new Shopbanhang();
		public IQueryable<LSANPHAM> GetCategory()
		{
			IQueryable<LSANPHAM> lst = db.LSANPHAMs;
			return lst;
		}
        internal string ThemLoaiSP(LSANPHAM loai)
        {
            loai.MALOAI = TaoMa();
            db.LSANPHAMs.Add(loai);
            db.SaveChanges();
            return loai.MALOAI;
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
            var temp = db.LSANPHAMs.Find(maID);
            if (temp == null)
                return true;
            return false;
        }

        internal IQueryable<LSANPHAM> SearchByName(string key)
        {
            if (string.IsNullOrEmpty(key))
                return db.LSANPHAMs;
            return db.LSANPHAMs.Where(u => u.TENLOAI.Contains(key));
        }
    }
}