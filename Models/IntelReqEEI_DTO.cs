using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMPS9000_WebAPI
{
    public class IntelReqEEI_DTO
    {
        public int id { get; set; }
        public string intelReqID { get; set; }
        public int? targetID { get; set; }
        public string targetName { get; set; }
        public string targetNumber { get; set; }
        public string threatGroupID { get; set; }
        public string location { get; set; }
        public string district { get; set; }
        public string gridCoordinates { get; set; }
        public string LIMIDS_Req { get; set; }
        public string POI1_ID { get; set; }
        public string POI2_ID { get; set; }
        public string POI3_ID { get; set; }
        public int? EEIs { get; set; }
        public int? objectiveID { get; set; }
        public string objectiveName { get; set; }
    }
}