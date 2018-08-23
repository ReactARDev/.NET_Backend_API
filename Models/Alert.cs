namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Alert
    {
        [StringLength(36)]
        public string id { get; set; }

        public int Type { get; set; }

        [Required]
        [StringLength(150)]
        public string Message { get; set; }

        public bool DashboardInd { get; set; }

        [StringLength(36)]
        public string AssetID { get; set; }

        public int? AssetType { get; set; }

        public bool Complete { get; set; }

        public DateTime createDate { get; set; }

        [StringLength(36)]
        public string createUserId { get; set; }

        public DateTime? lastUpdate { get; set; }

        [StringLength(36)]
        public string lastUpdateUserId { get; set; }

        [StringLength(3)]
        public string languageCode { get; set; }

        ////[StringLength(150)]
        ////public string LinkTo { get; set; }

        public virtual AlertType AlertType { get; set; }

        public virtual AssetType AssetType1 { get; set; }
    }
}
