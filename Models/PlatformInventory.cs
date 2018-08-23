using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AMPS9000_WebAPI
{
    [Table("PlatformInventory")]
    public class PlatformInventory
    {


        [Key]
        [StringLength(36)]
        public String id { get; set; }

        [Required]
        [StringLength(36)]
        public string metaDataID { get; set; }

        [Required]
        [StringLength(36)]
        public string locationID { get; set; }

        public int owningUnit { get; set; }

        [StringLength(50)]
        public string tailNumber { get; set; }

        [StringLength(36)]
        public string payload1 { get; set; }

        [StringLength(36)]
        public string payload2 { get; set; }

        [StringLength(36)]
        public string payload3 { get; set; }

        [StringLength(36)]
        public string armament1 { get; set; }

        [StringLength(36)]
        public string armament2 { get; set; }

        [StringLength(36)]
        public string armament3 { get; set; }

        public int? coms1 { get; set; }

        public int? coms2 { get; set; }

        public DateTime lastUpdate { get; set; }

        [StringLength(36)]
        public string lastUpdateUserId { get; set; }

        public int? branch { get; set; }

        public int? COCOM { get; set; }

        public int? locationcategory { get; set; }
    }

}