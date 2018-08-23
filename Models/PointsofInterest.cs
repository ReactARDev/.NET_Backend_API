namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PointsofInterest")]
    public partial class PointsofInterest
    {
        [Key]
        [StringLength(36)]
        public string PointofInterestID { get; set; }

        [StringLength(50)]
        public string ReferenceCode { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        [StringLength(150)]
        public string Image { get; set; }

        [StringLength(150)]
        public string Document { get; set; }

        public DbGeography Latitude { get; set; }

        public DbGeography Longitude { get; set; }

        [StringLength(50)]
        public string MGRS { get; set; }

        public decimal? Elevation { get; set; }

        public DateTime createDate { get; set; }

        [Required]
        [StringLength(36)]
        public string createUserId { get; set; }

        public DateTime? lastUpdate { get; set; }

        [StringLength(36)]
        public string lastUpdateUserId { get; set; }

        [StringLength(150)]
        public string KML { get; set; }
    }
}
