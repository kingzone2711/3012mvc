using _3012MVC.Areas.Admin.Models;
using _3012MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace _3012MVC.Models
{
	
	public class DonHangModel
	{
		public USSER nguoimua;
		public DONHANG donhang;
		public string tinhtrangdh;
		public List<DonHangModel> xemdonhang(long MAKH)
		{
			using(Shopbanhang db = new Shopbanhang())
			{
				List<DonHangModel> listdh = new List<DonHangModel>();
				db.DONHANGs.AsNoTracking();
				var danhsach = from p in db.DONHANGs where p.MAKH == MAKH select p;
				foreach(var item in danhsach.ToList())
				{
					USSER user = (from p in db.USSERs where p.ID == MAKH select p).FirstOrDefault();
					listdh.Add(new DonHangModel()
					{
						donhang = item,
						nguoimua = user,
						tinhtrangdh = gettinhTrangDH(item.TINHTRANGDH)
					}) ;
				}
				return listdh;
			}
		}
		private string gettinhTrangDH(int? nullable)
		{
			switch (nullable)
			{
				case 0:
					{
						return "Chưa giao";
					}
				case 1:
					{
						return "Đang duyệt";
					}
				case 2:
					{
						return "Đang giao hàng";
					}
				case 3:
					{
						return "Đã giao";
					}
				case 4:
					{
						return "Đã hủy";
					}
			}
			return "Đang duyệt";
		}
		public bool HuyDH(string maDH)
		{
			try
			{
				using (Shopbanhang db = new Shopbanhang())
				{
					string query = "update DONHANG set TINHTRANGDH = '4' where MADONHANG ='" + maDH + "'";
					db.Database.ExecuteSqlCommand(query);
					return true;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}
		public USSER Checktrangthai(int id)
		{
			using(Shopbanhang db = new Shopbanhang())
			{
				USSER user = (from p in db.USSERs where p.ID == id select p).FirstOrDefault();
				return user;
			}
		}
		public void Luudonhang(DonHangTongQuan a,long maKH,GioHang giohang)
		{
			try
			{
				using (Shopbanhang db = new Shopbanhang())
				{
					DONHANG dhkh = new DONHANG();
					dhkh.MADONHANG = RandomMa();
					dhkh.MAKH = maKH;

					dhkh.DIACHI = a.address;
					dhkh.DIENTHOAI = a.phoneNumber;
					dhkh.GHICHU = a.Note;
					dhkh.NGAYDATMUA = DateTime.Now;
					dhkh.TINHTRANGDH = 1;
					dhkh.TONGTIEN = giohang.Tinhtongtiensanpham();
					dhkh.PHIVANCHUYEN = 0;

					dhkh = db.DONHANGs.Add(dhkh);
					db.SaveChanges();
					Luuchitietdonhang(giohang, db, dhkh.MADONHANG);
				}
			}
			catch (Exception) { }
		}
		public void Luuchitietdonhang(GioHang giohang,Shopbanhang db,string madh)
		{
			foreach(var item in giohang.getGiohang())
			{
				CTDONHANG ctdh = new CTDONHANG();
				ctdh.MADONHANG = madh;
				ctdh.MASP = item.sanpham.MASP;
				ctdh.SOLUONG = item.soluong;
				ctdh.THANHTIEN = item.Thanhtien;
				db.CTDONHANGs.Add(ctdh);
				db.SaveChanges();
			}

		}
		public string RandomMa()
		{
			string maID;
			Random rand = new Random();
			do
			{
				maID ="";
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
			using (Shopbanhang db = new Shopbanhang())
			{
				var temp = db.DONHANGs.Find(maID);
				if (temp == null)
					return true;
				return false;
			}
		}
		public IQueryable<DONHANG>Timdonhang(string key,string phone,DateTime? date,int? status)
		{
			Shopbanhang db = new Shopbanhang();
			IQueryable<DONHANG> lst = db.DONHANGs;
			if (!string.IsNullOrEmpty(key))
				lst = lst.Where(m => m.MADONHANG.Contains(key));
			if (!string.IsNullOrEmpty(phone))
				lst = lst.Where(m => m.DIENTHOAI.Contains(phone));
			if (status != null)
				lst = lst.Where(m => m.TINHTRANGDH == status);
			if (date != null)
				lst = lst.Where(m => m.NGAYDATMUA.Value.Year == date.Value.Year && m.NGAYDATMUA.Value.Month == date.Value.Month && m.NGAYDATMUA.Value.Day == date.Value.Day);
			return lst;
		}
		public bool Updatetinhtrangdh(string madh,int? tt)
		{
			if (tt == null)
				return false;
			try {
					Shopbanhang db = new Shopbanhang();
					DONHANG dh = db.DONHANGs.Find(madh);
					if(dh.TINHTRANGDH==4||dh.TINHTRANGDH==3)
					{ 
						return false;
					}
					if (dh.TINHTRANGDH == 1)
					{
						if(tt==2||tt==3)
						{
							foreach(var item in db.CTDONHANGs)
							{
							SanphamModel sp = new SanphamModel();
							sp.UpdateSoluong(item.MASP, item.SOLUONG, false);
							}
						}
					}
					if(dh.TINHTRANGDH==2)
					{
						if(tt==4)
						{
							foreach (var item in db.CTDONHANGs)
							{
								SanphamModel sp = new SanphamModel();
								sp.UpdateSoluong(item.MASP, item.SOLUONG, false);
							}
						}
						if(tt==1)
						{
						return false;
						}

					}
				string query = "update DONHANG set TINHTRANGDH = " + tt + " where MADONHANG ='" + madh + "'";
				db.Database.ExecuteSqlCommand(query);
				return true;

			}
			catch (Exception)
			{
				return false;
			}
			
		}
		public IQueryable<CTDONHANG>Chitietdonhang(string madh)
		{
			Shopbanhang db = new Shopbanhang();
			return db.CTDONHANGs.Where(x => x.MADONHANG.Contains(madh));
		}
	}
}