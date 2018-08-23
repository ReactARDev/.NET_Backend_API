namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LIMIDSReq")]
    public partial class LIMIDSReq
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LIMIDSReq()
        {
            IntelReqEEIs = new HashSet<IntelReqEEI>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        public int? displayOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IntelReqEEI> IntelReqEEIs { get; set; }
    }
}
