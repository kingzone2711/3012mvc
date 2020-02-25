using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3012MVC.Models
{
	public class ChiTietGioHang
	{
		public SPHAM sanpham { get; set; }
		public int soluong { get; set; }
		private double thanhtien;
		public double Thanhtien
		{
			get { return (double)sanpham.GIA * soluong; }
			set{ thanhtien = value; } 
		}
		public DONHANG donhang
		{
			get;set;
		}
		public void tinhtien()
		{
			Thanhtien = (double)sanpham.GIA * soluong;
		}
	}
}