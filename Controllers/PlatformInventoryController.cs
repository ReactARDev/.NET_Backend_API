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
    public class PlatformInventoryController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/PlatformInventory
        public IHttpActionResult GetPlatformInventory()
        {
            var results = (from a in db.PlatformInventory
                           join b in db.Platforms on a.metaDataID equals b.PlatformID
                           orderby a.lastUpdate descending, b.PlatformName ascending, a.tailNumber ascending
                           select new DropDownDTO
                           {
                               id = a.id,
                               description = b.PlatformName + " - " + a.tailNumber
                           });

            return Ok(results);
        }

        // GET: api/PlatformInventoryData
        public IHttpActionResult GetPlatformInventoryData()
        {
            var results = (from a in db.PlatformInventory
                           join b0 in db.Platforms on a.metaDataID equals b0.PlatformID
                           join c0 in db.Companies on b0.PlatformManufacturer equals c0.id into lojMan
                           from c1 in lojMan.DefaultIfEmpty()
                           join c in db.Locations on a.locationID equals c.LocationID 
                           join d in db.Units on a.owningUnit equals d.id into lojUnit
                           join e in db.PlatformCategories on b0.PlatformCategory equals e.id into lojPCatg
                           from e1 in lojPCatg.DefaultIfEmpty()
                           from g in lojUnit.DefaultIfEmpty()
                           join j in db.BranchOfServices on a.branch equals j.id into lojBranch
                           from j1 in lojBranch.DefaultIfEmpty()
                           join k in db.COCOMs on a.COCOM equals k.id into lojCC
                           from k1 in lojCC.DefaultIfEmpty()
                           orderby a.lastUpdate descending
                           select new 
                           {
                               id = a.id,
                               tailNbr = a.tailNumber ?? "Unknown",
                               manufacturer = c1.description.Trim() ?? "Unknown",
                               name = b0.PlatformName + " - " + a.tailNumber,
                               category = e1.description ?? "Unknown",
                               branchOfService = j1.description.Trim() ?? "Unknown",
                               COCOM = k1.description.Trim() ?? "Unknown",
                               owningUnit = g.description ?? "Unknown",
                               location = c.LocationName ?? "Unknown"
                           });

            return Ok(results);
        }

        // GET: api/PlatformInventory/{guid}
        [ResponseType(typeof(PlatformInventory))]
        public IHttpActionResult GetPlatformInventory(string id)
        {
            PlatformInventory platformInventory = db.PlatformInventory.Find(id);
            if (platformInventory == null)
            {
                return NotFound();
            }

            return Ok(platformInventory);
        }

        // PUT: api/PlatformInventory/{guid}
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlatformInventory(string id, PlatformInventory platformInventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != platformInventory.id)
            {
                return BadRequest();
            }

            if(!db.Platforms.Any(x => x.PlatformID == platformInventory.metaDataID))
            {
                return BadRequest("Invalid Platform Specification ID (metaDataID)");
            }

            if (!db.Locations.Any(x => x.LocationID == platformInventory.locationID))
            {
                return BadRequest("Invalid Location ID");
            }

            if (!db.Units.Any(x => x.id == platformInventory.owningUnit))
            {
                return BadRequest("Invalid Owning Unit ID");
            }

            if (platformInventory.tailNumber != null && platformInventory.tailNumber.Length > 50)
            {
                return BadRequest("Tail Number is too long");
            }

            if(platformInventory.payload1 != null && db.PayloadInventory.Where(x => x.id == platformInventory.payload1).Count() <= 0)
            {
                return BadRequest("Invalid Payload ID - Payload 1");
            }

            if (platformInventory.payload2 != null && db.PayloadInventory.Where(x => x.id == platformInventory.payload2).Count() <= 0)
            {
                return BadRequest("Invalid Payload ID - Payload 2");
            }

            if (platformInventory.payload3 != null && db.PayloadInventory.Where(x => x.id == platformInventory.payload3).Count() <= 0)
            {
                return BadRequest("Invalid Payload ID - Payload 3");
            }

            if(platformInventory.armament1 != null && db.MunitionsInventory.Where(x => x.id == platformInventory.armament1).Count() <= 0)
            {
                return BadRequest("Invalid Armament ID - Armament 1");
            }

            if (platformInventory.armament2 != null && db.MunitionsInventory.Where(x => x.id == platformInventory.armament2).Count() <= 0)
            {
                return BadRequest("Invalid Armament ID - Armament 2");
            }

            if (platformInventory.armament3 != null && db.MunitionsInventory.Where(x => x.id == platformInventory.armament3).Count() <= 0)
            {
                return BadRequest("Invalid Armament ID - Armament 3");
            }

            if(platformInventory.coms1 != null && db.ComsTypes.Where(x => x.id == platformInventory.coms1).Count() <= 0)
            {
                return BadRequest("Invalid Coms Type - COMS1");
            }

            if (platformInventory.coms2 != null && db.ComsTypes.Where(x => x.id == platformInventory.coms2).Count() <= 0)
            {
                return BadRequest("Invalid Coms Type - COMS2");
            }

            if(platformInventory.branch != null)
            {
                if(!db.BranchOfServices.Any(x => x.id == platformInventory.branch))
                {
                    return BadRequest("Invalid Branch");
                }
            }

            if(platformInventory.COCOM != null)
            {
                if(!db.COCOMs.Any(x => x.id == platformInventory.COCOM))
                {
                    return BadRequest("Invalid COCOM");
                }
            }

            platformInventory.lastUpdate = DateTime.Now;
            platformInventory.lastUpdateUserId = "000";

            db.Entry(platformInventory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlatformInventoryExists(id))
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

        // POST: api/PlatformInventory
        [ResponseType(typeof(PlatformInventory))]
        public IHttpActionResult PostPlatformInventory(PlatformInventory platformInventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (db.Platforms.Where(x => x.PlatformID == platformInventory.metaDataID).Count() <= 0)
            {
                return BadRequest("Invalid Platform Specification ID (metaDataID)");
            }

            if (db.Locations.Where(x => x.LocationID == platformInventory.locationID).Count() <= 0)
            {
                return BadRequest("Invalid Location ID");
            }

            if (db.Units.Where(x => x.id == platformInventory.owningUnit).Count() <= 0)
            {
                return BadRequest("Invalid Owning Unit ID");
            }

            if (platformInventory.tailNumber != null && platformInventory.tailNumber.Length > 50)
            {
                return BadRequest("Tail Number is too long");
            }

            if (platformInventory.payload1 != null && db.PayloadInventory.Where(x => x.id == platformInventory.payload1).Count() <= 0)
            {
                return BadRequest("Invalid Payload ID - Payload 1");
            }

            if (platformInventory.payload2 != null && db.PayloadInventory.Where(x => x.id == platformInventory.payload2).Count() <= 0)
            {
                return BadRequest("Invalid Payload ID - Payload 2");
            }

            if (platformInventory.payload3 != null && db.PayloadInventory.Where(x => x.id == platformInventory.payload3).Count() <= 0)
            {
                return BadRequest("Invalid Payload ID - Payload 3");
            }

            if (platformInventory.armament1 != null && db.MunitionsInventory.Where(x => x.id == platformInventory.armament1).Count() <= 0)
            {
                return BadRequest("Invalid Armament ID - Armament 1");
            }

            if (platformInventory.armament2 != null && db.MunitionsInventory.Where(x => x.id == platformInventory.armament2).Count() <= 0)
            {
                return BadRequest("Invalid Armament ID - Armament 2");
            }

            if (platformInventory.armament3 != null && db.MunitionsInventory.Where(x => x.id == platformInventory.armament3).Count() <= 0)
            {
                return BadRequest("Invalid Armament ID - Armament 3");
            }

            if (platformInventory.coms1 != null && db.ComsTypes.Where(x => x.id == platformInventory.coms1).Count() <= 0)
            {
                return BadRequest("Invalid Coms Type - COMS1");
            }

            if (platformInventory.coms2 != null && db.ComsTypes.Where(x => x.id == platformInventory.coms2).Count() <= 0)
            {
                return BadRequest("Invalid Coms Type - COMS2");
            }

            if (platformInventory.branch != null)
            {
                if (!db.BranchOfServices.Any(x => x.id == platformInventory.branch))
                {
                    return BadRequest("Invalid Branch");
                }
            }

            if (platformInventory.COCOM != null)
            {
                if (!db.COCOMs.Any(x => x.id == platformInventory.COCOM))
                {
                    return BadRequest("Invalid COCOM");
                }
            }

            platformInventory.id = Guid.NewGuid().ToString();
            platformInventory.lastUpdate = DateTime.Now;
            platformInventory.lastUpdateUserId = "000";

            db.PlatformInventory.Add(platformInventory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PlatformInventoryExists(platformInventory.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            db.PlatformStatus.Add(new PlatformStatu
            {
                PlatformInventoryID = platformInventory.id,
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

            return CreatedAtRoute("DefaultApiPost", new { id = platformInventory.id }, platformInventory);
        }

        // DELETE: api/PlatformInventory/5
        [ResponseType(typeof(PlatformInventory))]
        public IHttpActionResult DeletePlatformInventory(string id)
        {
            PlatformInventory platformInventory = db.PlatformInventory.Find(id);
            if (platformInventory == null)
            {
                return NotFound();
            }

            db.PlatformInventory.Remove(platformInventory);
            db.SaveChanges();

            return Ok(platformInventory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlatformInventoryExists(string id)
        {
            return db.PlatformInventory.Count(e => e.id == id) > 0;
        }
    }
}