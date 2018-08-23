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
    public class PersonnelStatusController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/Personnel/GetPersonnelStatusData
        public IHttpActionResult GetPersonnelStatusData()
        {
            var result = (from a in db.Personnels
                          join b in db.Ranks on a.Rank equals b.id into lojRank
                          from c in lojRank.DefaultIfEmpty()
                          join d in db.DutyPositions on a.DutyPosition1 equals d.id into lojDuty
                          from e in lojDuty.DefaultIfEmpty()
                          from f in db.PersonnelStatus
                          join g in db.StatusCodes on f.StatusCode equals g.id
                          where f.PersonnelID == a.PersonnelID && g.type == 1
                          orderby a.LastUpdate descending, a.FirstName ascending
                          select new
                          {
                              ID = a.PersonnelID,
                              fullName = a.FirstName + " " + a.LastName,
                              rank = c.description.Trim(),
                              dutyPos = e.description.Trim(),
                              status = g.description,
                              arrive = f.PersonnelArrive,
                              depart = f.PersonnelDepart
                          });

            return Ok(result);
        }

        // GET: api/PersonnelStatus
        public IHttpActionResult GetPersonnelStatusDetail()
        {
            var results = (from a in db.Personnels
                           join b in db.PersonnelStatus on a.PersonnelID equals b.PersonnelID
                           orderby a.LastName
                           select new
                           {
                               FirstName = a.FirstName,
                               LastName = a.LastName,
                               CACID = a.CACid,
                               Status = new 
                               {
                                   id = b.StatusCode,
                                   description = db.StatusCodes.Where(x => x.id == b.StatusCode && x.type == 1).Select(x => x.description).FirstOrDefault()
                               },
                               Rank = new
                               {
                                   id = a.Rank,
                                   abbreviation = db.Ranks.Where(x => x.id == a.Rank).Select(x => x.rankAbbreviation).FirstOrDefault(),
                                   description = db.Ranks.Where(x => x.id == a.Rank).Select(x => x.description).FirstOrDefault()
                               },
                               BranchOfService = new
                               {
                                   id = a.ServiceBranch,
                                   description = db.BranchOfServices.Where(x => x.id == a.ServiceBranch).Select(x => x.description).FirstOrDefault()
                               },
                               DeployedUnit = new
                               {
                                   id = a.DeployedUnit,
                                   description = db.Units.Where(x => x.id == a.DeployedUnit).Select(x => x.description).FirstOrDefault()
                               }
                           });

            return Ok(results);
        }

        // GET: api/PersonnelStatus/{guid}
        [ResponseType(typeof(PersonnelStatusUpdateDTO))]
        public IHttpActionResult GetPersonnelStatus(string id)
        {
            var results = (from a in db.PersonnelStatus
                           where a.PersonnelID == id
                           select new PersonnelStatusUpdateDTO
                           {
                               id = a.PersonnelID,
                               ArrivalDate = a.PersonnelArrive,
                               DepartureDate = a.PersonnelDepart,
                               StatusCode = a.StatusCode,
                               Remark = a.PersonnelRemarks ?? ""
                           }).FirstOrDefault();

            return Ok(results);
        }

        // PUT: api/PersonnelStatus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPersonnelStatu(string id, PersonnelStatusUpdateDTO personnelStatu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personnelStatu.id)
            {
                return BadRequest();
            }

            PersonnelStatu status = db.PersonnelStatus.Where(x => x.PersonnelID == id).FirstOrDefault();

            if(status == null)
            {
                return BadRequest("Invalid Personnel ID");
            }

            if (!db.StatusCodes.Any(x => x.id == personnelStatu.StatusCode && x.type == 1))
            {
                return BadRequest("Invalid Status Code");
            }
            else
            {
                status.StatusCode = personnelStatu.StatusCode;
            }

            if (personnelStatu.DepartureDate < personnelStatu.ArrivalDate)
            {
                return BadRequest("Invalid Departure Date");
            }

            status.PersonnelArrive = personnelStatu.ArrivalDate;
            status.PersonnelDepart = personnelStatu.DepartureDate;
            status.PersonnelRemarks = personnelStatu.Remark;
            status.lastUpdate = DateTime.Now;
            status.lastUpdateUserId = "000";

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonnelStatuExists(id))
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonnelStatuExists(string id)
        {
            return db.PersonnelStatus.Count(e => e.PersonnelID == id) > 0;
        }
    }
}