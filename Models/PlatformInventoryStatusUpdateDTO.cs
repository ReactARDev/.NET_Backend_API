using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AMPS9000_WebAPI
{
    public class PlatformInventoryStatusUpdateDTO
    {
        [StringLength(36)]
        public string id { get; set; }

        public int StatusCode { get; set; }

        public int ETIC { get; set; }

        [StringLength(300)]
        public string Remark { get; set; }

        public string TailNumber { get; set; }
    }
}