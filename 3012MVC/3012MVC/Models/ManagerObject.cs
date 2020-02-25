using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3012MVC.Models
{
	public class ManagerObject
	{
		private static ManagerObject manager;
		public GioHang giohang = new GioHang();

		public static ManagerObject getIntance()
		{
			if (manager == null)
			{
				manager = new ManagerObject();
			}
			return manager;
		}
	}
}