using _3012MVC.Areas.Admin.Models;
using _3012MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3012MVC.Models
{
	
	public class DonHangModel
	{
		public USSER nguoimua;
		public DONHANG donhang;
		public string tinhtrangdh;
		public List<DonHangModel> xemdonhang(int MAKH)
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
				}
			}
			catch (Exception) { }
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
	}
}