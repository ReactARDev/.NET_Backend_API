namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BranchCommands
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BranchCommands()
        {
            BranchSubordinateTier1 = new HashSet<BranchSubordinateTier1>();
        }

        public int id { get; set; }

        public int COCOM { get; set; }

        public int branchOfService { get; set; }

        [Required]
        [StringLength(150)]
        public string name { get; set; }

        [StringLength(50)]
        public string abbreviation { get; set; }

        [StringLength(100)]
        public string HQCity { get; set; }

        [StringLength(10)]
        public string HQCountryCode { get; set; }

        [StringLength(2)]
        public string HQStateAbbrev { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BranchSubordinateTier1> BranchSubordinateTier1 { get; set; }
    }
}
