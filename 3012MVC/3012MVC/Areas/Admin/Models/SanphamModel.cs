using _3012MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3012MVC.Areas.Admin.Models
{
	
	public class SanphamModel
	{
		Shopbanhang db = new Shopbanhang();
		internal IQueryable<SPHAM> SPMoiNhap()
		{
			var splist = db.SPHAMs.Where(s => s.ISNEW == true);
			return splist;
		}

		public IQueryable<SPHAM>Getsp()
		{
			return db.SPHAMs;
		}
		public IQueryable<HSANXUAT>Gethangsanxuat()
		{
			return db.HSANXUATs;
		}
		public IQueryable<LSANPHAM>Getloaisp()
		{
			return db.LSANPHAMs;
		}
		public void deletesp(string id)
		{
			SPHAM sp= db.SPHAMs.Find(id);
			db.SPHAMs.Remove(sp);
			db.SaveChanges();
		 }
		public string themsp(SPHAM sanpham)
		{
			sanpham.MASP = TaoMa();
			sanpham.ANHDAIDIEN = sanpham.MASP + "1.jpg";
			sanpham.GIA = sanpham.GIA;
			db.SPHAMs.Add(sanpham);
			db.SaveChanges();
			return sanpham.MASP;
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
			
				var temp = db.SPHAMs.Find(maID);
				if (temp == null)
					return true;
				return false;
			
		}
	}
}