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
    public class MissionTypeController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/MissionType
        public IQueryable<DropDownDTO> GetMissionTypes()
        {
            var result = (from a in db.MissionTypes
                          orderby a.displayOrder ascending
                          select new DropDownDTO { id = a.id.ToString(), description = a.description }).AsQueryable();
            return result;
        }

        // GET: api/MissionType/5
        [ResponseType(typeof(MissionType))]
        public IHttpActionResult GetMissionType(int id)
        {
            MissionType missionType = db.MissionTypes.Find(id);
            if (missionType == null)
            {
                return NotFound();
            }

            return Ok(missionType);
        }

        // PUT: api/MissionType/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMissionType(int id, MissionType missionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != missionType.id)
            {
                return BadRequest();
            }

            db.Entry(missionType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MissionTypeExists(id))
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

        // POST: api/MissionType
        [ResponseType(typeof(MissionType))]
        public IHttpActionResult PostMissionType(MissionType missionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MissionTypes.Add(missionType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApiPost", new { id = missionType.id }, missionType);
        }

        // DELETE: api/MissionType/5
        [ResponseType(typeof(MissionType))]
        public IHttpActionResult DeleteMissionType(int id)
        {
            MissionType missionType = db.MissionTypes.Find(id);
            if (missionType == null)
            {
                return NotFound();
            }

            db.MissionTypes.Remove(missionType);
            db.SaveChanges();

            return Ok(missionType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MissionTypeExists(int id)
        {
            return db.MissionTypes.Count(e => e.id == id) > 0;
        }
    }
}