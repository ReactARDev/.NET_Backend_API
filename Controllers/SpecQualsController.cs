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
    public class SpecQualsController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/SpecQuals
        public IQueryable<DropDownDTO> GetSpecialQualifications()
        {
            var result = (from a in db.SpecialQualifications
                          orderby a.DisplayOrder ascending
                          select new DropDownDTO { id = a.id.ToString(), description = a.description }).AsQueryable();
            return result;
        }

        // GET: api/SpecQuals/5
        [ResponseType(typeof(SpecialQualification))]
        public IHttpActionResult GetSpecialQualifications(int id)
        {
            SpecialQualification specialQualifications = db.SpecialQualifications.Find(id);
            if (specialQualifications == null)
            {
                return NotFound();
            }

            return Ok(new DropDownDTO
            {
                id = specialQualifications.id.ToString(),
                description = specialQualifications.description
            });
        }

        // PUT: api/SpecQuals/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSpecialQualifications(int id, SpecialQualification specialQualifications)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != specialQualifications.id)
            {
                return BadRequest();
            }

            db.Entry(specialQualifications).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecialQualificationsExists(id))
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

        // POST: api/SpecQuals
        [ResponseType(typeof(SpecialQualification))]
        public IHttpActionResult PostSpecialQualifications(SpecialQualification specialQualifications)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SpecialQualifications.Add(specialQualifications);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApiPost", new { id = specialQualifications.id }, specialQualifications);
        }

        // DELETE: api/SpecQuals/5
        [ResponseType(typeof(SpecialQualification))]
        public IHttpActionResult DeleteSpecialQualifications(int id)
        {
            SpecialQualification specialQualifications = db.SpecialQualifications.Find(id);
            if (specialQualifications == null)
            {
                return NotFound();
            }

            db.SpecialQualifications.Remove(specialQualifications);
            db.SaveChanges();

            return Ok(specialQualifications);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SpecialQualificationsExists(int id)
        {
            return db.SpecialQualifications.Count(e => e.id == id) > 0;
        }
    }
}