namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PEDTeam
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PEDTeam()
        {
            PEDPersonnels = new HashSet<PEDPersonnel>();
        }

        [Key]
        [StringLength(36)]
        public string PEDID { get; set; }

        [Required]
        [StringLength(50)]
        public string Identifier { get; set; }

        public int TypeCode { get; set; }

        public int StatusCode { get; set; }

        public DateTime StatusDateTime { get; set; }

        [StringLength(300)]
        public string StatusComments { get; set; }

        public DateTime lastUpdate { get; set; }

        [Required]
        [StringLength(36)]
        public string lastUpdateUserId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PEDPersonnel> PEDPersonnels { get; set; }

        public virtual PEDType PEDType { get; set; }

        public virtual User User { get; set; }
    }
}
