using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3012MVC.Common
{
	[Serializable]
	public class Userlogin
	{
		public long UserId	{get;set;}
		public string Name { get; set; }
		public string Groupid { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
	}
}