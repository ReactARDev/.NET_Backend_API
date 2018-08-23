namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PlatformCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PlatformCategory()
        {
        }

        public int id { get; set; }

        [StringLength(50)]
        public string description { get; set; }

        [StringLength(3)]
        public string languageCode { get; set; }

        [StringLength(10)]
        public string abbreviation { get; set; }

    }
}
