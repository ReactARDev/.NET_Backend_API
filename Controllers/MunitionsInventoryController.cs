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
    public class MunitionsInventoryController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/MunitionsInventory
        public IQueryable<DropDownDTO> GetMunitionsInventory()
        {
            var results = (from a in db.MunitionsInventory
                           join b in db.Munitions on a.metaDataID equals b.MunitionID
                           orderby a.lastUpdate descending, b.MunitionName ascending, a.serialNumber ascending
                           select new DropDownDTO { id = a.id, description = b.MunitionName + " - " + a.serialNumber });

            return results;
        }

        // GET: api/MunitionsInventoryData
        public IHttpActionResult GetMunitionsInventoryData()
        {
            var results = (from a in db.MunitionsInventory
                           join b in db.Munitions on a.metaDataID equals b.MunitionID
                           join c in db.MunitionRoles on b.MunitionRole equals c.id into lojRole
                           from d in lojRole.DefaultIfEmpty()
                           join e in db.Units on a.owningUnit equals e.id into lojUnit
                           from f in lojUnit.DefaultIfEmpty()
                           join h2 in db.BranchOfServices on a.branch equals h2.id into lojBranch
                           from h3 in lojBranch.DefaultIfEmpty()
                           join i0 in db.COCOMs on a.COCOM equals i0.id into lojCC
                           from i1 in lojCC.DefaultIfEmpty()
                           join i in db.Locations on a.locationID equals i.LocationID
                           join j in db.Companies on b.MunitionManufacturer equals j.id into lojComp
                           from k in lojComp.DefaultIfEmpty()
                           orderby a.lastUpdate descending
                           select new
                           {
                               ID = a.id,
                               type = d.description,
                               manufacturer = k.description.Trim() ?? "Unknown",
                               name = b.MunitionName,
                               serialNumber = a.serialNumber ?? "Unknown",
                               branch = h3.description.Trim() ?? "Unknown",
                               COCOM = i1.description.Trim() ?? "Unknown",
                               owningUnit = f.description ?? "Unknown",
                               location = i.LocationName,
                           });

            return Ok(results);
        }

        // GET: api/MunitionsInventory/{guid}
        [ResponseType(typeof(MunitionsInventory))]
        public IHttpActionResult GetMunitionsInventory(string id)
        {
            var munitionsInventory = (from a in db.MunitionsInventory
                           join b in db.Munitions on a.metaDataID equals b.MunitionID
                           join c in db.MunitionRoles on b.MunitionRole equals c.id into lojRole
                           from d in lojRole.DefaultIfEmpty()
                           join e in db.Locations on a.locationID equals e.LocationID into lojLoc
                           from f in lojLoc.DefaultIfEmpty()
                           where a.id == id
                           select new 
                           {
                               id = a.id,
                               type = b.MunitionRole,
                               lastUpdateUserId = a.lastUpdateUserId,
                               serialNumber = a.serialNumber,
                               owningUnit = a.owningUnit,
                               locationID = a.locationID,
                               locationCatg = f.LocationCategory,
                               lastUpdate = a.lastUpdate,
                               metaDataID = a.metaDataID,
                               branch = a.branch,
                               COCOM = a.COCOM
                           }).FirstOrDefault();

            if (munitionsInventory == null)
            {
                return NotFound();
            }

            return Ok(munitionsInventory);
        }

        // PUT: api/MunitionsInventory/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMunitionsInventory(string id, MunitionsInventory munitionsInventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != munitionsInventory.id)
            {
                return BadRequest();
            }

            if (db.Munitions.Where(x => x.MunitionID == munitionsInventory.metaDataID).Count() <= 0)
            {
                return BadRequest("Invalid Munition Specifications ID (metaDataID)");
            }

            if (db.Locations.Where(x => x.LocationID == munitionsInventory.locationID).Count() <= 0)
            {
                return BadRequest("Invalid Location ID");
            }

            if (db.Units.Where(x => x.id == munitionsInventory.owningUnit).Count() <= 0)
            {
                return BadRequest("Invalid Owning Unit ID");
            }

            if (munitionsInventory.serialNumber != null && munitionsInventory.serialNumber.Length > 50)
            {
                return BadRequest("Serial Number is too long");
            }

            if(munitionsInventory.branch != null)
            {
                if(!db.BranchOfServices.Any(x => x.id == munitionsInventory.branch))
                {
                    return BadRequest("Invalid Branch");
                }
            }

            if (munitionsInventory.COCOM != null)
            {
                if (!db.COCOMs.Any(x => x.id == munitionsInventory.COCOM))
                {
                    return BadRequest("Invalid COCOM");
                }
            }

            munitionsInventory.lastUpdate = DateTime.Now;
            munitionsInventory.lastUpdateUserId = "000";

            db.Entry(munitionsInventory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MunitionsInventoryExists(id))
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

        // POST: api/MunitionsInventory
        [ResponseType(typeof(MunitionsInventory))]
        public IHttpActionResult PostMunitionsInventory(MunitionsInventory munitionsInventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (db.Munitions.Where(x => x.MunitionID == munitionsInventory.metaDataID).Count() <= 0)
            {
                return BadRequest("Invalid Munition Specifications ID (metaDataID)");
            }

            if (db.Locations.Where(x => x.LocationID == munitionsInventory.locationID).Count() <= 0)
            {
                return BadRequest("Invalid Location ID");
            }

            if (db.Units.Where(x => x.id == munitionsInventory.owningUnit).Count() <= 0)
            {
                return BadRequest("Invalid Owning Unit ID");
            }

            if (munitionsInventory.serialNumber != null && munitionsInventory.serialNumber.Length > 50)
            {
                return BadRequest("Serial Number is too long");
            }

            if (munitionsInventory.branch != null)
            {
                if (!db.BranchOfServices.Any(x => x.id == munitionsInventory.branch))
                {
                    return BadRequest("Invalid Branch");
                }
            }

            if (munitionsInventory.COCOM != null)
            {
                if (!db.COCOMs.Any(x => x.id == munitionsInventory.COCOM))
                {
                    return BadRequest("Invalid COCOM");
                }
            }

            munitionsInventory.id = Guid.NewGuid().ToString();
            munitionsInventory.lastUpdate = DateTime.Now;
            munitionsInventory.lastUpdateUserId = "000";

            db.MunitionsInventory.Add(munitionsInventory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MunitionsInventoryExists(munitionsInventory.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            db.MunitionStatus.Add(new MunitionStatu
            {
                MunitionInventoryID = munitionsInventory.id,
                StatusCode = (int)AssetStatuses.FLIGHT_READY,
                ETIC = 0,
                lastUpdate = DateTime.Now,
                lastUpdateUserId = "000"
            });

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return CreatedAtRoute("DefaultApiPost", new { id = munitionsInventory.id }, munitionsInventory);
        }

        // DELETE: api/MunitionsInventory/{guid}
        [ResponseType(typeof(MunitionsInventory))]
        public IHttpActionResult DeleteMunitionsInventory(string id)
        {
            MunitionsInventory munitionsInventory = db.MunitionsInventory.Find(id);
            if (munitionsInventory == null)
            {
                return NotFound();
            }

            db.MunitionsInventory.Remove(munitionsInventory);
            db.SaveChanges();

            return Ok(munitionsInventory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MunitionsInventoryExists(string id)
        {
            return db.MunitionsInventory.Count(e => e.id == id) > 0;
        }
    }
}