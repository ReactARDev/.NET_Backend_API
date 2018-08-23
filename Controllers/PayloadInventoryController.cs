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
using System.Data.Entity.SqlServer;

namespace AMPS9000_WebAPI.Controllers
{
    public class PayloadInventoryController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/PayloadInventory
        public IQueryable<DropDownDTO> GetPayloadInventory()
        {
            var results = (from a in db.PayloadInventory
                           join b in db.Payloads on a.metaDataID equals b.PayloadID
                           orderby a.lastUpdate descending, b.PayloadName ascending, a.serialNumber ascending
                           select new DropDownDTO { id = a.id, description = b.PayloadName + " - " + a.serialNumber });

            return results;
        }

        // GET: api/PayloadInventoryData
        public IHttpActionResult GetPayloadInventoryData()
        {
            var results = (from a in db.PayloadInventory
                           join b in db.Payloads on a.metaDataID equals b.PayloadID
                           join c in db.PayloadTypes on b.PayloadType equals c.id into lojType
                           from d in lojType.DefaultIfEmpty()
                           join e in db.Units on a.owningUnit equals e.id into lojUnit
                           from f in lojUnit.DefaultIfEmpty()
                           join g2 in db.BranchOfServices on a.branch equals g2.id into lojBranch
                           from g3 in lojBranch.DefaultIfEmpty()
                           join h2 in db.COCOMs on a.COCOM equals h2.id into lojCC
                           from h3 in lojCC.DefaultIfEmpty()
                           join i in db.Locations on a.locationID equals i.LocationID                           
                           join j2 in db.Companies on b.PayloadManufacturer equals j2.id into lojComp
                           from j3 in lojComp.DefaultIfEmpty()
                           orderby a.lastUpdate descending
                           select new
                           {
                               ID = a.id,
                               typeDesc = d.description ?? "Unknown",
                               manufacturer = j3.description ?? "Unknown",
                               name = b.PayloadName, 
                               serialNumber = a.serialNumber ?? "Unknown",
                               branch = g3.description ?? "Unknown",
                               COCOM = h3.description ?? "Unknown",
                               owningUnit = f.description ?? "Unknown",
                               location = i.LocationName,
                           });

            return Ok(results);
        }

        // GET: api/PayloadInventory/{guid}
        [ResponseType(typeof(PayloadInventory))]
        public IHttpActionResult GetPayloadInventory(string id)
        {
            PayloadInventory payloadInventory = db.PayloadInventory.Find(id);
            if (payloadInventory == null)
            {
                return NotFound();
            }

            return Ok(payloadInventory);
        }

        // PUT: api/PayloadInventory/{guid}
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPayloadInventory(string id, PayloadInventory payloadInventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != payloadInventory.id)
            {
                return BadRequest();
            }

            if(!db.Payloads.Any(x => x.PayloadID == payloadInventory.metaDataID))
            {
                return BadRequest("Invalid Payload Specification ID (metaDataID)");
            }

            if (!db.Locations.Any(x => x.LocationID == payloadInventory.locationID))
            {
                return BadRequest("Invalid Location ID");
            }

            if (!db.Units.Any(x => x.id == payloadInventory.owningUnit))
            {
                return BadRequest("Invalid Owning Unit ID");
            }

            if (payloadInventory.serialNumber != null && payloadInventory.serialNumber.Length > 50)
            {
                return BadRequest("Serial Number is too long");
            }

            if(payloadInventory.branch != null)
            {
                if(!db.BranchOfServices.Any(x => x.id == payloadInventory.branch))
                {
                    return BadRequest("Invalid Branch");
                }
            }

            if(payloadInventory.COCOM != null)
            {
                if(!db.COCOMs.Any(x => x.id == payloadInventory.COCOM))
                {
                    return BadRequest("Invalid COCOM");
                }
            }

            payloadInventory.lastUpdate = DateTime.Now;
            payloadInventory.lastUpdateUserId = "000";

            db.Entry(payloadInventory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayloadInventoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PayloadInventory
        [ResponseType(typeof(PayloadInventory))]
        public IHttpActionResult PostPayloadInventory(PayloadInventory payloadInventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (db.Payloads.Where(x => x.PayloadID == payloadInventory.metaDataID).Count() <= 0)
            {
                return BadRequest("Invalid Payload Specification ID (metaDataID)");
            }

            if (db.Locations.Where(x => x.LocationID == payloadInventory.locationID).Count() <= 0)
            {
                return BadRequest("Invalid Location ID");
            }

            if (db.Units.Where(x => x.id == payloadInventory.owningUnit).Count() <= 0)
            {
                return BadRequest("Invalid Owning Unit ID");
            }

            if (payloadInventory.serialNumber != null && payloadInventory.serialNumber.Length > 50)
            {
                return BadRequest("Serial Number is too long");
            }

            if (payloadInventory.branch != null)
            {
                if (!db.BranchOfServices.Any(x => x.id == payloadInventory.branch))
                {
                    return BadRequest("Invalid Branch");
                }
            }

            if (payloadInventory.COCOM != null)
            {
                if (!db.COCOMs.Any(x => x.id == payloadInventory.COCOM))
                {
                    return BadRequest("Invalid COCOM");
                }
            }

            payloadInventory.id = Guid.NewGuid().ToString();
            payloadInventory.lastUpdate = DateTime.Now;
            payloadInventory.lastUpdateUserId = "000";

            db.PayloadInventory.Add(payloadInventory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PayloadInventoryExists(payloadInventory.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            db.PayloadStatus.Add(new PayloadStatu
            {
                PayloadInventoryID = payloadInventory.id,
                StatusCode = (int)AssetStatuses.FLIGHT_READY,
                ETIC = 0,
                lastUpdate = DateTime.Now,
                lastUpdateUserId = "1"
            });

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return CreatedAtRoute("DefaultApiPost", new { id = payloadInventory.id }, payloadInventory);
        }

        // DELETE: api/PayloadInventory/{guid}
        [ResponseType(typeof(PayloadInventory))]
        public IHttpActionResult DeletePayloadInventory(string id)
        {
            PayloadInventory payloadInventory = db.PayloadInventory.Find(id);
            if (payloadInventory == null)
            {
                return NotFound();
            }

            db.PayloadInventory.Remove(payloadInventory);
            db.SaveChanges();

            return Ok(payloadInventory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PayloadInventoryExists(string id)
        {
            return db.PayloadInventory.Count(e => e.id == id) > 0;
        }
    }
}