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
    public class PlatformController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();
        string[] properties =
        {
            "PlatformPhoto",
            "PlatformWireframe",
            "Platform3D",
            "PlatformIcon",
            "PlatformDatasheet"
        };

        // GET: api/Platform
        public IQueryable<DropDownDTO> GetPlatforms()
        {
            var result = (from a in db.Platforms
                          orderby a.LastUpdate descending, a.PlatformName ascending
                          select new DropDownDTO { id = a.PlatformID.ToString(), description = a.PlatformName }).AsQueryable();
            return result;
        }

        // GET: api/Platform/{guid}
        [ResponseType(typeof(Platform))]
        [GetBaseUrl("Platform/")]
        public IHttpActionResult GetPlatform(string id)
        {
            object uploadUrl;
            Request.Properties.TryGetValue("uploadUrl", out uploadUrl);
            Platform platform = db.Platforms.Find(id);
            if (platform == null)
            {
                return NotFound();
            }

            platform.AddUrl(platform.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), this.Request);
            //platform.PlatformWireframe = uploadUrl + platform.PlatformWireframe;
            //platform.PlatformPhoto = uploadUrl + platform.PlatformPhoto;
            //platform.Platform3D = uploadUrl + platform.Platform3D;
            //platform.PlatformIcon = uploadUrl + platform.PlatformIcon;
            //platform.PlatformDatasheet = uploadUrl + platform.PlatformDatasheet;
            return Ok(platform);
        }

        // GET: api/platform/GetPlatformsData
        [GetBaseUrl("Platform/")]
        public IHttpActionResult GetPlatformsData()
        {
            object uploadUrl;
            Request.Properties.TryGetValue("uploadUrl", out uploadUrl);
            var result = (from a in db.Platforms
                          join b in db.PlatformCategories on a.PlatformCategory equals b.id into lojCatg
                          join c in db.PlatformRoles on a.PlatformRole equals c.id into lojRole
                          join c1 in db.Companies on a.PlatformManufacturer equals c1.id into lojMan
                          from d in lojCatg.DefaultIfEmpty()
                          from e in lojRole.DefaultIfEmpty()
                          from f in lojMan.DefaultIfEmpty()
                          orderby a.LastUpdate descending
                          select new
                          {
                              ID = a.PlatformID,
                              name = a.PlatformName,
                              manufacturer = f.description.Trim() ?? "Unknown",
                              category = d.description.Trim() ?? "Unknown",
                              role = e.description.Trim() ?? "Unknown",
                              payloadCapacity = a.PlatformPayloadCapacity,
                              armamentCapacity = a.PlatformArmamentCapacity,
                              platformWireframe = uploadUrl + a.PlatformWireframe,
                              platformPhoto = uploadUrl + a.PlatformPhoto,
                              platform3D = uploadUrl + a.Platform3D,
                              platformIcon = uploadUrl + a.PlatformIcon,
                              platformDatasheet = uploadUrl + a.PlatformDatasheet
                          });

            return Ok(result);
        }

        // PUT: api/Platform/{guid}
        [ResponseType(typeof(void))]
        [GetBaseUrl("Platform/")]
        public async Task<IHttpActionResult> PutPlatform(string id)
        {
            Platform platform = await UpdateDataWithFile();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != platform.PlatformID)
            {
                return BadRequest();
            }

            if (platform.PlatformName == null || platform.PlatformName.Trim() == "")
            {
                return BadRequest("Invalid Platform Name: " + platform.PlatformName);
            }

            if (!PlatformCategoryExists(platform.PlatformCategory))
            {
                return BadRequest("Invalid Platform Category: " + platform.PlatformCategory);
            }

            if (!PlatformRoleExists(platform.PlatformRole))
            {
                return BadRequest("Invalid Platform Role: " + platform.PlatformRole);
            }

            if (!IsValidMOS(platform.PlatformFlightCrewMOS))
            {
                return BadRequest("Invalid MOS: " + platform.PlatformFlightCrewMOS);
            }

            if (!IsValidMOS(platform.PlatformLineCrewMOS))
            {
                return BadRequest("Invalid MOS: " + platform.PlatformLineCrewMOS);
            }

            if (!IsValidMOS(platform.PlatformPayloadCrewMOS))
            {
                return BadRequest("Invalid MOS: " + platform.PlatformPayloadCrewMOS);
            }

            if (!IsValidMOS(platform.PlatformPEDCrewMOS))
            {
                return BadRequest("Invalid MOS: " + platform.PlatformPEDCrewMOS);
            }

            platform.RemoveUrl(platform.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), this.Request);
            platform.LastUpdate = DateTime.Now;
            db.Entry(platform).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlatformExists(id))
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

        // POST: api/Platform
        [ResponseType(typeof(Platform))]
        [GetBaseUrl("Platform/")]
        public async Task<IHttpActionResult> PostPlatform()
        {
            Platform platform = await UpdateDataWithFile();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(platform.PlatformName == null || platform.PlatformName.Trim() == "")
            {
                return BadRequest("Invalid Platform Name: " + platform.PlatformName);
            }

            if(!PlatformCategoryExists(platform.PlatformCategory))
            {
                return BadRequest("Invalid Platform Category: " + platform.PlatformCategory);
            }

            if(!PlatformRoleExists(platform.PlatformRole))
            {
                return BadRequest("Invalid Platform Role: " + platform.PlatformRole);
            }

            if(!IsValidMOS(platform.PlatformFlightCrewMOS))
            {
                return BadRequest("Invalid MOS: " + platform.PlatformFlightCrewMOS);
            }

            if (!IsValidMOS(platform.PlatformLineCrewMOS))
            {
                return BadRequest("Invalid MOS: " + platform.PlatformLineCrewMOS);
            }

            if (!IsValidMOS(platform.PlatformPayloadCrewMOS))
            {
                return BadRequest("Invalid MOS: " + platform.PlatformPayloadCrewMOS);
            }

            if (!IsValidMOS(platform.PlatformPEDCrewMOS))
            {
                return BadRequest("Invalid MOS: " + platform.PlatformPEDCrewMOS);
            }

            platform.PlatformID = Guid.NewGuid().ToString();
            platform.LastUpdate = DateTime.Now;
            db.Platforms.Add(platform);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PlatformExists(platform.PlatformID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApiPost", new { id = platform.PlatformID }, platform);
        }

        private bool IsValidMOS(int? platformFlightCrewMOS)
        {
            if (platformFlightCrewMOS == null)
            {
                return true;  //true will be ignored as this is an optional field
            }
            else
            {
                return db.MOS_Desc.Count(e => e.id == platformFlightCrewMOS) > 0;
            }
        }

        private bool PlatformRoleExists(int? platformRole)
        {
            if (platformRole == null)
            {
                return true;  //true will be ignored as this is an optional field
            }
            else
            {
                return db.PlatformRoles.Count(e => e.id == platformRole) > 0;
            }
        }

        private bool PlatformCategoryExists(int? platformCategory)
        {
            if (platformCategory == null)
            {
                return true;  //true will be ignored as this is an optional field
            }
            else
            {
                return db.PlatformCategories.Count(e => e.id == platformCategory) > 0;
            }
        }

        // DELETE: api/Platform/{guid}
        [ResponseType(typeof(Platform))]
        public IHttpActionResult DeletePlatform(string id)
        {
            Platform platform = db.Platforms.Find(id);
            if (platform == null)
            {
                return NotFound();
            }

            if (db.PlatformInventory.Any(x => x.metaDataID == platform.PlatformID))
            {
                return BadRequest("This specification cannot be deleted -- there are inventory items that use this specification.");
            }

            db.Platforms.Remove(platform);
            db.SaveChanges();
            platform.DeleteFiles(platform.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), "Platform");

            return Ok(platform);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlatformExists(string id)
        {
            return db.Platforms.Count(e => e.PlatformID == id) > 0;
        }

        private bool PayloadExists(string id)
        {
            return db.Payloads.Count(e => e.PayloadID == id) > 0;
        }

        private async Task<Platform> UpdateDataWithFile()
        {
            using (var uploadController = new UploadController<Platform>())
            {
                uploadController.ControllerContext = this.ControllerContext;
                uploadController.Request = this.Request;
                var platform = await uploadController.PostFileWithData("platformFormData", "Platform");
                if (platform != null && uploadController.ListofFiles != null && uploadController.ListofFiles.Count > 0)
                {
                    platform.UpdateFileProperties(platform.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), this.Request, uploadController.ListofFiles);
                }

                return platform;
            }
        }

        //private async Task<Platform> UpdateDataWithFile()
        //{
        //    using (var uploadController = new UploadController<Platform>())
        //    {
        //        uploadController.ControllerContext = this.ControllerContext;
        //        uploadController.Request = this.Request;
        //        var platform = await uploadController.PostFileWithData("platformFormData", "Platform");
        //        platform.PlatformPhoto = Path.GetFileName(uploadController.ListofFiles["platformPhoto"]);
        //        platform.PlatformWireframe = Path.GetFileName(uploadController.ListofFiles["platformWireframe"]);
        //        platform.Platform3D = Path.GetFileName(uploadController.ListofFiles["platform3D"]);
        //        platform.PlatformIcon = Path.GetFileName(uploadController.ListofFiles["platformIcon"]);
        //        platform.PlatformDatasheet = Path.GetFileName(uploadController.ListofFiles["platformDatasheet"]);
        //        return platform;
        //    }
        //}
    }
}