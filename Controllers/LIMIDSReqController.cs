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
    public class LIMIDSReqController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/LIMIDSReq
        public IQueryable<LIMIDSReq> GetLIMIDSReqs()
        {
            return db.LIMIDSReqs;
        }

        // GET: api/LIMIDSReq/5
        [ResponseType(typeof(LIMIDSReq))]
        public IHttpActionResult GetLIMIDSReq(int id)
        {
            LIMIDSReq lIMIDSReq = db.LIMIDSReqs.Find(id);
            if (lIMIDSReq == null)
            {
                return NotFound();
            }

            return Ok(lIMIDSReq);
        }

        // PUT: api/LIMIDSReq/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLIMIDSReq(int id, LIMIDSReq lIMIDSReq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lIMIDSReq.id)
            {
                return BadRequest();
            }

            db.Entry(lIMIDSReq).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LIMIDSReqExists(id))
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

        // POST: api/LIMIDSReq
        [ResponseType(typeof(LIMIDSReq))]
        public IHttpActionResult PostLIMIDSReq(LIMIDSReq lIMIDSReq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LIMIDSReqs.Add(lIMIDSReq);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApiPost", new { id = lIMIDSReq.id }, lIMIDSReq);
        }

        // DELETE: api/LIMIDSReq/5
        [ResponseType(typeof(LIMIDSReq))]
        public IHttpActionResult DeleteLIMIDSReq(int id)
        {
            LIMIDSReq lIMIDSReq = db.LIMIDSReqs.Find(id);
            if (lIMIDSReq == null)
            {
                return NotFound();
            }

            db.LIMIDSReqs.Remove(lIMIDSReq);
            db.SaveChanges();

            return Ok(lIMIDSReq);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LIMIDSReqExists(int id)
        {
            return db.LIMIDSReqs.Count(e => e.id == id) > 0;
        }
    }
}