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
    public class PlatformRolesController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/PlatformRoles
        public IQueryable<DropDownDTO> GetPlatformRoles()
        {
            var result = (from a in db.PlatformRoles
                          orderby a.DisplayOrder ascending
                          select new DropDownDTO { id = a.id.ToString(), description = a.description }).AsQueryable();
            return result;
        }

        // GET: api/PlatformRoles/5
        [ResponseType(typeof(PlatformRole))]
        public IHttpActionResult GetPlatformRoles(int id)
        {
            PlatformRole platformRoles = db.PlatformRoles.Find(id);
            if (platformRoles == null)
            {
                return NotFound();
            }

            return Ok(new DropDownDTO
            {
                id = platformRoles.id.ToString(),
                description = platformRoles.description
            });
        }

        // PUT: api/PlatformRoles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlatformRoles(int id, PlatformRole platformRoles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != platformRoles.id)
            {
                return BadRequest();
            }

            db.Entry(platformRoles).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlatformRolesExists(id))
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

        // POST: api/PlatformRoles
        [ResponseType(typeof(PlatformRole))]
        public IHttpActionResult PostPlatformRoles(PlatformRole platformRoles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PlatformRoles.Add(platformRoles);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApiPost", new { id = platformRoles.id }, platformRoles);
        }

        // DELETE: api/PlatformRoles/5
        [ResponseType(typeof(PlatformRole))]
        public IHttpActionResult DeletePlatformRoles(int id)
        {
            PlatformRole platformRoles = db.PlatformRoles.Find(id);
            if (platformRoles == null)
            {
                return NotFound();
            }

            db.PlatformRoles.Remove(platformRoles);
            db.SaveChanges();

            return Ok(platformRoles);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlatformRolesExists(int id)
        {
            return db.PlatformRoles.Count(e => e.id == id) > 0;
        }
    }
}