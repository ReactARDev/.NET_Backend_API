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
using System.Web.Http.Cors;

namespace AMPS9000_WebAPI.Controllers
{
    public class UnitsController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/Units
        public IQueryable<DropDownDTO> GetUnits()
        {
            var result = (from a in db.Units
                          orderby a.DisplayOrder ascending
                          select new DropDownDTO { id = a.id.ToString(), description = a.description }).AsQueryable();
            return result;
        }

        // GET: api/Units/5
        [ResponseType(typeof(Unit))]
        public IHttpActionResult GetUnits(int id)
        {
            Unit units = db.Units.Find(id);
            if (units == null)
            {
                return NotFound();
            }

            return Ok(new DropDownDTO
            {
                id = units.id.ToString(),
                description = units.description
            });
        }

        // PUT: api/Units/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUnits(int id, Unit units)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != units.id)
            {
                return BadRequest();
            }

            db.Entry(units).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitsExists(id))
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

        // POST: api/Units
        [ResponseType(typeof(Unit))]
        public IHttpActionResult PostUnits(Unit units)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Units.Add(units);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApiPost", new { id = units.id }, units);
        }

        // DELETE: api/Units/5
        [ResponseType(typeof(Unit))]
        public IHttpActionResult DeleteUnits(int id)
        {
            Unit units = db.Units.Find(id);
            if (units == null)
            {
                return NotFound();
            }

            db.Units.Remove(units);
            db.SaveChanges();

            return Ok(units);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UnitsExists(int id)
        {
            return db.Units.Count(e => e.id == id) > 0;
        }
    }
}