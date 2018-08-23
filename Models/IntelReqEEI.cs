namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IntelReqEEI")]
    public partial class IntelReqEEI
    {
        public int id { get; set; }

        [StringLength(36)]
        public string intelReqID { get; set; }

        public int? threatGroupID { get; set; }

        [StringLength(150)]
        public string location { get; set; }
        [StringLength(50)]

        public string district { get; set; }

        public string gridCoordinates { get; set; }

        public int? LIMIDS_Req { get; set; }

        [StringLength(36)]
        public string POI1_ID { get; set; }

        [StringLength(36)]
        public string POI2_ID { get; set; }

        [StringLength(36)]
        public string POI3_ID { get; set; }

        public int? EEIs { get; set; }

        public DateTime createDate { get; set; }

        public DateTime? lastUpdateDate { get; set; }

        public int? objective { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public virtual EEIThreat EEIThreat { get; set; }

        public virtual LIMIDSReq LIMIDSReq { get; set; }

        public int? targetID { get; set; }

        public int? objectiveID { get; set; }
    }
}
