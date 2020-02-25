using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _3012MVC.Models
{
	public class Login
	{
		[Key]
		[Display(Name ="Tên đăng nhâp")]
		[Required(ErrorMessage = "Nhập user name")]
		public string UserName { get; set; }
		[Display(Name = "Mật Khẩu")]
		[Required(ErrorMessage = "Nhập user name")]
		public string Password { get; set; }

	}
}