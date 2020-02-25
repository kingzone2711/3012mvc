
namespace _3012MVC.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;
	using System.Windows.Input;
	[Table("ROLE")]
	public class ROLE
	{
		[Key]
		[StringLength(50)]
		public string ID { get; set; }
		[StringLength(50)]
		public string NAME { get; set; }

	}
}