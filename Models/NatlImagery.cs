namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NatlImagery")]
    public partial class NatlImagery
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(36)]
        public string NatImageryID { get; set; }

        [StringLength(10)]
        public string ReferenceCode { get; set; }

        [StringLength(10)]
        public string Name { get; set; }

        public DbGeography Latitude { get; set; }

        public DbGeography Longitude { get; set; }

        [StringLength(50)]
        public string MGRS { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime CreateDateTime { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(150)]
        public string NatlImgaryImage { get; set; }
    }
}
