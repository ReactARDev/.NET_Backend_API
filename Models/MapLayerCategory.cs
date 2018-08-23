namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MapLayerCategory")]
    public partial class MapLayerCategory
    {
        public int id { get; set; }

        [Required]
        [StringLength(150)]
        public string description { get; set; }

        public DateTime createDate { get; set; }

        [Required]
        [StringLength(36)]
        public string createUserId { get; set; }

        [StringLength(200)]
        public string name { get; set; }
    }
}
