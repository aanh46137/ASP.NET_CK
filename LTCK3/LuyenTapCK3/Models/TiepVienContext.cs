using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace LuyenTapCK3.Models
{
    public partial class TiepVienContext : DbContext
    {
        public TiepVienContext()
            : base("name=TiepVienContext")
        {
        }

        public virtual DbSet<BoPhan> BoPhan { get; set; }
        public virtual DbSet<TiepVien> TiepVien { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
