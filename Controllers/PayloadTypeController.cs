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
    public class PayloadTypeController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/PayloadType
        public IQueryable<PayloadType> GetPayloadTypes()
        {
            return db.PayloadTypes;
        }

        // GET: api/PayloadType/5
        [ResponseType(typeof(PayloadType))]
        public IHttpActionResult GetPayloadType(int id)
        {
            PayloadType payloadType = db.PayloadTypes.Find(id);
            if (payloadType == null)
            {
                return NotFound();
            }

            return Ok(payloadType);
        }

        // PUT: api/PayloadType/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPayloadType(int id, PayloadType payloadType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != payloadType.id)
            {
                return BadRequest();
            }

            db.Entry(payloadType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayloadTypeExists(id))
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

        // POST: api/PayloadType
        [ResponseType(typeof(PayloadType))]
        public IHttpActionResult PostPayloadType(PayloadType payloadType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PayloadTypes.Add(payloadType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = payloadType.id }, payloadType);
        }

        // DELETE: api/PayloadType/5
        [ResponseType(typeof(PayloadType))]
        public IHttpActionResult DeletePayloadType(int id)
        {
            PayloadType payloadType = db.PayloadTypes.Find(id);
            if (payloadType == null)
            {
                return NotFound();
            }

            db.PayloadTypes.Remove(payloadType);
            db.SaveChanges();

            return Ok(payloadType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PayloadTypeExists(int id)
        {
            return db.PayloadTypes.Count(e => e.id == id) > 0;
        }
    }
}