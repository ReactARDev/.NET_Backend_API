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
    public class PlatformCategoryController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/PlatformCategory
        public IQueryable<DropDownDTO> GetPlatformCategories()
        {
            var result = (from a in db.PlatformCategories
                          orderby a.description ascending
                          select new DropDownDTO { id = a.id.ToString(), description = a.abbreviation + " - " + a.description.Trim() }).AsQueryable();
            return result;
        }

        // GET: api/PlatformCategory/5
        [ResponseType(typeof(PlatformCategory))]
        public IHttpActionResult GetPlatformCategory(int id)
        {
            PlatformCategory platformCategory = db.PlatformCategories.Find(id);
            if (platformCategory == null)
            {
                return NotFound();
            }

            return Ok(platformCategory);
        }

        // PUT: api/PlatformCategory/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlatformCategory(int id, PlatformCategory platformCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != platformCategory.id)
            {
                return BadRequest();
            }

            db.Entry(platformCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlatformCategoryExists(id))
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

        // POST: api/PlatformCategory
        [ResponseType(typeof(PlatformCategory))]
        public IHttpActionResult PostPlatformCategory(PlatformCategory platformCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PlatformCategories.Add(platformCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = platformCategory.id }, platformCategory);
        }

        // DELETE: api/PlatformCategory/5
        [ResponseType(typeof(PlatformCategory))]
        public IHttpActionResult DeletePlatformCategory(int id)
        {
            PlatformCategory platformCategory = db.PlatformCategories.Find(id);
            if (platformCategory == null)
            {
                return NotFound();
            }

            db.PlatformCategories.Remove(platformCategory);
            db.SaveChanges();

            return Ok(platformCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlatformCategoryExists(int id)
        {
            return db.PlatformCategories.Count(e => e.id == id) > 0;
        }
    }
}