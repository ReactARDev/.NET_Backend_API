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
    public class MunitionRolesController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/MunitionRoles
        public IQueryable<DropDownDTO> GetMunitionRoles()
        {
            var result = (from a in db.MunitionRoles
                          orderby a.displayOrder ascending
                          select new DropDownDTO { id = a.id.ToString(), description = a.description }).AsQueryable();
            return result;
        }

        // GET: api/MunitionRoles/5
        [ResponseType(typeof(MunitionRole))]
        public IHttpActionResult GetMunitionRoles(int id)
        {
            MunitionRole munitionRoles = db.MunitionRoles.Find(id);
            if (munitionRoles == null)
            {
                return NotFound();
            }

            return Ok(new DropDownDTO
            {
                id = munitionRoles.id.ToString(),
                description = munitionRoles.description
            });
        }

        // PUT: api/MunitionRoles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMunitionRoles(int id, MunitionRole munitionRoles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != munitionRoles.id)
            {
                return BadRequest();
            }

            db.Entry(munitionRoles).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MunitionRolesExists(id))
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

        // POST: api/MunitionRoles
        [ResponseType(typeof(MunitionRole))]
        public IHttpActionResult PostMunitionRoles(MunitionRole munitionRoles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MunitionRoles.Add(munitionRoles);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MunitionRolesExists(munitionRoles.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApiPost", new { id = munitionRoles.id }, munitionRoles);
        }

        // DELETE: api/MunitionRoles/5
        [ResponseType(typeof(MunitionRole))]
        public IHttpActionResult DeleteMunitionRoles(int id)
        {
            MunitionRole munitionRoles = db.MunitionRoles.Find(id);
            if (munitionRoles == null)
            {
                return NotFound();
            }

            db.MunitionRoles.Remove(munitionRoles);
            db.SaveChanges();

            return Ok(munitionRoles);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MunitionRolesExists(int id)
        {
            return db.MunitionRoles.Count(e => e.id == id) > 0;
        }
    }
}