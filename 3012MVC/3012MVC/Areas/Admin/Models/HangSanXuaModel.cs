using _3012MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3012MVC.Areas.Admin.Models
{
	
	public class HangSanXuaModel
	{
		Shopbanhang db = new Shopbanhang();
		public IQueryable<HSANXUAT> GetHangSX()
		{
			IQueryable<HSANXUAT> lst = db.HSANXUATs;
			return lst;
		}
        internal string ThemHangSX(HSANXUAT loai)
        {
            loai.HANGSX = TaoMa();
            db.HSANXUATs.Add(loai);
            db.SaveChanges();
            return loai.HANGSX;
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
            var temp = db.HSANXUATs.Find(maID);
            if (temp == null)
                return true;
            return false;
        }
        internal IQueryable<HSANXUAT> SearchByName(string key)
		{
			if (string.IsNullOrEmpty(key))
				return db.HSANXUATs;
			return db.HSANXUATs.Where(u => u.TENHANG.Contains(key));
		}
	}
}