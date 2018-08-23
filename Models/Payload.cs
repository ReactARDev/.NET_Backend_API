namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Payload
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Payload()
        {
        }

        [StringLength(36)]
        public string PayloadID { get; set; }

        [StringLength(50)]
        public string PayloadReferenceCode { get; set; }

        [StringLength(150)]
        public string PayloadWireframe { get; set; }

        [StringLength(150)]
        public string PayloadPhoto { get; set; }

        [StringLength(150)]
        public string Payload3D { get; set; }

        [StringLength(150)]
        public string PayloadIcon { get; set; }

        [StringLength(150)]
        public string Payload2525B { get; set; }

        [StringLength(150)]
        public string PayloadDatasheet { get; set; }

        [StringLength(150)]
        public string PayloadName { get; set; }

        [StringLength(50)]
        public string PayloadNomenclature { get; set; }

        public int? PayloadManufacturer { get; set; }

        [StringLength(50)]
        public string PayloadExecutiveAgent { get; set; }

        [StringLength(50)]
        public string PayloadContractProgram { get; set; }

        public decimal? PayloadCost { get; set; }

        [StringLength(300)]
        public string PayloadCostNotes { get; set; }

        public decimal? PayloadLength { get; set; }

        public decimal? PayloadWidth { get; set; }

        public decimal? PayloadHeight { get; set; }

        public decimal? PayloadWeight { get; set; }

        public decimal? PayloadPower { get; set; }

        [StringLength(50)]
        public string PayloadConnector1 { get; set; }

        [StringLength(50)]
        public string PayloadConnector2 { get; set; }

        public bool? PayloadDaySpotter { get; set; }

        public bool? PayloadThermalImager { get; set; }

        public bool? PayloadLaserDesginator { get; set; }

        public bool? PayloadContinuousZoom { get; set; }

        public bool? PayloadStabalization { get; set; }

        public bool? PayloadVibrationIsolation { get; set; }

        public bool? PayloadAutoTracker { get; set; }

        public bool? PayloadGPSTimeSync { get; set; }

        public bool? PayloadInternalGPS { get; set; }

        public bool? PayloadInternalINS { get; set; }

        public bool? PayloadMetadata { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PayloadCrewCount { get; set; }

        public int? PayloadMOS1 { get; set; }

        public int? PayloadMOS2 { get; set; }

        public int? PayloadMOS3 { get; set; }

        [StringLength(50)]
        public string PayloadFrequencyRange { get; set; }

        [StringLength(50)]
        public string PayloadScanCoverage { get; set; }

        [StringLength(50)]
        public string PayloadMaximumRange { get; set; }

        [StringLength(50)]
        public string PayloadMapResolution { get; set; }

        [StringLength(50)]
        public string PayloadGroundMapping { get; set; }

        [StringLength(50)]
        public string PayloadStripSAR { get; set; }

        [StringLength(50)]
        public string PayloadSpotlightSAR { get; set; }

        [StringLength(50)]
        public string PayloadCCEOIR { get; set; }

        [StringLength(50)]
        public string PayloadGeoReferencing { get; set; }

        [StringLength(50)]
        public string PayloadChangeDetect { get; set; }

        public int? PayloadLensCount { get; set; }

        [StringLength(50)]
        public string PayloadImageResolution { get; set; }

        public int? PayloadMaxAltitude { get; set; }

        public int? PayloadMaxRange { get; set; }

        [StringLength(50)]
        public string PayloadRefreshRateEO { get; set; }

        [StringLength(50)]
        public string PayloadRefreshRateIR { get; set; }

        [StringLength(50)]
        public string PayloadAngularCoverage { get; set; }

        [StringLength(50)]
        public string PayloadAreaCoverage { get; set; }

        [StringLength(50)]
        public string PayloadVirtualZoom { get; set; }

        [StringLength(50)]
        public string PayloadCrossCueing { get; set; }

        public int? PayloadType { get; set; }

        public DateTime LastUpdate { get; set; }

        public int? MissionRole { get; set; }

    }
}
