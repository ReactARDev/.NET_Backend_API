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
    public class RegionsController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/Regions
        public IQueryable<DropDownDTO> GetRegions()
        {
            var result = (from a in db.Regions
                          orderby a.displayOrder ascending
                          select new DropDownDTO { id = a.id.ToString(), description = a.description }).AsQueryable();
            return result;
        }

        // GET: api/Regions/5
        [ResponseType(typeof(Region))]
        public IHttpActionResult GetRegions(int id)
        {
            Region regions = db.Regions.Find(id);
            if (regions == null)
            {
                return NotFound();
            }

            return Ok(new DropDownDTO
            {
                id = regions.id.ToString(),
                description = regions.description
            });
        }

        // PUT: api/Regions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRegions(int id, Region regions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != regions.id)
            {
                return BadRequest();
            }

            db.Entry(regions).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegionsExists(id))
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

        // POST: api/Regions
        [ResponseType(typeof(Region))]
        public IHttpActionResult PostRegions(Region regions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Regions.Add(regions);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApiPost", new { id = regions.id }, regions);
        }

        // DELETE: api/Regions/5
        [ResponseType(typeof(Region))]
        public IHttpActionResult DeleteRegions(int id)
        {
            Region regions = db.Regions.Find(id);
            if (regions == null)
            {
                return NotFound();
            }

            db.Regions.Remove(regions);
            db.SaveChanges();

            return Ok(regions);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RegionsExists(int id)
        {
            return db.Regions.Count(e => e.id == id) > 0;
        }
    }
}