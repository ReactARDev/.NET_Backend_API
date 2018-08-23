namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AlertRecipient")]
    public partial class AlertRecipient
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(36)]
        public string id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(36)]
        public string personnelID { get; set; }
    }
}
