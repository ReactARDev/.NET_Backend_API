namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MOS_Desc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MOS_Desc()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string description { get; set; }

        public byte? displayOrder { get; set; }

        [StringLength(10)]
        public string MOSCode { get; set; }

        [StringLength(3)]
        public string languageCode { get; set; }

        public int branchOfServiceID { get; set; }

    }
}
