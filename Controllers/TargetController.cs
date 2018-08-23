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
    public class TargetController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/Target
        public IQueryable<DropDownDTO> GetTargets()
        {
            var result = (from a in db.Targets
                          orderby a.targetName ascending
                          select new DropDownDTO { id = a.id.ToString(), description = a.targetNumber + " - " + a.targetName.Trim() }).AsQueryable();
            return result;
        }

        // GET: api/Target/5
        [ResponseType(typeof(Target))]
        public IHttpActionResult GetTarget(int id)
        {
            Target Target = db.Targets.Find(id);
            if (Target == null)
            {
                return NotFound();
            }

            return Ok(Target);
        }

        // PUT: api/PutTarget/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTarget(int id, Target Target)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Target.id)
            {
                return BadRequest();
            }

            db.Entry(Target).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TargetExists(id))
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

        // POST: api/Target
        [ResponseType(typeof(Target))]
        public IHttpActionResult PostTarget(Target Target)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Targets.Add(Target);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApiPost", new { id = Target.id }, Target);
        }

        // DELETE: api/Target/5
        [ResponseType(typeof(Target))]
        public IHttpActionResult DeleteTarget(int id)
        {
            Target Target = db.Targets.Find(id);
            if (Target == null)
            {
                return NotFound();
            }

            db.Targets.Remove(Target);
            db.SaveChanges();

            return Ok(Target);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TargetExists(int id)
        {
            return db.Targets.Count(e => e.id == id) > 0;
        }
    }
}