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
		
		public IQueryable<SPHAM> SPMoiNhap()
		{
			var splist = db.SPHAMs.Where(s => s.ISNEW == true);
			return splist;
		}
		public IQueryable<SPHAM>Searchwithname(string name)
		{
			IQueryable<SPHAM> sp;
			sp = db.SPHAMs.Where(x => x.TENSP.Contains(name));
			return sp;
		}
		public IQueryable<SPHAM>Searchaddvend(string ten,string loai,string hangsx,int? max,int? min)
		{
			IQueryable<SPHAM> sp=db.SPHAMs;
			if(!string.IsNullOrEmpty(ten))
			{
				sp = Searchwithname(ten);
			}
			if(!string.IsNullOrEmpty(loai))
			{
				sp = from p in db.SPHAMs where p.LOAISP.Equals(loai) select p;
			}
			if (!string.IsNullOrEmpty(hangsx))
			{
				sp = from p in db.SPHAMs where p.HANGSX.Equals(hangsx) select p;
			}
			if (max!=null)
			{
				sp = from p in db.SPHAMs where p.GIA <= max select p;
			}
			if (min != null)
			{
				sp = from p in db.SPHAMs where p.GIA >= min select p;
			}
			return sp;
		}
		public IQueryable<SPHAM>Searchbytype(string type)
		{
			var sp = from p in db.SPHAMs where p.LOAISP.Equals(type) select p;
			return sp;

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
		public void UpdateSoluong(string masp,int? sl,bool? kt)
		{
			var s = db.SPHAMs.Find(masp);
			if(sl!=null)
			{
				if (kt == true)
				{
					s.SOLUONG += sl;
				}
				else if(kt==false)
				{
					s.SOLUONG -= sl;
				}
				db.Entry(s).State = System.Data.Entity.EntityState.Modified;
				db.SaveChanges();
			}
			
		}
	}
}