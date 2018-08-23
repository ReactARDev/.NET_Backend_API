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
    public class PayloadStatusController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/GetPayloadStatusData
        public IHttpActionResult GetPayloadStatusData()
        {
            var result = (from a in db.PayloadInventory
                          join b in db.Payloads on a.metaDataID equals b.PayloadID
                          join c in db.PayloadStatus on a.id equals c.PayloadInventoryID
                          join d in db.StatusCodes on c.StatusCode equals d.id into lojStat
                          from e in lojStat.DefaultIfEmpty()
                          orderby a.lastUpdate descending
                          select new
                          {
                              ID = a.id,
                              name = b.PayloadName,
                              serialNbr = a.serialNumber ?? "Unknown",
                              status = e.description ?? "Unknown",
                              remark = c.StatusComments,
                              ETIC = c.ETIC
                          });

            return Ok(result);
        }

        // GET: api/PayloadStatus/{guid}
        [ResponseType(typeof(PayloadInventoryStatusUpdateDTO))]
        public IHttpActionResult GetPayloadStatus(string id)
        {
            var results = (from a in db.PayloadStatus
                           join b in db.PayloadInventory on a.PayloadInventoryID equals b.id
                           where a.PayloadInventoryID == id
                           select new PayloadInventoryStatusUpdateDTO
                           {
                               id = a.PayloadInventoryID,
                               ETIC = a.ETIC,
                               Remark = a.StatusComments.Trim() ?? "",
                               StatusCode = a.StatusCode,
                               SerialNumber = b.serialNumber
                           }).FirstOrDefault();

            return Ok(results);
        }

        // PUT: api/PutPayloadStatusUpdate/{guid}
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPayloadStatusUpdate(string id, PayloadInventoryStatusUpdateDTO payloadInventoryStatusUpdate)
        {
            if (id != payloadInventoryStatusUpdate.id)
            {
                return BadRequest();
            }

            PayloadStatu status = db.PayloadStatus.Where(x => x.PayloadInventoryID == id).FirstOrDefault();

            if (status == null)
            {
                return BadRequest();
            }

            if (!db.StatusCodes.Any(x => x.id == payloadInventoryStatusUpdate.StatusCode && x.type == 3))
            {
                return BadRequest();
            }
            else
            {
                status.StatusCode = payloadInventoryStatusUpdate.StatusCode;
            }

            status.ETIC = payloadInventoryStatusUpdate.ETIC;
            status.StatusComments = payloadInventoryStatusUpdate.Remark;
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

        private bool PayloadStatuExists(string id)
        {
            return db.PayloadStatus.Count(e => e.PayloadInventoryID == id) > 0;
        }
    }
}