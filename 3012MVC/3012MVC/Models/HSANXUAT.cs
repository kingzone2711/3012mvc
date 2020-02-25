namespace _3012MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HSANXUAT")]
    public partial class HSANXUAT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HSANXUAT()
        {
            SPHAMs = new HashSet<SPHAM>();
        }

        [Key]
        [StringLength(10)]
        public string HANGSX { get; set; }

        [StringLength(50)]
        public string TENHANG { get; set; }

        [StringLength(50)]
        public string QUOCGIA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPHAM> SPHAMs { get; set; }
    }
}
