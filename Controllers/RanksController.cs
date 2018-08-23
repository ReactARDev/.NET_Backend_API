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
    public class RanksController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/Ranks
        public IQueryable<DropDownDTO> GetRanks()
        {
            var result = (from a in db.Ranks
                          orderby a.displayOrder ascending
                          select new DropDownDTO { id = a.id.ToString(), description = (a.description.Trim() + " (" + a.rankAbbreviation.Trim() + ")") }).AsQueryable();
            return result;
        }

        public IHttpActionResult GetRanksByBranch(int branchID)
        {
            if(db.BranchOfServices.Where(x => x.id == branchID).Count() <= 0)
            {
                return NotFound();
            }

            var results = (from a in db.Ranks
                           where a.branchOfServiceID == branchID
                           select new DropDownDTO { id = a.id.ToString(), description = (a.description.Trim() + " (" + a.rankAbbreviation.Trim() + ")") });

            return Ok(results);
        }

        public IHttpActionResult GetRanksByPayGrade(int payGradeID)
        {
            if (db.PayGrades.Where(x => x.id == payGradeID).Count() <= 0)
            {
                return NotFound();
            }

            var results = (from a in db.Ranks
                           where a.payGradeID == payGradeID
                           select new DropDownDTO { id = a.id.ToString(), description = (a.description.Trim() + " (" + a.rankAbbreviation.Trim() + ")") });

            return Ok(results);
        }

        // GET: api/Ranks/5
        [ResponseType(typeof(Rank))]
        public IHttpActionResult GetRanks(int id)
        {
            Rank ranks = db.Ranks.Find(id);
            if (ranks == null)
            {
                return NotFound();
            }

            return Ok(new DropDownDTO
            {
                id = ranks.id.ToString(),
                description = (ranks.description + " (" + ranks.rankAbbreviation + ")")
            });
        }

        // PUT: api/Ranks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRanks(int id, Rank ranks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ranks.id)
            {
                return BadRequest();
            }

            db.Entry(ranks).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RanksExists(id))
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

        // POST: api/Ranks
        [ResponseType(typeof(Rank))]
        public IHttpActionResult PostRanks(Rank ranks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ranks.Add(ranks);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApiPost", new { id = ranks.id }, ranks);
        }

        // DELETE: api/Ranks/5
        [ResponseType(typeof(Rank))]
        public IHttpActionResult DeleteRanks(int id)
        {
            Rank ranks = db.Ranks.Find(id);
            if (ranks == null)
            {
                return NotFound();
            }

            db.Ranks.Remove(ranks);
            db.SaveChanges();

            return Ok(ranks);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RanksExists(int id)
        {
            return db.Ranks.Count(e => e.id == id) > 0;
        }
    }
}