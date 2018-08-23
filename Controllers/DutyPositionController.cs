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
    public class DutyPositionController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/DutyPosition
        public IQueryable<DropDownDTO> GetDutyPositions()
        {
            var result = (from a in db.DutyPositions
                          orderby a.displayOrder, a.description ascending
                          select new DropDownDTO { id = a.id.ToString(), description = a.description.Trim() }).AsQueryable();
            return result;
        }

        // GET: api/DutyPosition/5
        [ResponseType(typeof(DutyPosition))]
        public IHttpActionResult GetDutyPosition(int id)
        {
            DutyPosition dutyPosition = db.DutyPositions.Find(id);
            if (dutyPosition == null)
            {
                return NotFound();
            }

            return Ok(new DropDownDTO
            {
                id = dutyPosition.id.ToString(),
                description = dutyPosition.description
            });
        }

        // PUT: api/DutyPosition/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDutyPosition(int id, DutyPosition dutyPosition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dutyPosition.id)
            {
                return BadRequest();
            }

            db.Entry(dutyPosition).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DutyPositionExists(id))
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

        // POST: api/DutyPosition
        [ResponseType(typeof(DutyPosition))]
        public IHttpActionResult PostDutyPosition(DutyPosition dutyPosition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DutyPositions.Add(dutyPosition);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DutyPositionExists(dutyPosition.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApiPost", new { id = dutyPosition.id }, dutyPosition);
        }

        // DELETE: api/DutyPosition/5
        [ResponseType(typeof(DutyPosition))]
        public IHttpActionResult DeleteDutyPosition(int id)
        {
            DutyPosition dutyPosition = db.DutyPositions.Find(id);
            if (dutyPosition == null)
            {
                return NotFound();
            }

            db.DutyPositions.Remove(dutyPosition);
            db.SaveChanges();

            return Ok(dutyPosition);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DutyPositionExists(int id)
        {
            return db.DutyPositions.Count(e => e.id == id) > 0;
        }
    }
}