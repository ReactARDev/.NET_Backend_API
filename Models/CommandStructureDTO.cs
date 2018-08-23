using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AMPS9000_WebAPI
{
    public class CommandStructureDTO
    {
        [StringLength(36)]
        public string CommandEntityStructID { get; set; }

        public int unitID { get; set; }

        public string UnitName { get; set; }

        /// <summary>
        /// The location ID for the unit's base
        /// </summary>
        /// <example>6A11733B-C184-4573-A146-871CA701F2D1</example>
        [StringLength(36)]
        public string LocationID { get; set; }

        public string LocationName { get; set; }

        public string cityState { get; set; }

        /// <summary>
        /// The unit directly above this one
        /// </summary>
        /// <example>38C45052-CACC-47C1-BDB4-C5C9363DAC24</example>
        [StringLength(36)]
        public string superiorCommandStructID { get; set; }

        public int branchID { get; set; }

        /// <summary>
        /// Unit's depth in the returned tree
        /// </summary>
        public int tier { get; set; }
    }
}