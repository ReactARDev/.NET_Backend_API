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
    public class PayGradesController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/PayGrades
        public IQueryable<DropDownDTO> GetPayGrades()
        {
            var result = (from a in db.PayGrades
                          orderby a.DisplayOrder ascending
                          select new DropDownDTO { id = a.id.ToString(), description = a.DisplayText }).AsQueryable();
            return result;
        }

        public IHttpActionResult GetPayGradesByRank(int rankID)
        {
            if (db.Ranks.Where(x => x.id == rankID).Count() <= 0)
            {
                return NotFound();
            }

            var results = (from a in db.PayGrades
                           join b in db.Ranks on a.id equals b.payGradeID
                           where b.id == rankID
                           select new DropDownDTO { id = a.id.ToString(), description = a.DisplayText });

            return Ok(results);
        }

        // GET: api/PayGrades/5
        [ResponseType(typeof(PayGrade))]
        public IHttpActionResult GetPayGrades(int id)
        {
            PayGrade payGrades = db.PayGrades.Find(id);
            if (payGrades == null)
            {
                return NotFound();
            }

            return Ok(new DropDownDTO
            {
                id = payGrades.id.ToString(),
                description = payGrades.DisplayText
            });
        }

        // PUT: api/PayGrades/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPayGrades(int id, PayGrade payGrades)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != payGrades.id)
            {
                return BadRequest();
            }

            db.Entry(payGrades).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayGradesExists(id))
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

        // POST: api/PayGrades
        [ResponseType(typeof(PayGrade))]
        public IHttpActionResult PostPayGrades(PayGrade payGrades)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PayGrades.Add(payGrades);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApiPost", new { id = payGrades.id }, payGrades);
        }

        // DELETE: api/PayGrades/5
        [ResponseType(typeof(PayGrade))]
        public IHttpActionResult DeletePayGrades(int id)
        {
            PayGrade payGrades = db.PayGrades.Find(id);
            if (payGrades == null)
            {
                return NotFound();
            }

            db.PayGrades.Remove(payGrades);
            db.SaveChanges();

            return Ok(payGrades);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PayGradesExists(int id)
        {
            return db.PayGrades.Count(e => e.id == id) > 0;
        }
    }
}