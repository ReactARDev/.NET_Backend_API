namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rank()
        {

        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        public int displayOrder { get; set; }

        [StringLength(10)]
        public string rankAbbreviation { get; set; }

        [StringLength(3)]
        public string languageCode { get; set; }

        public int? classificationID { get; set; }

        public int? payGradeID { get; set; }

        public int? branchOfServiceID { get; set; }


    }
}
