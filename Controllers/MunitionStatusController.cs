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
    public class MunitionStatusController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/GetMunitionsStatusData
        public IHttpActionResult GetMunitionsStatusData()
        {
            var result = (from a in db.MunitionsInventory
                          join b in db.MunitionStatus on a.id equals b.MunitionInventoryID
                          join c in db.StatusCodes on b.StatusCode equals c.id into lojStat
                          from d in lojStat.DefaultIfEmpty()
                          join e in db.Munitions on a.metaDataID equals e.MunitionID
                          orderby a.lastUpdate descending
                          select new
                          {
                              ID = a.id,
                              name = e.MunitionName,
                              serialNbr = a.serialNumber ?? "Unknown",
                              status = d.description ?? "Unknown",
                              remark = b.StatusComments ?? ""
                          });

            return Ok(result);
        }

        // GET: api/GetMunitionsStatus/{guid}
        [ResponseType(typeof(MunitionsInventoryStatusUpdateDTO))]
        public IHttpActionResult GetMunitionsStatus(string id)
        {
            var results = (from a in db.MunitionStatus
                           join b in db.MunitionsInventory on a.MunitionInventoryID equals b.id
                           where a.MunitionInventoryID == id
                           select new MunitionsInventoryStatusUpdateDTO
                           {
                               id = a.MunitionInventoryID,
                               ETIC = a.ETIC,
                               Remark = a.StatusComments.Trim() ?? "",
                               StatusCode = a.StatusCode,
                               SerialNumber = b.serialNumber
                           }).FirstOrDefault();

            return Ok(results);
        }

        // PUT: api/MunitionsStatusUpdate/{guid}
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMunitionsStatusUpdate(string id, MunitionsInventoryStatusUpdateDTO munitionStatusUpdate)
        {
            if (id != munitionStatusUpdate.id)
            {
                return BadRequest();
            }

            MunitionStatu status = db.MunitionStatus.Where(x => x.MunitionInventoryID == id).FirstOrDefault();

            if (status == null)
            {
                return BadRequest();
            }

            if (!db.StatusCodes.Any(x => x.id == munitionStatusUpdate.StatusCode && x.type == 3))
            {
                return BadRequest("Invalid Status Code");
            }
            else
            {
                status.StatusCode = munitionStatusUpdate.StatusCode;
            }

            status.ETIC = munitionStatusUpdate.ETIC;
            status.StatusComments = munitionStatusUpdate.Remark;
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

        private bool MunitionStatuExists(string id)
        {
            return db.MunitionStatus.Count(e => e.MunitionInventoryID == id) > 0;
        }
    }
}