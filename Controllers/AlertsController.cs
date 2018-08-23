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
    public class AlertsController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/Alerts
        public IQueryable<AlertsDTO> GetAlerts()
        {
            return (from a in db.Alerts
                    select new AlertsDTO
                    {
                        name = a.Message,
                        type = "checkbox",
                        alertType = a.Type,
                        complete = a.Complete,
                        dashboardInd = a.DashboardInd,
                        ID = a.id,
                        //LinkTo = a.LinkTo
                    });
        }

        // GET: api/Alerts/5
        [ResponseType(typeof(Alert))]
        public IHttpActionResult GetAlert(string id)
        {
            Alert alert = db.Alerts.Find(id);
            if (alert == null)
            {
                return NotFound();
            }

            return Ok(alert);
        }

        // PUT: api/Alerts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAlert(string id, Alert alert)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alert.id)
            {
                return BadRequest();
            }

            db.Entry(alert).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlertExists(id))
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

        // POST: api/Alerts
        [ResponseType(typeof(Alert))]
        public IHttpActionResult PostAlert(Alert alert)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Alerts.Add(alert);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AlertExists(alert.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = alert.id }, alert);
        }

        // DELETE: api/Alerts/5
        [ResponseType(typeof(Alert))]
        public IHttpActionResult DeleteAlert(string id)
        {
            Alert alert = db.Alerts.Find(id);
            if (alert == null)
            {
                return NotFound();
            }

            db.Alerts.Remove(alert);
            db.SaveChanges();

            return Ok(alert);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlertExists(string id)
        {
            return db.Alerts.Count(e => e.id == id) > 0;
        }
    }
}