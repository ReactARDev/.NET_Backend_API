using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMPS9000_WebAPI
{
	public class IntelReqDTO
	{
		public string IntelRequestID { get; set; }

		public int ReqUserFrndlyID { get; set; }

		public DateTime? OrignatedDateTime { get; set; }

		public string OrginatorPersonnelID { get; set; }

		public int? AreaOfOperations { get; set; }

		public int? SupportedCommand { get; set; }

		public string COCOMText { get; set; }

		public int? SupportedUnit { get; set; }


		public string NamedOperation { get; set; }

		public int? MissionType { get; set; }

		public string MissionTypeText { get; set; }

		public int? SubMissionType { get; set; }

		public DateTime? ActiveDateTimeStart { get; set; }

		public DateTime? ActiveDateTimeEnd { get; set; }

		public DateTime? BestCollectionTime { get; set; }

		public DateTime? LatestTimeIntelValue { get; set; }

		public string PriorityIntelRequirement { get; set; }

		public string SpecialInstructions { get; set; }

		public int? PrimaryPayload { get; set; }

		public string PrimaryPayloadName { get; set; }

		public int? SecondaryPayload { get; set; }

		public string SecondaryPayloadName { get; set; }

		public bool? Armed { get; set; }

		public string MunitionName { get; set; }

		public string PointofContact { get; set; }

		public string POCText { get; set; }

		public string ReportClassification { get; set; }

		public string RptClassText { get; set; }

		public string LIMIDSRequest { get; set; }

		public string OriginatorFirstName { get; set; }

		public string OriginatorLastName { get; set; }

		public int StatusId { get; set; }

		public string Status { get; set; }

		public string OriginatorEmail { get; internal set; }

		public string OriginatorDSN { get; internal set; }

		public IQueryable<IntelReqEEI_DTO> IntelReqEEIs { get; set; }

		public string NextHigherUnit { get; internal set; }

		public int? NextHigherUnitId { get; internal set; }

		public string Priority { get; internal set; }

		public int? PriorityId { get; internal set; }

		public string ReportClassificationName { get; internal set; }

		public string Unit { get; internal set; }

		public int? UnitId { get; internal set; }

		public string Abbreviation { get; internal set; }

		public int? AssetId { get; internal set; }

		public string Asset { get; internal set; }

        public bool? IsInCollectionPlan { get; set; }
    }
}