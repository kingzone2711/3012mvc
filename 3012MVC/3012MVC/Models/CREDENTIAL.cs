
namespace _3012MVC.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;
	using System.Windows.Input;
	[Table("CREDENTIAL")]
	[Serializable]
	public class CREDENTIAL
	{
		[Key]
		[StringLength(20)]
		public string USERGROUP { get; set; }
	
		[StringLength(50)]
		public string ROLEID { get; set; }
	}
}