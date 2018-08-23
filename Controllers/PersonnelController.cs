using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AMPS9000_WebAPI;
using AMPS9000_WebAPI.Filters;
using AMPS9000_WebAPI.Helper;

namespace AMPS9000_WebAPI.Controllers
{
    public class PersonnelController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();
        private string[] properties = {
            "PersonnelPhoto",
            "OrganizationLogo",
            "Datasheet"
        };
        // GET: api/Personnel
        public IQueryable<DropDownDTO> GetPersonnel()
        {
            var result = (from a in db.Personnels
                          join b in db.Ranks on a.Rank equals b.id into lojRank
                          from c in lojRank.DefaultIfEmpty()
                          orderby a.LastUpdate descending, a.FirstName ascending
                          select new DropDownDTO { id = a.PersonnelID.ToString(), description = (c.rankAbbreviation.Trim() + " " + a.FirstName.Trim() + " " + a.LastName.Trim()) }).AsQueryable();
            return result;
        }

        // GET: api/Personnel/{guid}
        [ResponseType(typeof(Personnel))]
        [GetBaseUrl("Personnel/")]
        public IHttpActionResult GetPersonnel(string id)
        {
            Personnel personnel = db.Personnels.Find(id);
            if (personnel == null)
            {
                return NotFound();
            }

            personnel.AddUrl(personnel.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), this.Request);

            //object uploadUrl;
            //Request.Properties.TryGetValue("uploadUrl", out uploadUrl);
            //personnel.PersonnelPhoto = uploadUrl + personnel.PersonnelPhoto;
            //personnel.OrganizationLogo = uploadUrl + personnel.OrganizationLogo;
            //personnel.Datasheet = uploadUrl + personnel.Datasheet;
            return Ok(personnel);
        }

        // GET: api/Personnel/GetPersonnelData
        [GetBaseUrl("Personnel/")]
        public IHttpActionResult GetPersonnelData()
        {
            object uploadUrl;
            Request.Properties.TryGetValue("uploadUrl", out uploadUrl);
            var result = (from a in db.Personnels
                          join b in db.PersonnelStatus on a.PersonnelID equals b.PersonnelID
                          join c in db.Ranks on a.Rank equals c.id into lojRank
                          from d in lojRank.DefaultIfEmpty()
                          join e in db.BranchOfServices on a.ServiceBranch equals e.id into lojBranch
                          from f in lojBranch.DefaultIfEmpty()
                          join g in db.Units on a.DeployedUnit equals g.id into lojDeploy
                          from h in lojDeploy.DefaultIfEmpty()
                          join i in db.Units on a.AssignedUnit equals i.id into lojAssign
                          from j in lojAssign.DefaultIfEmpty()
                          orderby a.LastUpdate descending, a.FirstName ascending
                          select new
                          {
                              ID = a.PersonnelID,
                              CACID = a.CACid ?? "Unknown",
                              firstName = a.FirstName,
                              lastName = a.LastName,
                              rank = d.description ?? "Unknown",
                              branchOfService = f.description ?? "Unknown",
                              deployedUnit = h.description ?? "Unknown",
                              assignedUnit = j.description ?? "Unknown",
                              photo = uploadUrl + a.PersonnelPhoto,
                              organizationLogo = uploadUrl + a.OrganizationLogo,
                              datasheet = uploadUrl + a.Datasheet,
                          });

            return Ok(result);
        }

        // GET: api/Personnel/GetCommanderList
        public IQueryable<DropDownDTO> GetCommanderList()
        {
            var result = (from a in db.Personnels
                          join b in db.Ranks on a.Rank equals b.id into lojRank
                          from c in lojRank.DefaultIfEmpty()
                          where c.description.Equals("Commander", StringComparison.OrdinalIgnoreCase)
                          orderby a.FirstName ascending
                          select new DropDownDTO { id = a.PersonnelID.ToString(), description = (c.rankAbbreviation.Trim() + " " + a.FirstName.Trim() + " " + a.LastName.Trim()) }).AsQueryable();
            return result;
        }
        // PUT: api/Personnel/{guid}
        [ResponseType(typeof(void))]
        [GetBaseUrl("Personnel/")]
        public async Task<IHttpActionResult> PutPersonnel(string id)
        {
            Personnel personnel = await UpdateDataWithFile();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personnel.PersonnelID)
            {
                return BadRequest();
            }

            DateTime dte = db.Personnels.Where(x => x.PersonnelID == id).Select(x => x.CreateDate).FirstOrDefault();

            personnel.CreateDate = dte;
            personnel.LastUpdate = DateTime.Now;
            personnel.RemoveUrl(personnel.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), this.Request);
            db.Entry(personnel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonnelExists(id))
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

        // POST: api/Personnel
        [ResponseType(typeof(Personnel))]
        [GetBaseUrl("Personnel/")]
        public async Task<IHttpActionResult> PostPersonnel()
        {
            Personnel personnel = await UpdateDataWithFile();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            personnel.PersonnelID = Guid.NewGuid().ToString();
            personnel.CreateDate = DateTime.Now;
            personnel.LastUpdate = DateTime.Now;

            db.Personnels.Add(personnel);

            try
            {
                db.SaveChanges();

                PersonnelStatu pStatus = new PersonnelStatu
                {
                    PersonnelID = personnel.PersonnelID,
                    StatusCode = (int)PersonnelStatuses.PENDING,
                    PersonnelArrive = personnel.CurrentAssignmentStart ?? DateTime.Today,
                    PersonnelDepart = personnel.CurrentAssignmentEnd ?? DateTime.Today,
                    lastUpdate = DateTime.Now,
                    lastUpdateUserId = "000"
                };

                db.PersonnelStatus.Add(pStatus);
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PersonnelExists(personnel.PersonnelID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApiPost", new { id = personnel.PersonnelID }, personnel);
        }

        // DELETE: api/Personnel/5
        [ResponseType(typeof(Personnel))]
        public IHttpActionResult DeletePersonnel(string id)
        {
            Personnel personnel = db.Personnels.Find(id);
            if (personnel == null)
            {
                return NotFound();
            }

            db.Personnels.Remove(personnel);
            db.SaveChanges();
            personnel.DeleteFiles(personnel.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), "Personnel");
            return Ok(personnel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private async Task<Personnel> UpdateDataWithFile()
        {
            using (var uploadController = new UploadController<Personnel>())
            {
                uploadController.ControllerContext = this.ControllerContext;
                uploadController.Request = this.Request;
                var personnel = await uploadController.PostFileWithData("personnelFormData", "Personnel");
                if (personnel != null && uploadController.ListofFiles != null && uploadController.ListofFiles.Count > 0)
                {
                    personnel.UpdateFileProperties(personnel.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), this.Request, uploadController.ListofFiles);
                    //personnel.PersonnelPhoto = Path.GetFileName(uploadController.ListofFiles.GetDictionaryValue("PersonnelPhoto", personnel.PersonnelPhoto));
                    //personnel.OrganizationLogo = Path.GetFileName(uploadController.ListofFiles.GetDictionaryValue("OrganizationLogo", personnel.OrganizationLogo));
                    //personnel.Datasheet = Path.GetFileName(uploadController.ListofFiles.GetDictionaryValue("Datasheet", personnel.Datasheet));
                }

                return personnel;
            }
        }

        private bool PersonnelExists(string id)
        {
            return db.Personnels.Count(e => e.PersonnelID == id) > 0;
        }
    }
}