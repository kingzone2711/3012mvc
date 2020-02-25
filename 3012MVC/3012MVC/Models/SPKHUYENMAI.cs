namespace _3012MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SPKHUYENMAI")]
    public partial class SPKHUYENMAI
    {
        [Key]
        [StringLength(50)]
        public string MAKM { get; set; }

        [StringLength(10)]
        public string MASP { get; set; }

        [Column(TypeName = "ntext")]
        public string MOTA { get; set; }

        [StringLength(10)]
        public string GIAMGIA { get; set; }

        public virtual KMAI KMAI { get; set; }

        public virtual SPHAM SPHAM { get; set; }
    }
}
