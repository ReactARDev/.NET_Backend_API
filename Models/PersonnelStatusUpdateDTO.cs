using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AMPS9000_WebAPI
{
    public class PersonnelStatusUpdateDTO
    {
        [StringLength(36)]
        public string id { get; set; }

        public int StatusCode { get; set; }

        public DateTime ArrivalDate { get; set; }

        public DateTime DepartureDate { get; set; }

        [StringLength(300)]
        public string Remark { get; set; }

    }
}