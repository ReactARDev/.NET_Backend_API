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
    public class OrderTypeController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/OrderType
        public IQueryable<DropDownDTO> GetOrderTypes()
        {
            var result = (from a in db.OrderTypes
                          orderby a.displayOrder ascending
                          select new DropDownDTO { id = a.id.ToString(), description = a.description.Trim() }).AsQueryable();
            return result;
        }

        // GET: api/OrderType/5
        [ResponseType(typeof(OrderType))]
        public IHttpActionResult GetOrderType(int id)
        {
            OrderType orderType = db.OrderTypes.Find(id);
            if (orderType == null)
            {
                return NotFound();
            }

            return Ok(orderType);
        }

        // PUT: api/OrderType/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrderType(int id, OrderType orderType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderType.id)
            {
                return BadRequest();
            }

            db.Entry(orderType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderTypeExists(id))
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

        // POST: api/OrderType
        [ResponseType(typeof(OrderType))]
        public IHttpActionResult PostOrderType(OrderType orderType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderTypes.Add(orderType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = orderType.id }, orderType);
        }

        // DELETE: api/OrderType/5
        [ResponseType(typeof(OrderType))]
        public IHttpActionResult DeleteOrderType(int id)
        {
            OrderType orderType = db.OrderTypes.Find(id);
            if (orderType == null)
            {
                return NotFound();
            }

            db.OrderTypes.Remove(orderType);
            db.SaveChanges();

            return Ok(orderType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderTypeExists(int id)
        {
            return db.OrderTypes.Count(e => e.id == id) > 0;
        }
    }
}