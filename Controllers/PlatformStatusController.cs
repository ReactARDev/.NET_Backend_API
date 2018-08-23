using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AMPS9000_WebAPI;

namespace AMPS9000_WebAPI.Controllers
{
    public class PlatformStatusController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/GetPlatformStatusData
        public IHttpActionResult GetPlatformStatusData()
        {
            var results = (from a in db.PlatformInventory
                           join b in db.Platforms on a.metaDataID equals b.PlatformID
                           join c in db.PlatformStatus on a.id equals c.PlatformInventoryID
                           join s in db.StatusCodes on c.StatusCode equals s.id
                           where s.type == 3
                           orderby a.lastUpdate descending
                           select new
                           {
                               ID = a.id,
                               name = b.PlatformName,
                               tailNbr = a.tailNumber,
                               status = s.description,
                               remark = c.StatusComments,
                               ETIC = c.ETIC
                           });

            return Ok(results);
        }

        // GET: api/GetPlatformStatus/{guid}
        [ResponseType(typeof(PlatformInventory))]
        public IHttpActionResult GetPlatformStatus(string id)
        {
            var results = (from a in db.PlatformStatus
                           join b in db.PlatformInventory on a.PlatformInventoryID equals b.id
                           where a.PlatformInventoryID == id
                           select new PlatformInventoryStatusUpdateDTO
                           {
                               id = a.PlatformInventoryID,
                               ETIC = a.ETIC,
                               Remark = a.StatusComments.Trim() ?? "",
                               StatusCode = a.StatusCode,
                               TailNumber = b.tailNumber ?? ""
                           }).FirstOrDefault();

            return Ok(results);
        }

        // PUT: api/PutPlatformStatusUpdate/{guid}
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlatformStatusUpdate(string id, PlatformInventoryStatusUpdateDTO platformInventoryStatusUpdate)
        {
            if (id != platformInventoryStatusUpdate.id)
            {
                return BadRequest();
            }

            PlatformStatu status = db.PlatformStatus.Where(x => x.PlatformInventoryID == id).FirstOrDefault();

            if (status == null)
            {
                return BadRequest();
            }

            if (!db.StatusCodes.Any(x => x.id == platformInventoryStatusUpdate.StatusCode && x.type == 3))
            {
                return BadRequest();
            }
            else
            {
                status.StatusCode = platformInventoryStatusUpdate.StatusCode;
            }

            status.ETIC = platformInventoryStatusUpdate.ETIC;
            status.StatusComments = platformInventoryStatusUpdate.Remark;
            status.lastUpdate = DateTime.Now;
            status.lastUpdateUserId = "000";

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlatformStatuExists(string id)
        {
            return db.PlatformStatus.Count(e => e.PlatformInventoryID == id) > 0;
        }
    }
}