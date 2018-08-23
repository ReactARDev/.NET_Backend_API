namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IntelReqStatu
    {
        public int id { get; set; }

        [Required]
        [StringLength(36)]
        public string IntelRequestID { get; set; }

        public int? Status { get; set; }

        public DateTime? StatusDateTime { get; set; }

    }
}
