namespace _3012MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USSER")]
    public partial class USSER
    {
        public long ID { get; set; }

        [StringLength(50)]
        public string USERNAME { get; set; }

        [StringLength(50)]
        public string PASS { get; set; }

        [StringLength(50)]
        public string NAME { get; set; }

        [StringLength(50)]
        public string ADDRESS { get; set; }

        [StringLength(50)]
        public string EMAIL { get; set; }

        [StringLength(20)]
        public string PHONE { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CREATEDATE { get; set; }

        [StringLength(50)]
        public string CREATEBY { get; set; }

        public DateTime? MODDIFIEDDATE { get; set; }

        public bool? SATUS { get; set; }

		[StringLength(20)]
		public string GROUPID { get; set; }
		[StringLength(50)]
		public string Token { get; set; }

	}
}
