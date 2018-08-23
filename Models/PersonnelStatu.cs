namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PersonnelStatu
    {
        [Key]
        [StringLength(36)]
        public string PersonnelID { get; set; }

        public int StatusCode { get; set; }

        public DateTime PersonnelArrive { get; set; }

        public DateTime PersonnelDepart { get; set; }

        [StringLength(300)]
        public string PersonnelRemarks { get; set; }

        public DateTime lastUpdate { get; set; }

        [Required]
        [StringLength(36)]
        public string lastUpdateUserId { get; set; }
    }
}
