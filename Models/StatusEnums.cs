using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMPS9000_WebAPI
{
    public enum PersonnelStatuses
    {
        PENDING = 11,
        ASSIGNED = 12,
        ON_LEAVE = 13
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
        FLIGHT_READY = 7, 
        OFFLINE = 8,
        READY = 9,
        LOW = 10
    }
}