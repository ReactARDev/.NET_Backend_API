namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MunitionsInventory")]
    public partial class MunitionsInventory
    {
        [StringLength(36)]
        public string id { get; set; }

        [Required]
        [StringLength(36)]
        public string metaDataID { get; set; }

        [Required]
        [StringLength(36)]
        public string locationID { get; set; }

        public int? owningUnit { get; set; }

        public int? branch { get; set; }

        public int? COCOM { get; set; }

        [StringLength(50)]
        public string serialNumber { get; set; }

        public DateTime lastUpdate { get; set; }

        [StringLength(36)]
        public string lastUpdateUserId { get; set; }

        public int? locationcategory { get; set; }
    }

}
