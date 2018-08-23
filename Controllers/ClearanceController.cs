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
    public class ClearanceController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/IC_ISM_Classifications
        public IQueryable<DropDownDTO> GetIC_ISM_Classifications()
        {
            var result = (from a in db.IC_ISM_Classifications
                          select new DropDownDTO { id = a.ClassificationMarkingValue, description = a.Description}).AsQueryable();
            return result;
        }

        // GET: api/IC_ISM_Classifications/5
        [ResponseType(typeof(IC_ISM_Classifications))]
        public IHttpActionResult GetIC_ISM_Classifications(string id)
        {
            IC_ISM_Classifications iC_ISM_Classifications = db.IC_ISM_Classifications.Find(id);
            if (iC_ISM_Classifications == null)
            {
                return NotFound();
            }

            return Ok(new DropDownDTO {
                id = iC_ISM_Classifications.ClassificationMarkingValue,
                description = iC_ISM_Classifications.Description
            });
        }

        // PUT: api/IC_ISM_Classifications/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIC_ISM_Classifications(string id, IC_ISM_Classifications iC_ISM_Classifications)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iC_ISM_Classifications.ClassificationMarkingValue)
            {
                return BadRequest();
            }

            db.Entry(iC_ISM_Classifications).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IC_ISM_ClassificationsExists(id))
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

        // POST: api/IC_ISM_Classifications
        [ResponseType(typeof(IC_ISM_Classifications))]
        public IHttpActionResult PostIC_ISM_Classifications(IC_ISM_Classifications iC_ISM_Classifications)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IC_ISM_Classifications.Add(iC_ISM_Classifications);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (IC_ISM_ClassificationsExists(iC_ISM_Classifications.ClassificationMarkingValue))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApiPost", new { id = iC_ISM_Classifications.ClassificationMarkingValue }, iC_ISM_Classifications);
        }

        // DELETE: api/IC_ISM_Classifications/5
        [ResponseType(typeof(IC_ISM_Classifications))]
        public IHttpActionResult DeleteIC_ISM_Classifications(string id)
        {
            IC_ISM_Classifications iC_ISM_Classifications = db.IC_ISM_Classifications.Find(id);
            if (iC_ISM_Classifications == null)
            {
                return NotFound();
            }

            db.IC_ISM_Classifications.Remove(iC_ISM_Classifications);
            db.SaveChanges();

            return Ok(iC_ISM_Classifications);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IC_ISM_ClassificationsExists(string id)
        {
            return db.IC_ISM_Classifications.Count(e => e.ClassificationMarkingValue == id) > 0;
        }
    }
}