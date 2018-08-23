namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Region
    {
        public int id { get; set; }

        [Required]
        [StringLength(150)]
        public string description { get; set; }

        public int? displayOrder { get; set; }

        [StringLength(3)]
        public string languageCode { get; set; }
    }
}
