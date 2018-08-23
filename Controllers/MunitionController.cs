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
using AMPS9000_WebAPI.Filters;
using AMPS9000_WebAPI.Helper;

namespace AMPS9000_WebAPI.Controllers
{
    public class MunitionController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();
        string[] properties = {
            "MunitionPhoto",
            "MunitionWireframe",
            "Munition3D",
            "MunitionIcon",
            "MunitionDatasheet",
            "Munition2525B"
        };

        // GET: api/Munition
        public IQueryable<DropDownDTO> GetMunitions()
        {
            var result = (from a in db.Munitions
                          orderby a.LastUpdate descending, a.MunitionName ascending
                          select new DropDownDTO { id = a.MunitionID.ToString(), description = a.MunitionName }).AsQueryable();
            return result;
        }

        // GET: api/Munition/{guid}
        [ResponseType(typeof(Munition))]
        [GetBaseUrl("Munition/")]
        public IHttpActionResult GetMunition(string id)
        {
            Munition munition = db.Munitions.Find(id);
            if (munition == null)
            {
                return NotFound();
            }

            munition.AddUrl(munition.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), this.Request);
            //object uploadUrl;
            //Request.Properties.TryGetValue("uploadUrl", out uploadUrl);
            //munition.MunitionWireframe = uploadUrl + munition.MunitionWireframe;
            //munition.MunitionPhoto = uploadUrl + munition.MunitionPhoto;
            //munition.Munition3D = uploadUrl + munition.Munition3D;
            //munition.MunitionIcon = uploadUrl + munition.MunitionIcon;
            //munition.MunitionDatasheet = uploadUrl + munition.MunitionDatasheet;
            return Ok(munition);
        }

        // GET: api/GetMunitionsData
        [ResponseType(typeof(Munition))]
        [GetBaseUrl("Munition/")]
        public IHttpActionResult GetMunitionsData()
        {
            object uploadUrl;
            Request.Properties.TryGetValue("uploadUrl", out uploadUrl);
            var result = (from a in db.Munitions
                          join b in db.MunitionRoles on a.MunitionRole equals b.id into lojRole
                          from c in lojRole.DefaultIfEmpty()
                          join d in db.Companies on a.MunitionManufacturer equals d.id into lojComp
                          from e in lojComp.DefaultIfEmpty()
                          orderby a.LastUpdate descending
                          select new
                          {
                              ID = a.MunitionID,
                              type = c.description ?? "Unknown",
                              company = e.description.Trim() ?? "Unknown",
                              name = a.MunitionName,
                              role = c.description ?? "Unknown",
                              opsRange = a.MunitionRange,
                              weight = a.MunitionWeight,
                              munitionType = a.MunitionType,
                              manufacturer = e.description.Trim() ?? "Unknown",
                              munitionWireframe = uploadUrl + a.MunitionWireframe,
                              munitionPhoto = uploadUrl + a.MunitionPhoto,
                              munition3D = uploadUrl + a.Munition3D,
                              munitionIcon = uploadUrl + a.MunitionIcon,
                              munitionDatasheet = uploadUrl + a.MunitionDatasheet,
                              munition2525B = uploadUrl + a.Munition2525B
                          });

            return Ok(result);
        }


        // PUT: api/Munition/{guid}
        [ResponseType(typeof(void))]
        [GetBaseUrl("Munition/")]
        public async Task<IHttpActionResult> PutMunition(string id)
        {
            Munition munition = await UpdateDataWithFile();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != munition.MunitionID)
            {
                return BadRequest();
            }

            if (munition.MunitionName == null || munition.MunitionName.Trim() == "")
            {
                return BadRequest("Invalid Munition Name: " + munition.MunitionName);
            }

            if (!IsValidMunitionRole(munition.MunitionRole))
            {
                return BadRequest("Invalid Munition Role: " + munition.MunitionRole);
            }

            if (!IsValidMOS(munition.MunitionMOS1))
            {
                return BadRequest("Invalid MOS: " + munition.MunitionMOS1);
            }

            if (!IsValidMOS(munition.MunitionMOS2))
            {
                return BadRequest("Invalid MOS: " + munition.MunitionMOS2);
            }

            if (!IsValidMOS(munition.MunitionMOS3))
            {
                return BadRequest("Invalid MOS: " + munition.MunitionMOS3);
            }

            munition.RemoveUrl(munition.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), this.Request);
            munition.LastUpdate = DateTime.Now;
            db.Entry(munition).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MunitionExists(id))
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

        private bool IsValidMunitionRole(int? munitionRole)
        {
            if (munitionRole == null)
            {
                return true;  //true will be ignored as this is an optional field
            }
            else
            {
                return db.MunitionRoles.Count(e => e.id == munitionRole) > 0;
            }
        }

        // POST: api/Munition
        [ResponseType(typeof(Munition))]
        [GetBaseUrl("Munition/")]
        public async Task<IHttpActionResult> PostMunition()
        {
            Munition munition = await UpdateDataWithFile();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(munition.MunitionName == null || munition.MunitionName.Trim() == "")
            {
                return BadRequest("Invalid Munition Name: " + munition.MunitionName);
            }

            if (!IsValidMunitionRole(munition.MunitionRole))
            {
                return BadRequest("Invalid Munition Role: " + munition.MunitionRole);
            }

            if (!IsValidMOS(munition.MunitionMOS1))
            {
                return BadRequest("Invalid MOS: " + munition.MunitionMOS1);
            }

            if (!IsValidMOS(munition.MunitionMOS2))
            {
                return BadRequest("Invalid MOS: " + munition.MunitionMOS2);
            }

            if (!IsValidMOS(munition.MunitionMOS3))
            {
                return BadRequest("Invalid MOS: " + munition.MunitionMOS3);
            }

            munition.MunitionID = Guid.NewGuid().ToString();
            munition.LastUpdate = DateTime.Now;
            db.Munitions.Add(munition);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MunitionExists(munition.MunitionID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApiPost", new { id = munition.MunitionID }, munition);
        }

        // DELETE: api/Munition/{guid}
        [ResponseType(typeof(Munition))]
        public IHttpActionResult DeleteMunition(string id)
        {
            Munition munition = db.Munitions.Find(id);
            if (munition == null)
            {
                return NotFound();
            }

            if (db.MunitionsInventory.Any(x => x.metaDataID == munition.MunitionID))
            {
                return BadRequest("This specification cannot be deleted -- there are inventory items that use this specification.");
            }

            db.Munitions.Remove(munition);
            db.SaveChanges();
            munition.DeleteFiles(munition.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), "Munition");
            return Ok(munition);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MunitionExists(string id)
        {
            return db.Munitions.Count(e => e.MunitionID == id) > 0;
        }

        private bool IsValidMOS(int? MOS)
        {
            if (MOS == null)
            {
                return true;  //true will be ignored as this is an optional field
            }
            else
            {
                return db.MOS_Desc.Count(e => e.id == MOS) > 0;
            }
        }

        private async Task<Munition> UpdateDataWithFile()
        {
            using (var uploadController = new UploadController<Munition>())
            {
                uploadController.ControllerContext = this.ControllerContext;
                uploadController.Request = this.Request;
                var munition = await uploadController.PostFileWithData("munitionFormData", "Munition");
                if (munition != null && uploadController.ListofFiles != null && uploadController.ListofFiles.Count > 0)
                {
                    munition.UpdateFileProperties(munition.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), this.Request, uploadController.ListofFiles);
                }

                return munition;
            }
        }

        //private async Task<Munition> UpdateDataWithFile()
        //{
        //    using (var uploadController = new UploadController<Munition>())
        //    {
        //        uploadController.ControllerContext = this.ControllerContext;
        //        uploadController.Request = this.Request;
        //        var munition = await uploadController.PostFileWithData("personnelFormData", "Personnel");
        //        munition.MunitionPhoto = Path.GetFileName(uploadController.ListofFiles["munitionPhoto"]);
        //        munition.MunitionWireframe = Path.GetFileName(uploadController.ListofFiles["munitionWireframe"]);
        //        munition.Munition3D = Path.GetFileName(uploadController.ListofFiles["munition3D"]);
        //        munition.MunitionIcon = Path.GetFileName(uploadController.ListofFiles["munitionIcon"]);
        //        munition.MunitionDatasheet = Path.GetFileName(uploadController.ListofFiles["munitionDatasheet"]);
        //        return munition;
        //    }
        //}
    }
}