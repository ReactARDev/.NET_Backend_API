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
    public class ObjectiveController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/Objective
        public IQueryable<DropDownDTO> GetObjectives()
        {
            var result = (from a in db.Objectives
                          orderby a.objectiveName ascending
                          select new DropDownDTO { id = a.id.ToString(), description = a.objectiveName  }).AsQueryable();
            return result;
        }

        // GET: api/Objective/5
        [ResponseType(typeof(Objective))]
        public IHttpActionResult GetObjective(int id)
        {
            Objective Objective = db.Objectives.Find(id);
            if (Objective == null)
            {
                return NotFound();
            }

            return Ok(Objective);
        }

        // PUT: api/PutObjective/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutObjective(int id, Objective Objective)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Objective.id)
            {
                return BadRequest();
            }

            db.Entry(Objective).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObjectiveExists(id))
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

        // POST: api/Objective
        [ResponseType(typeof(Objective))]
        public IHttpActionResult PostObjective(Objective Objective)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Objectives.Add(Objective);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApiPost", new { id = Objective.id }, Objective);
        }

        // DELETE: api/Objective/5
        [ResponseType(typeof(Objective))]
        public IHttpActionResult DeleteObjective(int id)
        {
            Objective Objective = db.Objectives.Find(id);
            if (Objective == null)
            {
                return NotFound();
            }

            db.Objectives.Remove(Objective);
            db.SaveChanges();

            return Ok(Objective);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ObjectiveExists(int id)
        {
            return db.Objectives.Count(e => e.id == id) > 0;
        }
    }
}