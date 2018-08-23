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
    public class MOSController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/MOS
        public IQueryable<DropDownDTO> GetMOS_Desc()
        {
            var result = (from a in db.MOS_Desc
                          orderby a.description ascending
                          select new DropDownDTO { id = a.id.ToString(), description = (a.MOSCode.Trim() + " - " + a.description.Trim()) }).AsQueryable();
            return result;
        }

        public IHttpActionResult GetMOSDescByBranch(int branchID)
        {
            if(db.BranchOfServices.Where(x => x.id == branchID).Count() <= 0)
            {
                return NotFound();
            }

            var result = (from a in db.MOS_Desc
                          where a.branchOfServiceID == branchID
                          orderby a.description ascending
                          select new DropDownDTO { id = a.id.ToString(), description = (a.MOSCode.Trim() + " - " + a.description.Trim()) }).AsQueryable();

            return Ok(result);
        }

        // GET: api/MOS/5
        [ResponseType(typeof(MOS_Desc))]
        public IHttpActionResult GetMOS_Desc(int id)
        {
            MOS_Desc mOS_Desc = db.MOS_Desc.Find(id);
            if (mOS_Desc == null)
            {
                return NotFound();
            }

            return Ok(new DropDownDTO
            {
                id = mOS_Desc.id.ToString(),
                description = (mOS_Desc.MOSCode.Trim() + " - " + mOS_Desc.description.Trim())
            });
        }

        // PUT: api/MOS/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMOS_Desc(int id, MOS_Desc mOS_Desc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mOS_Desc.id)
            {
                return BadRequest();
            }

            db.Entry(mOS_Desc).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MOS_DescExists(id))
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

        // POST: api/MOS
        [ResponseType(typeof(MOS_Desc))]
        public IHttpActionResult PostMOS_Desc(MOS_Desc mOS_Desc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MOS_Desc.Add(mOS_Desc);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MOS_DescExists(mOS_Desc.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApiPost", new { id = mOS_Desc.id }, mOS_Desc);
        }

        // DELETE: api/MOS/5
        [ResponseType(typeof(MOS_Desc))]
        public IHttpActionResult DeleteMOS_Desc(int id)
        {
            MOS_Desc mOS_Desc = db.MOS_Desc.Find(id);
            if (mOS_Desc == null)
            {
                return NotFound();
            }

            db.MOS_Desc.Remove(mOS_Desc);
            db.SaveChanges();

            return Ok(mOS_Desc);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MOS_DescExists(int id)
        {
            return db.MOS_Desc.Count(e => e.id == id) > 0;
        }
    }
}