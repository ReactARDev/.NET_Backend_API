namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UnitPositions
    {
        [StringLength(36)]
        public string id { get; set; }

        public int unitID { get; set; }

        public int positionID { get; set; }

        [StringLength(36)]
        public string personnelID { get; set; }

        [Required]
        [StringLength(1)]
        public string vacantInd { get; set; }

        [StringLength(10)]
        public string Paragraph { get; set; }

        [StringLength(10)]
        public string Line { get; set; }

    }
}
