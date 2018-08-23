using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMPS9000_WebAPI
{
    public class AlertsDTO
    {
        public string name { get; set; }
        public int alertType { get; set; }
        public bool complete { get; set; }
        public bool dashboardInd { get; set; }
        public string type { get; set; }
        public string ID { get; set; }
        public string LinkTo { get; set; }
    }
}