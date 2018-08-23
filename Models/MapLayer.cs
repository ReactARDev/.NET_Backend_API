namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MapLayer
    {
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public int? categoryID { get; set; }

        [StringLength(250)]
        public string description { get; set; }

        [Required]
        [StringLength(150)]
        public string fileLocation { get; set; }
    }
}
