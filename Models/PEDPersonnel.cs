namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PEDPersonnel")]
    public partial class PEDPersonnel
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(36)]
        public string PEDID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(36)]
        public string PersonnelID { get; set; }

        public DateTime AssignmentDate { get; set; }

        public virtual PEDTeam PEDTeam { get; set; }
    }
}
