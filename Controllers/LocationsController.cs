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
using System.Web;
using System.IO;
using AMPS9000_WebAPI.Filters;
using AMPS9000_WebAPI.Helper;

namespace AMPS9000_WebAPI.Controllers
{
    public class LocationsController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();
        private string[] properties = {
            "LocationPhoto",
            "LocationMapImage",
            "LocationDocument",
            "KML"
        };

        // GET: api/Locations
        public IQueryable<DropDownDTO> GetLocations()
        {
            var result = (from a in db.Locations
                          orderby a.LastUpdate descending, a.LocationName ascending
                          select new DropDownDTO { id = a.LocationID.ToString(), description = a.LocationName.Trim() }).AsQueryable();
            return result;
        }

        public IHttpActionResult GetLocationsByCategory(int Category)
        {
            if (db.LocationCategories.Where(x => x.id == Category).Count() <= 0)
            {
                return NotFound();
            }

            var result = (from a in db.Locations
                          where a.LocationCategory == Category
                          orderby a.LastUpdate descending, a.LocationName ascending
                          select new DropDownDTO { id = a.LocationID.ToString(), description = a.LocationName.Trim() });

            return Ok(result);
        }

        // GET: api/Locations/{guid}
        [ResponseType(typeof(Location))]
        [GetBaseUrl("Location/")]
        public IHttpActionResult GetLocations(string id)
        {
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            location.AddUrl(location.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), this.Request);

            //object uploadUrl;
            //Request.Properties.TryGetValue("uploadUrl", out uploadUrl);
            //location.LocationPhoto = uploadUrl + location.LocationPhoto;
            //location.LocationDocument = uploadUrl + location.LocationDocument;
            //location.KML = uploadUrl + location.KML;
            //location.LocationMapImage = uploadUrl + location.LocationMapImage;

            return Ok(location);
        }

        // GET: api/Locations/{guid}
        [ResponseType(typeof(Location))]
        [GetBaseUrl("Location/")]
        public IHttpActionResult GetLocationsData(string id)
        {
            return this._GetLocationsData(id);
        }

        // GET: api/Locations/GetLocationsData
        [GetBaseUrl("Location/")]
        public IHttpActionResult GetLocationsData()
        {
            return this._GetLocationsData();
        }

        // GET: api/Locations/GetLocationsData
        [GetBaseUrl("Location/")]
        public IHttpActionResult GetLocationsDataByCategory(int categoryId)
        {
            return this._GetLocationsData(null, categoryId);
        }

        // GET: api/Locations/GetUserLocationIDUnique
        public IHttpActionResult GetUserLocationIDUnique(string userLocID)
        {
            return Ok(!db.Locations.Any(x => x.UserLocationID == userLocID));
        }

        // PUT: api/Locations/{guid}
        [ResponseType(typeof(void))]
        [GetBaseUrl("Location/")]
        public async Task<IHttpActionResult> PutLocations(string id)
        {
            Location locations = await UpdateDataWithFile();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != locations.LocationID)
            {
                return BadRequest();
            }

            if (locations.LocationName == null || locations.LocationName.ToString().Trim() == "")
            {
                return BadRequest("Invalid Location Name");
            }

            if (locations.LocationRegion == null)
            {
                return BadRequest("Invalid Region");
            }
            else if (db.Regions.Where(x => x.id == locations.LocationRegion).Count() <= 0)
            {
                return BadRequest("Invalid Region");
            }

            if (locations.LocationCountry == null)
            {
                return BadRequest("Invalid Country");
            }
            else if (db.Countries.Where(x => x.id == locations.LocationCountry).Count() <= 0)
            {
                return BadRequest("Invalid Country");
            }

            if (locations.LocationCOCOM == null || locations.LocationCOCOM.ToString().Trim() == "")
            {
                return BadRequest("Invalid COCOM");
            }
            else if (db.COCOMs.Where(x => x.id == locations.LocationCOCOM).Count() <= 0)
            {
                return BadRequest("Invalid COCOM");
            }

            if (locations.LocationCategory != null && locations.LocationCategory.ToString().Trim() != "")
            {
                if (db.LocationCategories.Where(x => x.id == locations.LocationCategory).Count() <= 0)
                {
                    return BadRequest("Invalid Location Category");
                }
            }

            locations.RemoveUrl(locations.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), this.Request);
            //object uploadUrl;
            //Request.Properties.TryGetValue("uploadUrl", out uploadUrl);
            //locations.KML = locations.KML.Replace(uploadUrl.ToString(), string.Empty);
            //locations.LocationPhoto = locations.LocationPhoto.Replace(uploadUrl.ToString(), string.Empty);
            //locations.LocationDocument = locations.LocationDocument.Replace(uploadUrl.ToString(), string.Empty);
            //locations.LocationMapImage = locations.LocationMapImage.Replace(uploadUrl.ToString(), string.Empty);
            locations.LastUpdate = DateTime.Now;

            db.Entry(locations).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                if (!LocationsExists(id))
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

        // POST: api/Locations
        [ResponseType(typeof(Location))]
        [GetBaseUrl("Location/")]
        public async Task<IHttpActionResult> PostLocations()
        {
            Location locations = await UpdateDataWithFile();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (locations.LocationName == null || locations.LocationName.ToString().Trim() == "")
            {
                return BadRequest("Invalid Location Name");
            }

            if (locations.LocationRegion == null)
            {
                return BadRequest("Invalid Region");
            }
            else if (db.Regions.Where(x => x.id == locations.LocationRegion).Count() <= 0)
            {
                return BadRequest("Invalid Region");
            }

            if (locations.LocationCountry == null)
            {
                return BadRequest("Invalid Country");
            }
            else if (db.Countries.Where(x => x.id == locations.LocationCountry).Count() <= 0)
            {
                return BadRequest("Invalid Country");
            }

            if (locations.LocationCOCOM == null || locations.LocationCOCOM.ToString().Trim() == "")
            {
                return BadRequest("Invalid COCOM");
            }
            else if (db.COCOMs.Where(x => x.id == locations.LocationCOCOM).Count() <= 0)
            {
                return BadRequest("Invalid COCOM");
            }

            if (locations.LocationCategory != null && locations.LocationCategory.ToString().Trim() != "")
            {
                if (db.LocationCategories.Where(x => x.id == locations.LocationCategory).Count() <= 0)
                {
                    return BadRequest("Invalid Location Category");
                }
            }

            locations.LocationID = Guid.NewGuid().ToString();
            locations.LastUpdate = DateTime.Now;
            db.Locations.Add(locations);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (LocationsExists(locations.LocationID))
                {
                    return Conflict();
                }
                else
                {
                    throw ex;
                }
            }

            return CreatedAtRoute("DefaultApiPost", new { id = locations.LocationID }, locations);
        }

        // DELETE: api/Locations/5
        [ResponseType(typeof(Location))]
        public IHttpActionResult DeleteLocations(string id)
        {
            Location locations = db.Locations.Find(id);
            if (locations == null)
            {
                return NotFound();
            }

            db.Locations.Remove(locations);
            db.SaveChanges();
            locations.DeleteFiles(locations.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), "Location");
            return Ok(locations);
        }

        public async Task<Location> UpdateDataWithFile()
        {
            using (var uploadController = new UploadController<Location>())
            {
                uploadController.ControllerContext = this.ControllerContext;
                uploadController.Request = this.Request;
                var location = await uploadController.PostFileWithData("locationFormData", "Location");
                if (location != null && uploadController.ListofFiles != null && uploadController.ListofFiles.Count > 0)
                {
                    location.UpdateFileProperties(location.GetType().GetProperties().Where(p => properties.Contains(p.Name)).ToArray(), this.Request, uploadController.ListofFiles);
                    //location.LocationPhoto = Path.GetFileName(controller.ListofFiles.GetDictionaryValue("locationPhotoFile", location.LocationPhoto));
                    //location.LocationMapImage = Path.GetFileName(controller.ListofFiles.GetDictionaryValue("locationMapFile", location.LocationMapImage));
                    //location.LocationDocument = Path.GetFileName(controller.ListofFiles.GetDictionaryValue("locationDocumentFile", location.LocationDocument));
                    //location.KML = Path.GetFileName(controller.ListofFiles.GetDictionaryValue("locationKMLFile", location.KML));
                }

                return location;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LocationsExists(string id)
        {
            return db.Locations.Count(e => e.LocationID == id) > 0;
        }

        private IHttpActionResult _GetLocationsData(string id = null, int categoryId = 0)
        {
            object uploadUrl;
            Request.Properties.TryGetValue("uploadUrl", out uploadUrl);
            var location = (from a in db.Locations
                            join b in db.COCOMs on a.LocationCOCOM equals b.id into lojCOCOM
                            join c in db.Regions on a.LocationRegion equals c.id into lojReg
                            join d in db.Countries on a.LocationCountry equals d.id into lojCountry
                            join d2 in db.LocationCategories on a.LocationCategory equals d2.id into lojCatgs
                            from e in lojCOCOM.DefaultIfEmpty()
                            from f in lojReg.DefaultIfEmpty()
                            from g in lojCountry.DefaultIfEmpty()
                            from h in lojCatgs.DefaultIfEmpty()
                            where id != null && id.Trim() != "" ? a.LocationID == id : (categoryId > 0 ? a.LocationCategory == categoryId : 1 == 1)
                            orderby a.LocationName ascending
                            select new
                            {
                                id = a.LocationID.ToString(),
                                type = h.description ?? "Unknown",
                                name = a.LocationName.Trim(),
                                COCOM = e.description == null ? "Unknown" : e.description.Trim(),
                                region = f.description == null ? "Unknown" : f.description.Trim(),
                                country = g.description == null ? "Unknown" : g.description.Trim(),
                                city = a.LocationCity.Trim() ?? "Unknown",
                                MGRS = a.LocationMGRS.Trim() ?? "Unknown",
                                locationID = a.UserLocationID.Trim() ?? "",
                                locationPhoto = uploadUrl + a.LocationPhoto,
                                locationDocument = uploadUrl + a.LocationDocument,
                                locationKML = uploadUrl + a.KML,
                                locationMapImage = uploadUrl + a.LocationMapImage
                            }).AsQueryable();
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }
    }
}