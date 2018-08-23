namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CCIRPIR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CCIRPIR()
        {
        }

        [Key]
        [StringLength(36)]
        public String CCIRPIRId { get; set; }

        [StringLength(255)]
        public string Description1 { get; set; }

        [StringLength(255)]
        public string Description2 { get; set; }

        [StringLength(255)]
        public string Description3 { get; set; }

        [StringLength(255)]
        public string Description4 { get; set; }

        [Required]
        [StringLength(255)]
        public string Description5 { get; set; }

        [StringLength(255)]
        public string Description6 { get; set; }

        [StringLength(255)]
        public string Description7 { get; set; }

        [StringLength(255)]
        public string Description8 { get; set; }

        public DateTime CreationDateTime { get; set; }

        public int COCOMId { get; set; }

        public int BranchId { get; set; }

        [Required]
        [StringLength(36)]
        public String CountryId { get; set; }

        public int RegionId { get; set; }

        public int UnitId { get; set; }

        [Required]
        [StringLength(36)]
        public String CommanderId { get; set; }

        [Required]
        [StringLength(50)]
        public String MissionName { get; set; }

        public String EffectiveAreaKML { get; set; }

        public DateTime LastUpdate { get; set; }

        [StringLength(36)]
        public string LastUpdateUserId { get; set; }
    }
}
