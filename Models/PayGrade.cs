namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PayGrade
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PayGrade()
        {
        }

        public int id { get; set; }

        [Required]
        [StringLength(3)]
        public string GradeType { get; set; }

        [Column("PayGrade")]
        [StringLength(2)]
        public string PayGrade1 { get; set; }

        [Required]
        [StringLength(5)]
        public string DisplayText { get; set; }

        public byte DisplayOrder { get; set; }

        [StringLength(3)]
        public string languageCode { get; set; }

    }
}
