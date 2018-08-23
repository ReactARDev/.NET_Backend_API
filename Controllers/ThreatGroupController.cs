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
    public class ThreatGroupController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/ThreatGroup
        public IQueryable<DropDownDTO> GetThreatGroups()
        {
            var results = (from a in db.ThreatGroups
                           orderby a.displayOrder, a.description
                           select new DropDownDTO { id = a.id.ToString(), description = a.description }).AsQueryable();
            return results;
        }

        // GET: api/ThreatGroup/5
        [ResponseType(typeof(ThreatGroup))]
        public IHttpActionResult GetThreatGroup(int id)
        {
            ThreatGroup threatGroup = db.ThreatGroups.Find(id);
            if (threatGroup == null)
            {
                return NotFound();
            }

            return Ok(threatGroup);
        }

        // PUT: api/ThreatGroup/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutThreatGroup(int id, ThreatGroup threatGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != threatGroup.id)
            {
                return BadRequest();
            }

            db.Entry(threatGroup).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThreatGroupExists(id))
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

        // POST: api/ThreatGroup
        [ResponseType(typeof(ThreatGroup))]
        public IHttpActionResult PostThreatGroup(ThreatGroup threatGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ThreatGroups.Add(threatGroup);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = threatGroup.id }, threatGroup);
        }

        // DELETE: api/ThreatGroup/5
        [ResponseType(typeof(ThreatGroup))]
        public IHttpActionResult DeleteThreatGroup(int id)
        {
            ThreatGroup threatGroup = db.ThreatGroups.Find(id);
            if (threatGroup == null)
            {
                return NotFound();
            }

            db.ThreatGroups.Remove(threatGroup);
            db.SaveChanges();

            return Ok(threatGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ThreatGroupExists(int id)
        {
            return db.ThreatGroups.Count(e => e.id == id) > 0;
        }
    }
}