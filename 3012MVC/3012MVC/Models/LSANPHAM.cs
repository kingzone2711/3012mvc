namespace _3012MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LSANPHAM")]
    public partial class LSANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LSANPHAM()
        {
            SPHAMs = new HashSet<SPHAM>();
        }

        [Key]
        [StringLength(50)]
        public string MALOAI { get; set; }

        [StringLength(50)]
        public string TENLOAI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPHAM> SPHAMs { get; set; }
    }
}
