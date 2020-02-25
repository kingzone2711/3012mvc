namespace _3012MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DONHANG")]
    public partial class DONHANG
    {
        [Key]
        [StringLength(50)]
        public string MADONHANG { get; set; }

       
        public long MAKH { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAYDATMUA { get; set; }

        public decimal? PHIVANCHUYEN { get; set; }

        [StringLength(200)]
        public string PTGIAODICH { get; set; }

        public int? TINHTRANGDH { get; set; }

        public double? TONGTIEN { get; set; }

        [StringLength(1000)]
        public string DIACHI { get; set; }

        [Column(TypeName = "text")]
        public string GHICHU { get; set; }

        [StringLength(11)]
        public string DIENTHOAI { get; set; }
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<CTDONHANG> CTDONHANGs { get; set; }
	}
}
