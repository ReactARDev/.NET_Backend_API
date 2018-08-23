namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Location
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Location()
        {
        }

        [StringLength(36)]
        public string LocationID { get; set; }

        [StringLength(50)]
        public string LocationReferenceCode { get; set; }

        [StringLength(150)]
        public string LocationPhoto { get; set; }

        [StringLength(150)]
        public string LocationDocument { get; set; }

        [StringLength(150)]
        public string LocationMapImage { get; set; }

        [StringLength(50)]
        public string LocationName { get; set; }

        [StringLength(50)]
        public string LocationStreet { get; set; }

        [StringLength(50)]
        public string LocationCity { get; set; }

        [StringLength(10)]
        public string LocationPostalCode { get; set; }

        [StringLength(2)]
        public string LocationCountry { get; set; }

        public int? LocationCOCOM { get; set; }

        public int? LocationRegion { get; set; }

        public string LocationLatitude { get; set; }

        public string LocationLongitude { get; set; }

        [StringLength(50)]
        public string LocationMGRS { get; set; }

        public int? LocationElevation { get; set; }

        [StringLength(36)]
        public string LocationPointofContact { get; set; }

        public decimal? LocationFrequency { get; set; }

        [StringLength(150)]
        public string KML { get; set; }

        public int? LocationCategory { get; set; }

        [StringLength(75)]
        public string LocationDescription { get; set; }

        public DateTime? LastUpdate { get; set; }

        [StringLength(50)]
        public string LocationNai { get; set; }

        [StringLength(2)]
        public string StateAbbrev { get; set; }

        [StringLength(200)]
        public string webAddress { get; set; }

        [StringLength(10)]
        public string IATA { get; set; }

        [StringLength(10)]
        public string ICAO { get; set; }

        [StringLength(10)]
        public string FAA { get; set; }

        [StringLength(10)]
        public string UserLocationID { get; set; }
    }
}
