namespace _3012MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


	[Table("CTDONHANG")]
	public partial class CTDONHANG
	{
		[Key]
		[Column(Order = 0)]
		[StringLength(50)]
		public string MADONHANG { get; set; }

		[Key]
		[Column(Order = 1)]
		[StringLength(10)]
		public string MASP { get; set; }

		public int? SOLUONG { get; set; }

		public double? THANHTIEN { get; set; }

		public virtual DONHANG DONHANG { get; set; }

		public virtual SPHAM SPHAM { get; set; }
	}
}

