namespace _3012MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SPHAM")]
    public partial class SPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPHAM()
        {
            SPKHUYENMAIs = new HashSet<SPKHUYENMAI>();
        }

        [Key]
        [StringLength(10)]
        public string MASP { get; set; }

        [Required]
        [StringLength(50)]
        public string TENSP { get; set; }

        [StringLength(50)]
        public string LOAISP { get; set; }

        [StringLength(10)]
        public string HANGSX { get; set; }

        [Column(TypeName = "money")]
        public decimal? GIA { get; set; }

        public int? SOLUONG { get; set; }

        [Column(TypeName = "ntext")]
        public string MOTA { get; set; }

        public bool? ISNEW { get; set; }

        [Column(TypeName = "ntext")]
        public string ANHDAIDIEN { get; set; }

        public virtual HSANXUAT HSANXUAT { get; set; }

        public virtual LSANPHAM LSANPHAM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPKHUYENMAI> SPKHUYENMAIs { get; set; }
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<CTDONHANG> CTDONHANGs { get; set; }
	}
}
