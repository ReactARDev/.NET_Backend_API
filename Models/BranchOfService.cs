namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BranchOfService")]
    public partial class BranchOfService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BranchOfService()
        {

        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        [StringLength(3)]
        public string languageCode { get; set; }

    }
}
