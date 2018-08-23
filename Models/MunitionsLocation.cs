namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MunitionsLocation
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(36)]
        public string locationID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(36)]
        public string munitionID { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime lastUpdate { get; set; }
    }
}
