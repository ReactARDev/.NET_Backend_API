namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Unit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Unit()
        {
           
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(10)]
        public string UnitIdentificationCode { get; set; }

        [StringLength(10)]
        public string DerivativeUIC { get; set; }

        [StringLength(10)]
        public string JointManningDocument { get; set; }

        [StringLength(36)]
        public string LocationID { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
