namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EEIOptions")]
    public partial class EEIOptions
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }
    }
}
