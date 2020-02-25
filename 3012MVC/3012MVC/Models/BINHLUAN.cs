namespace _3012MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BINHLUAN")]
    public partial class BINHLUAN
    {
        [Key]
        public int MABL { get; set; }

        [StringLength(10)]
        public string MASP { get; set; }

        [StringLength(50)]
        public string MAKH { get; set; }

        [Column(TypeName = "text")]
        public string NOIDUNG { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAYDANG { get; set; }

        [StringLength(20)]
        public string HOTEN { get; set; }

        [StringLength(50)]
        public string EMAIL { get; set; }

        public bool? DATRALOI { get; set; }

        public int? PARENT { get; set; }
    }
}
