namespace AMPS9000_WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IntelRequest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IntelRequest()
        {
        }

        [StringLength(36)]
        public string IntelRequestID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReqUserFrndlyID { get; set; }

        public DateTime? OrignatedDateTime { get; set; }

        [StringLength(36)]
        public string OrginatorPersonnelID { get; set; }

        public int? AreaOfOperations { get; set; }

        public int? SupportedCommand { get; set; }

        public int? SupportedUnit { get; set; }

        [StringLength(50)]
        public string NamedOperation { get; set; }

        public int? MissionType { get; set; }

        public int? SubMissionType { get; set; }

        public DateTime? ActiveDateTimeStart { get; set; }

        public DateTime? ActiveDateTimeEnd { get; set; }

        public DateTime? BestCollectionTime { get; set; }

        public DateTime? LatestTimeIntelValue { get; set; }

        [StringLength(50)]
        public string PriorityIntelRequirement { get; set; }

        [StringLength(300)]
        public string SpecialInstructions { get; set; }

        public int? PrimaryPayload { get; set; }

        public int? SecondaryPayload { get; set; }

        public bool? Armed { get; set; }

        [StringLength(36)]
        public string PointofContact { get; set; }

        [StringLength(3)]
        public string ReportClassification { get; set; }

        [StringLength(50)]
        public string LIMIDSRequest { get; set; }

        public int StatusId { get; set; }

        public int? NextHigherUnitId { get; set; }

        public int? PriorityId { get; set; }

        public int? AssetId { get; set; }

        public bool? IsInCollectionPlan { get; set; }
    }
}
