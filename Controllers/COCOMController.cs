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
    public class COCOMController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/COCOM
        public IQueryable<DropDownDTO> GetCOCOMs()
        {
            var result = (from a in db.COCOMs
                          orderby a.displayOrder ascending
                          select new DropDownDTO { id = a.id.ToString(), description = a.description }).AsQueryable();
            return result;
        }

        // GET: api/COCOM/5
        [ResponseType(typeof(COCOM))]
        public IHttpActionResult GetCOCOMs(int id)
        {
            COCOM cOCOMs = db.COCOMs.Find(id);
            if (cOCOMs == null)
            {
                return NotFound();
            }

            return Ok(new DropDownDTO
            {
                id = cOCOMs.id.ToString(),
                description = cOCOMs.description
            });
        }

        // PUT: api/COCOM/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCOCOMs(int id, COCOM cOCOMs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cOCOMs.id)
            {
                return BadRequest();
            }

            db.Entry(cOCOMs).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!COCOMsExists(id))
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

        // POST: api/COCOM
        [ResponseType(typeof(COCOM))]
        public IHttpActionResult PostCOCOMs(COCOM cOCOMs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.COCOMs.Add(cOCOMs);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApiPost", new { id = cOCOMs.id }, cOCOMs);
        }

        // DELETE: api/COCOM/5
        [ResponseType(typeof(COCOM))]
        public IHttpActionResult DeleteCOCOMs(int id)
        {
            COCOM cOCOMs = db.COCOMs.Find(id);
            if (cOCOMs == null)
            {
                return NotFound();
            }

            db.COCOMs.Remove(cOCOMs);
            db.SaveChanges();

            return Ok(cOCOMs);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool COCOMsExists(int id)
        {
            return db.COCOMs.Count(e => e.id == id) > 0;
        }
    }
}