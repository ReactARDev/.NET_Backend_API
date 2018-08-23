namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BranchSubordinateTier1
    {
        public int id { get; set; }

        public int COCOM { get; set; }

        public int branchOfService { get; set; }

        public int branchCommandID { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [StringLength(100)]
        public string HQCity { get; set; }

        [StringLength(2)]
        public string HQStateAbbrev { get; set; }

        [StringLength(10)]
        public string HQCountryCode { get; set; }

        [StringLength(300)]
        public string webAddress { get; set; }

        public virtual BranchCommands BranchCommands { get; set; }
    }
}
