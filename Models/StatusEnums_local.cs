using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMPS9000_WebAPI
{
    public enum PersonnelStatuses
    {
        PENDING = 1006,
        ASSIGNED = 1007,
        ON_LEAVE = 1008
    }

    public enum IntelReqStatuses
    {
        PENDING = 1,
        DENIED = 2,
        ASSIGNED = 3,
        ACTIVE = 4,
        CANCELED = 5,
        COMPLETED = 6
    }

    public enum AssetStatuses
    {
        FLIGHT_READY = 1002,
        OFFLINE = 1003,
        READY = 1004,
        LOW = 1005
    }
}