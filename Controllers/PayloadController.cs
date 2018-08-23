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
using System.Web.Http.Cors;
using System.Web.Http.Description;
using AMPS9000_WebAPI;
using AMPS9000_WebAPI.Filters;
using AMPS9000_WebAPI.Helper;

namespace AMPS9000_WebAPI.Controllers
{
    public class PayloadController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();
        string[] properties =
        {
            "PayloadPhoto",
            "PayloadWireframe",
            "Payload3D",
            "PayloadIcon",
            "PayloadDatasheet",
            "Payload2525B",
        };

        // GET: api/Payload
        public IQueryable<DropDownDTO> GetPayloads()
        {
            var result = (from a in db.Payloads
                          orderby a.LastUpdate descending, a.PayloadName ascending
                          select new DropDownDTO { id = a.PayloadID.ToString(), description = a.PayloadNomenclature }).AsQueryable();
            return result;
        }

        // GET: api/Payload/{guid}
        [ResponseType(typeof(Payload))]
        [GetBaseUrl("Payload/")]
        public IHttpActionResult GetPayload(string id)
        {
            object uploadUrl;
            Request.Properties.TryGetValue("uploadUrl", out uploadUrl);
            Payload payload = db.Payloads.Find(id);
            if (payload == null)
            {
                return NotFound();
            }

            payload.AddUrl(payload.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), this.Request);
            //payload.PayloadWireframe = uploadUrl + payload.PayloadWireframe;
            //payload.PayloadPhoto = uploadUrl + payload.PayloadPhoto;
            //payload.Payload3D = uploadUrl + payload.Payload3D;
            //payload.PayloadIcon = uploadUrl + payload.PayloadIcon;
            //payload.PayloadDatasheet = uploadUrl + payload.PayloadDatasheet;
            return Ok(payload);
        }

        // GET: api/Payload/GetPayloadsData
        [GetBaseUrl("Payload/")]
        public IHttpActionResult GetPayloadsData()
        {
            object uploadUrl;
            Request.Properties.TryGetValue("uploadUrl", out uploadUrl);
            var result = (from a in db.Payloads
                          join b in db.Companies on a.PayloadManufacturer equals b.id into lojComp
                          from c in lojComp.DefaultIfEmpty()
                          join d in db.PayloadTypes on a.PayloadType equals d.id into lojTyp
                          from e in lojTyp.DefaultIfEmpty()
                          join f in db.PayloadRole on a.MissionRole equals f.id into lojRole
                          from g in lojRole.DefaultIfEmpty()
                          orderby a.LastUpdate descending
                          select new
                          {
                              ID = a.PayloadID,
                              type = e.description ?? "Unknown",
                              typeID = a.PayloadType,
                              manufacturer = c.description ?? "Unknown",
                              name = a.PayloadName,
                              role = g.description ?? "Unknown",
                              weight = a.PayloadWeight,
                              power = a.PayloadPower,
                              payloadWireframe = uploadUrl + a.PayloadWireframe,
                              payloadPhoto = uploadUrl + a.PayloadPhoto,
                              payload3D = uploadUrl + a.Payload3D,
                              payloadIcon = uploadUrl + a.PayloadIcon,
                              payloadDatasheet = uploadUrl + a.PayloadDatasheet,
                              payload2525B = uploadUrl + a.Payload2525B
                          });

            return Ok(result);
        }

        // PUT: api/Payload/5
        [ResponseType(typeof(void))]
        [GetBaseUrl("Payload/")]
        public async Task<IHttpActionResult> PutPayload(string id)
        {
            Payload payload = await UpdateDataWithFile();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != payload.PayloadID)
            {
                return BadRequest();
            }

            if (payload.PayloadName == null || payload.PayloadName.Trim() == "")
            {
                return BadRequest("Invalid Payload Name: " + payload.PayloadName);
            }

            if (!IsValidMOS(payload.PayloadMOS1))
            {
                return BadRequest("Invalid MOS: " + payload.PayloadMOS1);
            }

            if (!IsValidMOS(payload.PayloadMOS2))
            {
                return BadRequest("Invalid MOS: " + payload.PayloadMOS2);
            }

            if (!IsValidMOS(payload.PayloadMOS3))
            {
                return BadRequest("Invalid MOS: " + payload.PayloadMOS3);
            }

            payload.RemoveUrl(payload.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), this.Request);
            payload.LastUpdate = DateTime.Now;
            db.Entry(payload).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayloadExists(id))
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

        // POST: api/Payload
        [ResponseType(typeof(Payload))]
        [GetBaseUrl("Payload/")]
        public async Task<IHttpActionResult> PostPayload()
        {
            Payload payload = await UpdateDataWithFile();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (payload.PayloadName == null || payload.PayloadName.Trim() == "")
            {
                return BadRequest("Invalid Payload Name: " + payload.PayloadName);
            }

            if (!IsValidMOS(payload.PayloadMOS1))
            {
                return BadRequest("Invalid MOS: " + payload.PayloadMOS1);
            }

            if (!IsValidMOS(payload.PayloadMOS2))
            {
                return BadRequest("Invalid MOS: " + payload.PayloadMOS2);
            }

            if (!IsValidMOS(payload.PayloadMOS3))
            {
                return BadRequest("Invalid MOS: " + payload.PayloadMOS3);
            }

            payload.PayloadID = Guid.NewGuid().ToString();
            payload.LastUpdate = DateTime.Now;
            db.Payloads.Add(payload);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PayloadExists(payload.PayloadID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApiPost", new { id = payload.PayloadID }, payload);
        }

        // DELETE: api/Payload/{guid}
        [ResponseType(typeof(Payload))]
        public IHttpActionResult DeletePayload(string id)
        {
            Payload payload = db.Payloads.Find(id);
            if (payload == null)
            {
                return NotFound();
            }

            if (db.PayloadInventory.Any(x => x.metaDataID == payload.PayloadID))
            {
                return BadRequest("This specification cannot be deleted -- there are inventory items that use this specification.");
            }

            db.Payloads.Remove(payload);
            db.SaveChanges();
            payload.DeleteFiles(payload.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), "Payload");
            return Ok(payload);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PayloadExists(string id)
        {
            return db.Payloads.Count(e => e.PayloadID == id) > 0;
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

        //private async Task<Payload> UpdateDataWithFile()
        //{
        //    using (var uploadController = new UploadController<Payload>())
        //    {
        //        uploadController.ControllerContext = this.ControllerContext;
        //        uploadController.Request = this.Request;
        //        var payload = await uploadController.PostFileWithData("payloadFormData", "Payload");
        //        payload.PayloadPhoto = Path.GetFileName(uploadController.ListofFiles["payloadPhoto"]);
        //        payload.PayloadWireframe = Path.GetFileName(uploadController.ListofFiles["payloadWireframe"]);
        //        payload.Payload3D = Path.GetFileName(uploadController.ListofFiles["payload3D"]);
        //        payload.PayloadIcon = Path.GetFileName(uploadController.ListofFiles["payloadIcon"]);
        //        payload.PayloadDatasheet = Path.GetFileName(uploadController.ListofFiles["payloadDatasheet"]);
        //        return payload;
        //    }
        //}

        private async Task<Payload> UpdateDataWithFile()
        {
            using (var uploadController = new UploadController<Payload>())
            {
                uploadController.ControllerContext = this.ControllerContext;
                uploadController.Request = this.Request;
                var payload = await uploadController.PostFileWithData("payloadFormData", "Payload");
                if (payload != null && uploadController.ListofFiles != null && uploadController.ListofFiles.Count > 0)
                {
                    payload.UpdateFileProperties(payload.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), this.Request, uploadController.ListofFiles);
                }

                return payload;
            }
        }
    }
}