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
    public class EEIThreatController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/EEIThreat
        public IQueryable<DropDownDTO> GetEEIThreats()
        {
            var result = (from a in db.EEIThreats
                          orderby a.displayOrder ascending
                          select new DropDownDTO { id = a.id.ToString(), description = a.description }).AsQueryable();
            return result;
        }

        // GET: api/EEIThreat/5
        [ResponseType(typeof(EEIThreat))]
        public IHttpActionResult GetEEIThreat(int id)
        {
            EEIThreat eEIThreat = db.EEIThreats.Find(id);
            if (eEIThreat == null)
            {
                return NotFound();
            }

            return Ok(eEIThreat);
        }

        // PUT: api/EEIThreat/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEEIThreat(int id, EEIThreat eEIThreat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eEIThreat.id)
            {
                return BadRequest();
            }

            db.Entry(eEIThreat).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EEIThreatExists(id))
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

        // POST: api/EEIThreat
        [ResponseType(typeof(EEIThreat))]
        public IHttpActionResult PostEEIThreat(EEIThreat eEIThreat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EEIThreats.Add(eEIThreat);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApiPost", new { id = eEIThreat.id }, eEIThreat);
        }

        // DELETE: api/EEIThreat/5
        [ResponseType(typeof(EEIThreat))]
        public IHttpActionResult DeleteEEIThreat(int id)
        {
            EEIThreat eEIThreat = db.EEIThreats.Find(id);
            if (eEIThreat == null)
            {
                return NotFound();
            }

            db.EEIThreats.Remove(eEIThreat);
            db.SaveChanges();

            return Ok(eEIThreat);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EEIThreatExists(int id)
        {
            return db.EEIThreats.Count(e => e.id == id) > 0;
        }
    }
}