namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Country
    {
        [StringLength(10)]
        public string id { get; set; }

        [Required]
        [StringLength(64)]
        public string description { get; set; }

        [StringLength(3)]
        public string languageCode { get; set; }

        public int displayOrder { get; set; }
    }
}
