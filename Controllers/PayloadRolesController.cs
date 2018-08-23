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
    public class PayloadRolesController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/PayloadRoles
        public IQueryable<DropDownDTO> GetPayloadRoles()
        {
            var result = (from a in db.PayloadRole
                          orderby a.displayOrder ascending
                          select new DropDownDTO { id = a.id.ToString(), description = a.description }).AsQueryable();
            return result;
        }

        // GET: api/PayloadRoles/5
        [ResponseType(typeof(PayloadRole))]
        public IHttpActionResult GetPayloadRoles(int id)
        {
            PayloadRole payloadRoles = db.PayloadRole.Find(id);
            if (payloadRoles == null)
            {
                return NotFound();
            }

            return Ok(new DropDownDTO
            {
                id = payloadRoles.id.ToString(),
                description = payloadRoles.description
            });
        }

        // PUT: api/PayloadRoles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPayloadRoles(int id, PayloadRole payloadRoles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != payloadRoles.id)
            {
                return BadRequest();
            }

            db.Entry(payloadRoles).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayloadRolesExists(id))
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

        // POST: api/PayloadRoles
        [ResponseType(typeof(PayloadRole))]
        public IHttpActionResult PostPayloadRoles(PayloadRole payloadRoles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PayloadRole.Add(payloadRoles);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PayloadRolesExists(payloadRoles.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApiPost", new { id = payloadRoles.id }, payloadRoles);
        }

        // DELETE: api/PayloadRoles/5
        [ResponseType(typeof(PayloadRole))]
        public IHttpActionResult DeletePayloadRoles(int id)
        {
            PayloadRole payloadRoles = db.PayloadRole.Find(id);
            if (payloadRoles == null)
            {
                return NotFound();
            }

            db.PayloadRole.Remove(payloadRoles);
            db.SaveChanges();

            return Ok(payloadRoles);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PayloadRolesExists(int id)
        {
            return db.PayloadRole.Count(e => e.id == id) > 0;
        }
    }
}