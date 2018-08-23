using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AMPS9000_WebAPI.Controllers
{
    public class StatusStatsController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/StatusStats
        public IHttpActionResult GetStatusStats()
        {
            int payCnt = db.Payloads.Count();
            int payReadyCnt = db.PayloadStatus.Where(x => x.StatusCode == (int)AssetStatuses.FLIGHT_READY || x.StatusCode == (int)AssetStatuses.READY).Count();
            int platCnt = db.Platforms.Count();
            int platReadyCnt = db.PlatformStatus.Where(x => x.StatusCode == (int)AssetStatuses.FLIGHT_READY || x.StatusCode == (int)AssetStatuses.READY).Count();

            var result = new
            {
                PayloadCount = payCnt,
                PayloadReadyCount = payReadyCnt,
                PayloadReadyPct = (Math.Round(((decimal)payReadyCnt / (decimal)payCnt), 2) * 100),
                PlatformCount = platCnt,
                PlatformReadyCount = platReadyCnt,
                PlatformReadyPct = (Math.Round(((decimal)platReadyCnt / (decimal)platCnt), 2) * 100)
            };

            return Ok(result);
        }
    }
}
