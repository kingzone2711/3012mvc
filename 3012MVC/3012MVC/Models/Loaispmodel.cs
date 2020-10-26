using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3012MVC.Models
{
	public class Loaispmodel
	{
		Shopbanhang db = new Shopbanhang();
		public IQueryable<LSANPHAM>getsanpham()
		{
			return db.LSANPHAMs;
		}
	}
}