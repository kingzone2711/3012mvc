namespace _3012MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KMAI")]
    public partial class KMAI
    {
        [Key]
        [StringLength(50)]
        public string MAKM { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAYBATDAU { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAYKETTHUC { get; set; }

        [Column(TypeName = "ntext")]
        public string NOIDUNG { get; set; }

        [Column(TypeName = "ntext")]
        public string ANHCT { get; set; }

        [Required]
        [StringLength(128)]
        public string TENCT { get; set; }
        public virtual SPKHUYENMAI SPKHUYENMAI { get; set; }
    }
}
