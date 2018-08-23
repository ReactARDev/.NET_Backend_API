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
using System.Web;
using System.Threading.Tasks;

namespace AMPS9000_WebAPI.Controllers
{
    public class MapLayerController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/MapLayer
        public IQueryable<MapLayer> GetMapLayers()
        {
            return db.MapLayers;
        }

        // GET: api/MapLayer/5
        [ResponseType(typeof(MapLayer))]
        public IHttpActionResult GetMapLayer(int id)
        {
            MapLayer mapLayer = db.MapLayers.Find(id);
            if (mapLayer == null)
            {
                return NotFound();
            }

            return Ok(mapLayer);
        }

        // PUT: api/MapLayer/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMapLayer(int id, MapLayer mapLayer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mapLayer.id)
            {
                return BadRequest();
            }

            db.Entry(mapLayer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MapLayerExists(id))
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

        // POST: api/MapLayer
        [ResponseType(typeof(MapLayer))]
        public async Task<IHttpActionResult> PostMapLayer()
        {
            MapLayer mapLayer = new MapLayer();

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (MultipartFileData file in provider.FileData)
                {
                    //Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    //Trace.WriteLine("Server file path: " + file.LocalFileName);
                    mapLayer.fileLocation = file.LocalFileName;
                }

                foreach (var key in provider.FormData.AllKeys)
                {
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        if (key == "name")
                        {
                            mapLayer.name = val;
                        } else if (key == "categoryID")
                        {
                            mapLayer.categoryID = int.Parse(val);
                        } else if(key == "description")
                        {
                            mapLayer.description = val;
                        }
                    }
                }
            } catch(System.Exception e)
            {
                return BadRequest(e.Message);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MapLayers.Add(mapLayer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApiPost", new { id = mapLayer.id }, mapLayer);
        }

        // DELETE: api/MapLayer/5
        [ResponseType(typeof(MapLayer))]
        public IHttpActionResult DeleteMapLayer(int id)
        {
            MapLayer mapLayer = db.MapLayers.Find(id);
            if (mapLayer == null)
            {
                return NotFound();
            }

            db.MapLayers.Remove(mapLayer);
            db.SaveChanges();

            return Ok(mapLayer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MapLayerExists(int id)
        {
            return db.MapLayers.Count(e => e.id == id) > 0;
        }
    }
}