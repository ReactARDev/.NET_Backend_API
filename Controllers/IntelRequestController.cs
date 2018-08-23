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
    public class IntelRequestController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/IntelRequest
        public IQueryable<IntelReqDTO> GetIntelRequestData()
        {
            return getIntelRequestData();
        }

        public IHttpActionResult GetIntelRequests()
        {
            var results = (from a in db.IntelRequests
                           join b in db.IntelReqStatus on a.IntelRequestID equals b.IntelRequestID into lojStat
                           from c in lojStat.DefaultIfEmpty()
                           select new
                           {
                               ID = a.ReqUserFrndlyID,
                               status = db.StatusCodes.Where(x => x.id == c.Status).Select(x => x.description).FirstOrDefault() ?? "Unknown"
                           });

            return Ok(results);
        }

        // GET: api/IntelRequest/5
        [ResponseType(typeof(IntelRequest))]
        public IHttpActionResult GetIntelRequest(string id)
        {
            var intelRequest = this.getIntelRequestData(id).SingleOrDefault();
            if (intelRequest != null)
            {
                intelRequest.IntelReqEEIs = (from ir in db.IntelReqEEIs
                                             join tr in db.Targets on ir.targetID equals tr.id into target
                                             from n in target.DefaultIfEmpty()
                                             join o in db.Objectives on ir.objective equals o.id into obj
                                             from m in obj.DefaultIfEmpty()
                                             join y in db.LIMIDSReqs on ir.LIMIDS_Req equals y.id into tempTable
                                             from l in tempTable.DefaultIfEmpty()
                                             join z in db.EEIThreats on ir.threatGroupID equals z.id into tempTable2
                                             from t in tempTable2.DefaultIfEmpty()
                                             where ir.intelReqID == id
                                             select new IntelReqEEI_DTO
                                             {
                                                 id = ir.id,
                                                 intelReqID = ir.intelReqID,
                                                 targetID = ir.targetID,
                                                 targetName = n.targetName,
                                                 targetNumber = n.targetNumber,
                                                 threatGroupID = t.description,
                                                 location = ir.location,
                                                 district = ir.district,
                                                 gridCoordinates = ir.gridCoordinates,
                                                 LIMIDS_Req = l.description,
                                                 POI1_ID = ir.POI1_ID,
                                                 POI2_ID = ir.POI2_ID,
                                                 POI3_ID = ir.POI3_ID,
                                                 EEIs = ir.EEIs,
                                                 objectiveID = ir.objectiveID,
                                                 objectiveName = m.objectiveName
                                             });
            }

            return Ok(intelRequest);
        }

        // GET: api/IntelRequest/GetRequestStatus
        public IHttpActionResult GetRequestStatus()
        {
            var result = (from p in db.StatusCodes.Where(x => x.type == 2)
                          join q in db.IntelReqStatus on p.id equals q.Status into j1
                          from j2 in j1.DefaultIfEmpty()
                          group j2 by p.id into grouped
                          select new
                          {
                              StatusID = grouped.Key,
                              Count = grouped.Sum(x => x.IntelRequestID != null ? 1 : 0)
                          }).AsEnumerable()
                         .Select(pt => new
                         {
                             StatusID = pt.StatusID,
                             Count = pt.Count
                         }).ToList();

            return Ok(result);
        }

        // GET: api/IntelRequest/GetApprovedIntelRequests
        public IQueryable<IntelReqDTO> GetApprovedIntelRequests()
        {
            return this.getIntelRequestData().Where(item => item.Abbreviation.Equals("AV", StringComparison.OrdinalIgnoreCase));
        }

        // GET: api/IntelRequest/GetIntelRequestByUnitIdAndSatatusId
        public IQueryable<IntelReqDTO> GetIntelRequestByUnitIdAndSatatusId(int statusId, int unitId)
        {
            return this.getIntelRequestData().Where(item => item.StatusId == statusId && item.UnitId == unitId);
        }

        // GET: api/IntelRequest/GetIntelRequestByAbbreviation
        public IQueryable<IntelReqDTO> GetIntelRequestByAbbreviation(string abbreviation, int unitId, bool? isInCollectionPlan)
        {
            if (!isInCollectionPlan.HasValue)
            {
                return this.getIntelRequestData().Where(item => item.Abbreviation.Equals(abbreviation.ToUpper()) && item.UnitId == unitId);
            }
            else
            {
                return this.getIntelRequestData().Where(item => item.Abbreviation.Equals(abbreviation.ToUpper()) && item.UnitId == unitId && (isInCollectionPlan.Value ? item.IsInCollectionPlan.Value : (!item.IsInCollectionPlan.Value || !item.IsInCollectionPlan.HasValue)));
            }
        }

        // PUT: api/IntelRequest/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIntelRequest(string id, IntelRequest intelRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != intelRequest.IntelRequestID)
            {
                return BadRequest();
            }

            db.Entry(intelRequest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IntelRequestExists(id))
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

        // PUT: api/IntelRequest/ChangeIntelRequestStatus
        [HttpPut]
        public IHttpActionResult ChangeIntelRequestStatus(string intelRequestId, int statusId)
        {
            return UpdateIntelRequestWithCollectionPlan(intelRequestId, statusId);
        }

        // PUT: api/IntelRequest/RouteCollectionIntelRequest
        [HttpPut]
        public IHttpActionResult RouteCollectionIntelRequest(int unitId, int statusId)
        {
            var personnelId = db.Personnels.FirstOrDefault(p => p.AssignedUnit == unitId);
            var intelRequests = db.IntelRequests.Where(ir => db.Personnels.Any(p => p.PersonnelID == ir.OrginatorPersonnelID) && ir.IsInCollectionPlan == true).ToList();
            intelRequests.ForEach(m => { m.StatusId = statusId; m.IsInCollectionPlan = null; });
            db.SaveChanges();
            return Ok();
        }

        // PUT: api/IntelRequest/MoveToCollectionPlan/{guid}
        [HttpPut]
        public IHttpActionResult MoveToCollectionPlan(string id)
        {
            return this.UpdateIntelRequestWithCollectionPlan(id, null, true);
        }

        // TODO For the time being statusid is not required here so its nullable
        // PUT: api/IntelRequest/MoveOutFromCollectionPlan
        [HttpPut]
        [Route("api/IntelRequest/MoveOutFromCollectionPlan/{id}/{statusId?}")]
        public IHttpActionResult MoveOutFromCollectionPlan(string id, int? statusId = null)
        {
            return this.UpdateIntelRequestWithCollectionPlan(id, statusId, false);
        }

        // POST: api/IntelRequest
        [ResponseType(typeof(IntelRequest))]
        public IHttpActionResult PostIntelRequest(IntelRequest intelRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (intelRequest.IntelRequestID == null || intelRequest.IntelRequestID == "0")
            {
                intelRequest.IntelRequestID = Guid.NewGuid().ToString();
            }

            intelRequest.StatusId = 1;
            db.IntelRequests.Add(intelRequest);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (IntelRequestExists(intelRequest.IntelRequestID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            db.IntelReqStatus.Add(new IntelReqStatu
            {
                IntelRequestID = intelRequest.IntelRequestID,
                StatusDateTime = DateTime.Now,
                Status = (int)IntelReqStatuses.PENDING
            });

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            db.Alerts.Add(new Alert
            {
                id = Guid.NewGuid().ToString(),
                Type = 1,    //Action Item
                Message = "Intel Request #" + intelRequest.ReqUserFrndlyID + " Pending Review",
                DashboardInd = true,
                Complete = false,
                createDate = DateTime.Now,
                //createUserId = "1",
                //LinkTo = "/intel-request/review/" + intelRequest.IntelRequestID
            });

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return CreatedAtRoute("DefaultApiPost", new { id = intelRequest.IntelRequestID }, intelRequest);
        }

        // DELETE: api/IntelRequest/5
        [ResponseType(typeof(IntelRequest))]
        public IHttpActionResult DeleteIntelRequest(string id)
        {
            IntelRequest intelRequest = db.IntelRequests.Find(id);
            if (intelRequest == null)
            {
                return NotFound();
            }

            // TODO In future we need to check if IntelRequest is being used somewhere in missions or other objects
            //Update status to delete
            var status = db.StatusCodes.Join(db.StatusCodeTypes, sc => sc.type, st => st.id, (statuscode, type) => new { statuscode.id, type.description }).FirstOrDefault(s => s.description.Equals("all", StringComparison.OrdinalIgnoreCase));
            intelRequest.StatusId = status.id;
            db.SaveChanges();

            return Ok(intelRequest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IntelRequestExists(string id)
        {
            return db.IntelRequests.Count(e => e.IntelRequestID == id) > 0;
        }

        private IQueryable<IntelReqDTO> getIntelRequestData(string id = "")
        {
            db.Configuration.LazyLoadingEnabled = false;

            var result = (from ir in db.IntelRequests
                          join pi in db.Personnels on ir.OrginatorPersonnelID equals pi.PersonnelID
                          into perir
                          from ljperir in perir.DefaultIfEmpty()
                          join unit in db.Units on ljperir.AssignedUnit equals unit.id
                          into unitPer
                          from ljunitPer in unitPer.DefaultIfEmpty()
                          join s in db.StatusCodes on ir.StatusId equals s.id
                          join u in db.Units on ir.NextHigherUnitId equals u.id into uir
                          from ljuir in uir.DefaultIfEmpty()
                          join p in db.Priorities on ir.PriorityId equals p.Id into prir
                          from ljprir in prir.DefaultIfEmpty()
                          join pt in db.PayloadTypes on ir.PrimaryPayload equals pt.id into ptir
                          from ljptir in ptir.DefaultIfEmpty()
                          join pt2 in db.PayloadTypes on ir.SecondaryPayload equals pt2.id into ptir2
                          from ljptir2 in ptir2.DefaultIfEmpty()
                          join c in db.IC_ISM_Classifications on ir.ReportClassification equals c.ClassificationMarkingValue into cir
                          from ljcir in cir.DefaultIfEmpty()
                          join mt in db.MissionTypes on ir.MissionType equals mt.id into mtir
                          from ljmtir in mtir.DefaultIfEmpty()
                          join cocom in db.COCOMs on ir.SupportedCommand equals cocom.id into cocomir
                          from ljcocomir in cocomir.DefaultIfEmpty()
                          join a in db.AssetTypes on ir.AssetId equals a.id
                          where (id == "") ? 1 == 1 : ir.IntelRequestID == id
                          select new IntelReqDTO
                          {
                              IntelRequestID = ir.IntelRequestID,
                              NamedOperation = ir.NamedOperation,
                              SupportedCommand = ir.SupportedCommand,
                              COCOMText = ljcocomir.description,
                              SupportedUnit = ir.SupportedUnit,
                              MissionType = ir.MissionType,
                              MissionTypeText = ljmtir.description,
                              OriginatorFirstName = ljperir.FirstName,
                              OriginatorLastName = ljperir.LastName,
                              OrignatedDateTime = ir.OrignatedDateTime,
                              OrginatorPersonnelID = ir.OrginatorPersonnelID,
                              LatestTimeIntelValue = ir.LatestTimeIntelValue,
                              AreaOfOperations = ir.AreaOfOperations,
                              StatusId = s.id,
                              Status = s.description,
                              Abbreviation = s.abbreviation,
                              OriginatorEmail = ljperir.EmailSIPR,
                              OriginatorDSN = ljperir.DSN,
                              NextHigherUnit = ljuir.description,
                              NextHigherUnitId = ir.NextHigherUnitId,
                              Unit = ljunitPer.description,
                              UnitId = ljperir.AssignedUnit,
                              Priority = ljprir.Description,
                              PriorityId = ir.PriorityId,
                              ActiveDateTimeStart = ir.ActiveDateTimeStart,
                              ActiveDateTimeEnd = ir.ActiveDateTimeEnd,
                              BestCollectionTime = ir.BestCollectionTime,
                              PriorityIntelRequirement = ir.PriorityIntelRequirement,
                              PrimaryPayloadName = ljptir.description,
                              SecondaryPayloadName = ljptir2.description,
                              PrimaryPayload = ir.PrimaryPayload,
                              SecondaryPayload = ir.SecondaryPayload,
                              ReportClassification = ir.ReportClassification,
                              ReportClassificationName = ljcir.Description,
                              ReqUserFrndlyID = ir.ReqUserFrndlyID,
                              AssetId = a.id,
                              Asset = a.description,
                              Armed = ir.Armed,
                              IsInCollectionPlan = ir.IsInCollectionPlan
                          }
                          );
            return result;
        }

        private IHttpActionResult UpdateIntelRequestWithCollectionPlan(string intelReqId, int? statusId, bool? isInCollectionPlan = null)
        {
            IntelRequest intelRequest = db.IntelRequests.Find(intelReqId);
            intelRequest.IsInCollectionPlan = isInCollectionPlan ?? intelRequest.IsInCollectionPlan;
            intelRequest.StatusId = statusId ?? intelRequest.StatusId;
            return this.PutIntelRequest(intelReqId, intelRequest);
        }
    }
}