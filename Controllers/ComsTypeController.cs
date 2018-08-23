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
    public class ComsTypeController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/ComsType
        public IQueryable<DropDownDTO> GetComsTypes()
        {
            var result = (from a in db.ComsTypes
                          orderby a.displayOrder ascending
                          select new DropDownDTO { id = a.id.ToString(), description = a.description.Trim() }).AsQueryable();
            return result;
        }

        // GET: api/ComsType/5
        [ResponseType(typeof(ComsType))]
        public IHttpActionResult GetComsType(int id)
        {
            ComsType comsType = db.ComsTypes.Find(id);
            if (comsType == null)
            {
                return NotFound();
            }

            return Ok(comsType);
        }

        // PUT: api/ComsType/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComsType(int id, ComsType comsType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comsType.id)
            {
                return BadRequest();
            }

            db.Entry(comsType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComsTypeExists(id))
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

        // POST: api/ComsType
        [ResponseType(typeof(ComsType))]
        public IHttpActionResult PostComsType(ComsType comsType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ComsTypes.Add(comsType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = comsType.id }, comsType);
        }

        // DELETE: api/ComsType/5
        [ResponseType(typeof(ComsType))]
        public IHttpActionResult DeleteComsType(int id)
        {
            ComsType comsType = db.ComsTypes.Find(id);
            if (comsType == null)
            {
                return NotFound();
            }

            db.ComsTypes.Remove(comsType);
            db.SaveChanges();

            return Ok(comsType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComsTypeExists(int id)
        {
            return db.ComsTypes.Count(e => e.id == id) > 0;
        }
    }
}