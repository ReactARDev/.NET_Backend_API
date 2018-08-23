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
    public class GroundControlSystemController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/GroundControlSystem
        public IQueryable<DropDownDTO> GetGroundControlSystems()
        {
            var result = (from a in db.GroundControlSystems
                          orderby a.displayOrder ascending
                          select new DropDownDTO { id = a.id.ToString(), description = a.description.Trim() }).AsQueryable();

            return result;
        }

        // GET: api/GroundControlSystem/5
        [ResponseType(typeof(GroundControlSystem))]
        public IHttpActionResult GetGroundControlSystem(int id)
        {
            GroundControlSystem groundControlSystem = db.GroundControlSystems.Find(id);
            if (groundControlSystem == null)
            {
                return NotFound();
            }

            return Ok(groundControlSystem);
        }

        // PUT: api/GroundControlSystem/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGroundControlSystem(int id, GroundControlSystem groundControlSystem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != groundControlSystem.id)
            {
                return BadRequest();
            }

            db.Entry(groundControlSystem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroundControlSystemExists(id))
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

        // POST: api/GroundControlSystem
        [ResponseType(typeof(GroundControlSystem))]
        public IHttpActionResult PostGroundControlSystem(GroundControlSystem groundControlSystem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GroundControlSystems.Add(groundControlSystem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = groundControlSystem.id }, groundControlSystem);
        }

        // DELETE: api/GroundControlSystem/5
        [ResponseType(typeof(GroundControlSystem))]
        public IHttpActionResult DeleteGroundControlSystem(int id)
        {
            GroundControlSystem groundControlSystem = db.GroundControlSystems.Find(id);
            if (groundControlSystem == null)
            {
                return NotFound();
            }

            db.GroundControlSystems.Remove(groundControlSystem);
            db.SaveChanges();

            return Ok(groundControlSystem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GroundControlSystemExists(int id)
        {
            return db.GroundControlSystems.Count(e => e.id == id) > 0;
        }
    }
}