namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommandStructure")]
    public partial class CommandStructure
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CommandStructure()
        {
        }

        [StringLength(36)]
        public string id { get; set; }

        public int unitID { get; set; }

        public int commandRelationship { get; set; }

        [StringLength(36)]
        public string superiorCommandStructID { get; set; }

        public int branchID { get; set; }

        public DateTime lastUpdate { get; set; }

        [Required]
        [StringLength(36)]
        public string lastUpdateUserID { get; set; }

    }
}
