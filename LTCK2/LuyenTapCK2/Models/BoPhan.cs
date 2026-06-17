namespace LuyenTapCK2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BoPhan")]
    public partial class BoPhan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BoPhan()
        {
            TiepVien = new HashSet<TiepVien>();
        }

        [Key]
        public int MaBoPhan { get; set; }

        [StringLength(25)]
        public string TenBoPhan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TiepVien> TiepVien { get; set; }
    }
}
