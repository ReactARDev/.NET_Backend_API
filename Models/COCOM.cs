namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class COCOM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public COCOM()
        {
        }

        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string description { get; set; }

        public int? displayOrder { get; set; }

        [StringLength(3)]
        public string languageCode { get; set; }
    }
}
