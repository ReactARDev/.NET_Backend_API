namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IC_ISM_Classifications
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IC_ISM_Classifications()
        {
        }

        [Key]
        [StringLength(3)]
        public string ClassificationMarkingValue { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(3)]
        public string languageCode { get; set; }

    }
}
