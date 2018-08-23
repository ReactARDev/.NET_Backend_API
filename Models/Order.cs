namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [StringLength(36)]
        public string OrderID { get; set; }

        public int? OrderNumber { get; set; }

        public int? OrderRelFTN { get; set; }

        public DateTime? OrderIssued { get; set; }

        public DateTime? OrderStart { get; set; }

        public DateTime? OrderEnd { get; set; }

        public int? OrderType { get; set; }

        public int? OrderMission { get; set; }

        public int? ServiceBranch { get; set; }

        public int? Unit { get; set; }

        [StringLength(50)]
        public string OrderSubCMD1 { get; set; }

        [StringLength(50)]
        public string OrderSubCMD2 { get; set; }

        [StringLength(50)]
        public string OrderDetail { get; set; }

        [StringLength(300)]
        public string OrderNotes { get; set; }

        [StringLength(150)]
        public string OrderLegacyDoc { get; set; }

        public virtual OrderType OrderType1 { get; set; }
    }
}
