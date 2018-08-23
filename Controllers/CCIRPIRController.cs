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
using System.Threading.Tasks;
using System.IO;
using AMPS9000_WebAPI.Filters;
using AMPS9000_WebAPI.Helper;

namespace AMPS9000_WebAPI.Controllers
{
    public class CCIRPIRController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();
        string[] properties = {
            "EffectiveAreaKML",
        };

        // GET: api/CCIRPIR
        public IQueryable<DropDownDTO> GetCCIRPIR()
        {
            var result = (from a in db.CCIRPIRs
                          select new DropDownDTO { id = a.CCIRPIRId.ToString(), description = a.Description1 }).AsQueryable();
            return result;
        }

        // GET: api/CCIRPIR/GetCCIRPIRList
        [HttpGet]
        [Route("api/CCIRPIR/GetCCIRPIRData")]
        [GetBaseUrl("CCIRPIR/")]
        public IHttpActionResult GetCCIRPIRData()
        {
            object uploadUrl;
            Request.Properties.TryGetValue("uploadUrl", out uploadUrl);
            return Ok(from a in db.CCIRPIRs
                      join b in db.Countries on a.CountryId equals b.id into countries
                      join c in db.COCOMs on a.COCOMId equals c.id into cocoms
                      join d in db.BranchOfServices on a.BranchId equals d.id into branches
                      join e in db.Regions on a.RegionId equals e.id into regions
                      join f in db.Units on a.UnitId equals f.id into units
                      join g in db.Personnels on a.CommanderId equals g.PersonnelID into personnels
                      from h in countries.DefaultIfEmpty()
                      from i in cocoms.DefaultIfEmpty()
                      from j in branches.DefaultIfEmpty()
                      from k in regions.DefaultIfEmpty()
                      from l in units.DefaultIfEmpty()
                      from m in personnels.DefaultIfEmpty()
                      select new
                      {
                          CCIRPIRId = a.CCIRPIRId,
                          Description1 = a.Description1,
                          Description2 = a.Description2,
                          Description3 = a.Description3,
                          Description4 = a.Description4,
                          Description5 = a.Description5,
                          Description6 = a.Description6,
                          Description7 = a.Description7,
                          Description8 = a.Description8,
                          CreationDateTime = a.CreationDateTime,
                          COCOMId = a.COCOMId,
                          BranchId = a.BranchId,
                          CountryId = a.CountryId,
                          RegionId = a.RegionId,
                          UnitId = a.UnitId,
                          CommanderId = a.CommanderId,
                          MissionName = a.MissionName,
                          EffectiveAreaKML = uploadUrl + a.EffectiveAreaKML,
                          LastUpdate = a.LastUpdate,
                          LastUpdateUserId = a.LastUpdateUserId,
                          CountryName = h.description,
                          COCOM = i.description,
                          BranchName = j.description,
                          RegionName = k.description,
                          UnitName = l.description,
                          CommanderName = m.FirstName + " " + m.LastName
                      });
        }

        [HttpGet]
        [Route("api/CCIRPIR/GetCCIRPIRDataByCommander/commanderId")]
        [GetBaseUrl("CCIRPIR/")]
        public IHttpActionResult GetCCIRPIRDataByCommander(string commanderId)
        {
            object uploadUrl;
            Request.Properties.TryGetValue("uploadUrl", out uploadUrl);
            return Ok(from a in db.CCIRPIRs
                      join b in db.Countries on a.CountryId equals b.id into countries
                      join c in db.COCOMs on a.COCOMId equals c.id into cocoms
                      join d in db.BranchOfServices on a.BranchId equals d.id into branches
                      join e in db.Regions on a.RegionId equals e.id into regions
                      join f in db.Units on a.UnitId equals f.id into units
                      join g in db.Personnels on a.CommanderId equals g.PersonnelID into personnels
                      from h in countries.DefaultIfEmpty()
                      from i in cocoms.DefaultIfEmpty()
                      from j in branches.DefaultIfEmpty()
                      from k in regions.DefaultIfEmpty()
                      from l in units.DefaultIfEmpty()
                      from m in personnels.DefaultIfEmpty()
                      where a.CommanderId == commanderId
                      select new
                      {
                          CCIRPIRId = a.CCIRPIRId,
                          Description1 = a.Description1,
                          Description2 = a.Description2,
                          Description3 = a.Description3,
                          Description4 = a.Description4,
                          Description5 = a.Description5,
                          Description6 = a.Description6,
                          Description7 = a.Description7,
                          Description8 = a.Description8,
                          CreationDateTime = a.CreationDateTime,
                          COCOMId = a.COCOMId,
                          BranchId = a.BranchId,
                          CountryId = a.CountryId,
                          RegionId = a.RegionId,
                          UnitId = a.UnitId,
                          CommanderId = a.CommanderId,
                          MissionName = a.MissionName,
                          EffectiveAreaKML = uploadUrl + a.EffectiveAreaKML,
                          LastUpdate = a.LastUpdate,
                          LastUpdateUserId = a.LastUpdateUserId,
                          CountryName = h.description,
                          COCOM = i.description,
                          BranchName = j.description,
                          RegionName = k.description,
                          UnitName = l.description,
                          CommanderName = m.FirstName + " " + m.LastName
                      });
        }

        [HttpGet]
        [Route("api/CCIRPIR/GetCCIRPIRDataByUnit/{unitId}")]
        public IEnumerable<CCIRPIR> GetCCIRPIRDataByUnit(int unitId)
        {
            return db.CCIRPIRs.Where(c => c.UnitId == unitId);
        }

        // GET: api/CCIRPIR/{id}
        [Route("api/CCIRPIR/{id}")]
        [ResponseType(typeof(CCIRPIR))]
        public IHttpActionResult GetCCIRPIR(string id)
        {
            CCIRPIR cCIRs = db.CCIRPIRs.Find(id);
            if (cCIRs == null)
            {
                return NotFound();
            }

            return Ok(new DropDownDTO
            {
                id = cCIRs.CCIRPIRId.ToString(),
                description = cCIRs.Description1
            });
        }

        // GET: api/CCIRPIR/GetCCIRPIRData/{id}
        [ResponseType(typeof(CCIRPIR))]
        [GetBaseUrl("CCIRPIR/")]
        public IHttpActionResult GetCCIRPIRData(string id)
        {
            CCIRPIR cCIRs = db.CCIRPIRs.Find(id);
            if (cCIRs == null)
            {
                return NotFound();
            }

            cCIRs.AddUrl(cCIRs.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), this.Request);
            //object uploadUrl;
            //Request.Properties.TryGetValue("uploadUrl", out uploadUrl);
            //cCIRs.EffectiveAreaKML = uploadUrl + cCIRs.EffectiveAreaKML;
            return Ok(cCIRs);
        }

        // PUT: api/CCIRPIR/5
        [ResponseType(typeof(void))]
        [Route("api/CCIRPIR/PutCCIRPIR")]
        [GetBaseUrl("CCIRPIR/")]
        public async Task<IHttpActionResult> PutCCIRPIR(string id)
        {
            CCIRPIR cCIRPIR = await UpdateDataWithFile();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO It can make api slow so commenting it. Will use if needed
            //string message = string.Empty;
            //if (!IsValidData(cCIRs, out message))
            //{
            //    return BadRequest(message);
            //}

            if (!string.Equals(cCIRPIR.CCIRPIRId, id.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest();
            }

            cCIRPIR.RemoveUrl(cCIRPIR.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), this.Request);
            cCIRPIR.LastUpdate = DateTime.Now;
            db.Entry(cCIRPIR).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CCIRPIRsExists(id.ToString()))
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

        // POST: api/CCIRPIR
        [HttpPost]
        [ResponseType(typeof(CCIRPIR))]
        [GetBaseUrl("CCIRPIR/")]
        public async Task<IHttpActionResult> PostCCIRPIR()
        {
            CCIRPIR cCIRPIR = await UpdateDataWithFile();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO It can make api slow so commenting it. Will use if needed
            //string message = string.Empty;
            //if (!IsValidData(cCIRs, out message))
            //{
            //    return BadRequest(message);
            //}

            cCIRPIR.CCIRPIRId = Guid.NewGuid().ToString();
            cCIRPIR.CreationDateTime = cCIRPIR.LastUpdate = DateTime.Now;
            db.CCIRPIRs.Add(cCIRPIR);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApiPost", new { id = cCIRPIR.CCIRPIRId }, cCIRPIR);
        }

        // DELETE: api/CCIRPIR/5
        [ResponseType(typeof(CCIRPIR))]
        public IHttpActionResult DeleteCCIRPIR(string id)
        {
            CCIRPIR cCIRs = db.CCIRPIRs.Find(id);
            if (cCIRs == null)
            {
                return NotFound();
            }

            db.CCIRPIRs.Remove(cCIRs);
            db.SaveChanges();
            cCIRs.DeleteFiles(cCIRs.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), "CCIRPIR");
            return Ok(cCIRs);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IsValidData(CCIRPIR cCIRPIR, out string message)
        {
            message = string.Empty;
            if (!IsValidCountry(cCIRPIR.CountryId))
            {
                message = "Invalid Country Id:" + cCIRPIR.CountryId;
            }

            if (!IsValidBranch(cCIRPIR.BranchId))
            {
                message = "Invalid Branch Id:" + cCIRPIR.BranchId;
            }

            if (!IsValidCOCOM(cCIRPIR.COCOMId))
            {
                message = "Invalid COCOM Id:" + cCIRPIR.COCOMId;
            }

            if (!IsValidCommander(cCIRPIR.CommanderId))
            {
                message = "Invalid Commander Id:" + cCIRPIR.CommanderId;
            }

            if (!IsValidRegion(cCIRPIR.RegionId))
            {
                message = "Invalid Region Id:" + cCIRPIR.RegionId;
            }

            if (!IsValidUnit(cCIRPIR.UnitId))
            {
                message = "Invalid Unit Id:" + cCIRPIR.UnitId;
            }

            return message == string.Empty;
        }

        private bool CCIRPIRsExists(string id)
        {
            return db.CCIRPIRs.Any(e => e.CCIRPIRId == id);
        }

        private bool IsValidCountry(string id)
        {
            return db.Countries.Any(c => c.id == id);
        }

        private bool IsValidCommander(string id)
        {
            return db.Personnels.Any(c => c.PersonnelID == id);
        }

        private bool IsValidRegion(int id)
        {
            return db.Regions.Any(r => r.id == id);
        }

        private bool IsValidUnit(int id)
        {
            return db.Units.Any(u => u.id == id);
        }

        private bool IsValidBranch(int id)
        {
            return db.BranchOfServices.Any(b => b.id == id);
        }

        private bool IsValidCOCOM(int id)
        {
            return db.COCOMs.Any(c => c.id == id);
        }

        private static bool CompareTwoStrings(string val1, string val2)
        {
            return string.Equals(val1, val2, StringComparison.OrdinalIgnoreCase);
        }

        private async Task<CCIRPIR> UpdateDataWithFile()
        {
            using (var uploadController = new UploadController<CCIRPIR>())
            {
                uploadController.ControllerContext = this.ControllerContext;
                uploadController.Request = this.Request;
                var cCIRPIR = await uploadController.PostFileWithData("ccirpirFormData", "CCIRPIR");
                if (cCIRPIR != null && uploadController.ListofFiles != null && uploadController.ListofFiles.Count > 0)
                {
                    cCIRPIR.UpdateFileProperties(cCIRPIR.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), this.Request, uploadController.ListofFiles);
                }

                return cCIRPIR;
            }
        }

        //private async Task<CCIRPIR> UpdateDataWithFile()
        //{
        //    using (var uploadController = new UploadController<CCIRPIR>())
        //    {
        //        uploadController.ControllerContext = this.ControllerContext;
        //        uploadController.Request = this.Request;
        //        var cCIRPIR = await uploadController.PostFileWithData("ccirpirFormData", "CCIRPIR");
        //        cCIRPIR.EffectiveAreaKML = Path.GetFileName(uploadController.ListofFiles["effectiveAreaKML"]);
        //        return cCIRPIR;
        //    }
        //}
    }
}