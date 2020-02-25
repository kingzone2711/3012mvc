namespace _3012MVC.Models
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class Shopbanhang : DbContext
	{
		public Shopbanhang()
			: base("name=Shopbanhang")
		{
		}

		public virtual DbSet<BINHLUAN> BINHLUANs { get; set; }
		public virtual DbSet<CTDONHANG> CTDONHANGs { get; set; }
		public virtual DbSet<DONHANG> DONHANGs { get; set; }
		public virtual DbSet<HSANXUAT> HSANXUATs { get; set; }
		public virtual DbSet<KMAI> KMAIs { get; set; }
		public virtual DbSet<LSANPHAM> LSANPHAMs { get; set; }
		public virtual DbSet<SPHAM> SPHAMs { get; set; }
		public virtual DbSet<SPKHUYENMAI> SPKHUYENMAIs { get; set; }
		public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
		public virtual DbSet<USSER> USSERs { get; set; }
		public virtual DbSet<ROLE> ROLEs { get; set; }
		public virtual DbSet<USERGROUP> USERGROUPs { get; set; }
		public virtual DbSet<CREDENTIAL> CREDENTIALs { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<BINHLUAN>()
				.Property(e => e.NOIDUNG)
				.IsUnicode(false);

			modelBuilder.Entity<DONHANG>()
				.Property(e => e.PHIVANCHUYEN)
				.HasPrecision(18, 0);

			modelBuilder.Entity<DONHANG>()
				.Property(e => e.GHICHU)
				.IsUnicode(false);

			modelBuilder.Entity<KMAI>()
				.HasOptional(e => e.SPKHUYENMAI)
				.WithRequired(e => e.KMAI);

			modelBuilder.Entity<LSANPHAM>()
				.HasMany(e => e.SPHAMs)
				.WithOptional(e => e.LSANPHAM)
				.HasForeignKey(e => e.LOAISP);

			modelBuilder.Entity<SPHAM>()
				.Property(e => e.GIA)
				.HasPrecision(19, 4);

			modelBuilder.Entity<USSER>()
				.Property(e => e.PASS)
				.IsUnicode(false);
		}

		public System.Data.Entity.DbSet<_3012MVC.Models.Login> Logins { get; set; }
	}
}
