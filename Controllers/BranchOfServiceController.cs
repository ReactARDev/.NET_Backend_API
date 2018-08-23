using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using AMPS9000_WebAPI;
using System.Web.Http.Cors;

namespace AMPS9000_WebAPI.Controllers
{
    public class BranchOfServiceController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/BranchOfService
        public IQueryable<DropDownDTO> GetBranchOfService()
        {
            var result = (from a in db.BranchOfServices
                          orderby a.description ascending
                          select new DropDownDTO { id = a.id.ToString(), description = a.description.Trim() }).AsQueryable();
            return result;
        }

        public IHttpActionResult GetBranchOfServiceByRankID(int rankID)
        {

            if(db.Ranks.Where(x => x.id == rankID).Count() <= 0)
            {
                return NotFound();
            }

            var result = (from a in db.BranchOfServices
                          join b in db.Ranks on a.id equals b.branchOfServiceID
                          where b.id == rankID
                          orderby a.description ascending
                          select new DropDownDTO { id = a.id.ToString(), description = a.description.Trim() }).AsQueryable();

            return Ok(result);
        }

        // GET: api/BranchOfService/5
        [ResponseType(typeof(BranchOfService))]
        public IHttpActionResult GetBranchOfService(int id)
        {
            BranchOfService branchOfService = db.BranchOfServices.Find(id);
            if (branchOfService == null)
            {
                return NotFound();
            }

            return Ok(new DropDownDTO
            {
                id = branchOfService.id.ToString(),
                description = branchOfService.description
            });
        }

        // PUT: api/BranchOfService/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBranchOfService(int id, BranchOfService branchOfService)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != branchOfService.id)
            {
                return BadRequest();
            }

            db.Entry(branchOfService).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchOfServiceExists(id))
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

        // POST: api/BranchOfService
        [ResponseType(typeof(BranchOfService))]
        public IHttpActionResult PostBranchOfService(BranchOfService branchOfService)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BranchOfServices.Add(branchOfService);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApiPost", new { id = branchOfService.id }, branchOfService);
        }

        // DELETE: api/BranchOfService/5
        [ResponseType(typeof(BranchOfService))]
        public IHttpActionResult DeleteBranchOfService(int id)
        {
            BranchOfService branchOfService = db.BranchOfServices.Find(id);
            if (branchOfService == null)
            {
                return NotFound();
            }

            db.BranchOfServices.Remove(branchOfService);
            db.SaveChanges();

            return Ok(branchOfService);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BranchOfServiceExists(int id)
        {
            return db.BranchOfServices.Count(e => e.id == id) > 0;
        }
    }
}