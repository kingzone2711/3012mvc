using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3012MVC.Models
{
	public class GioHang
	{
		public List<ChiTietGioHang> cart;
		public double phivanchuyen = 0;
		public GioHang()
		{
			cart = new List<ChiTietGioHang>();
		}
		public List<ChiTietGioHang> getGiohang()
		{
			return cart;
		}
		public void addCart(ChiTietGioHang a)
		{
			cart.Add(a);//add vo list trong gio thoi
		}
		public bool removeCart(int index)
		{
			try
			{
				cart.RemoveAt(index);
				return true;
			}
			catch (Exception ) { return false; }
		}
		public double Tinhtongtiensanpham()
		{
			double count = 0;
			foreach (var temp in cart)
			{
				count += temp.Thanhtien;
			}
			return count;
		}
		public double tinhtongcart()
		{
			return Tinhtongtiensanpham() + phivanchuyen;
		}
		public bool Changequanlity(int index, string soluong)
		{
			try
			{
				cart[index].soluong = Int32.Parse(soluong);
				cart[index].tinhtien();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public int Tinhtongsoluongtronggio()
		{
			int count = 0;
			foreach (var temp in cart)
			{
				count += temp.soluong;
			}
			return count;
		}
	}
}