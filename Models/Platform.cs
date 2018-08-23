namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Platform
    {
        [StringLength(36)]
        public string PlatformID { get; set; }

        [StringLength(150)]
        public string PlatformWireframe { get; set; }

        [StringLength(150)]
        public string PlatformPhoto { get; set; }

        [StringLength(150)]
        public string Platform3D { get; set; }

        [StringLength(150)]
        public string PlatformIcon { get; set; }

        [StringLength(150)]
        public string Platform2525B { get; set; }

        [StringLength(150)]
        public string PlatformDatasheet { get; set; }

        [StringLength(50)]
        public string PlatformName { get; set; }

        [StringLength(50)]
        public string PlatformNomenclature { get; set; }

        public int? PlatformCategory { get; set; }

        public int? PlatformRole { get; set; }

        public int? PlatformManufacturer { get; set; }

        [StringLength(50)]
        public string PlatformExecutiveAgent { get; set; }

        [StringLength(50)]
        public string PlatformContractProgram { get; set; }

        public decimal? PlatformCost { get; set; }

        [StringLength(300)]
        public string PlatformCostNotes { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PlatformIOCDate { get; set; }

        public int? PlatformGroundStation { get; set; }

        public decimal? PlatformLength { get; set; }

        public decimal? PlatformWingspan { get; set; }

        public decimal? PlatformHeight { get; set; }

        public decimal? PlatformWeight { get; set; }

        [StringLength(50)]
        public string PlatformPowerPlant { get; set; }

        public int? PlatformFuelCapacity { get; set; }

        public int? PlatformCruisingSpeed { get; set; }

        public int? PlatformRange { get; set; }

        public int? PlatformCeiling { get; set; }

        public int? PlatformMaxTakeOff { get; set; }

        public int? PlatformMinRunway { get; set; }

        public decimal? PlatformPayloadCapacity { get; set; }

        public int? PlatformPayloadCount { get; set; }

        public decimal? PlatformArmamentCapacity { get; set; }

        public int? PlatformArmamentCount { get; set; }

        public int? PlatformFlightCrewReq { get; set; }

        public int? PlatformLineCrewReq { get; set; }

        public int? PlatformPayloadCrewReq { get; set; }

        public int? PlatformPEDCrewReq { get; set; }

        public int? PlatformFlightCrewMOS { get; set; }

        public int? PlatformLineCrewMOS { get; set; }

        public int? PlatformPayloadCrewMOS { get; set; }

        public int? PlatformPEDCrewMOS { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
