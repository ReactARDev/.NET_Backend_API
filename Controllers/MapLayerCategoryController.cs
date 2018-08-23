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
    public class MapLayerCategoryController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/MapLayerCategory
        public IQueryable<MapLayerCategory> GetMapLayerCategories()
        {
            return db.MapLayerCategories;
        }

        // GET: api/MapLayerCategory/5
        [ResponseType(typeof(MapLayerCategory))]
        public IHttpActionResult GetMapLayerCategory(int id)
        {
            MapLayerCategory mapLayerCategory = db.MapLayerCategories.Find(id);
            if (mapLayerCategory == null)
            {
                return NotFound();
            }

            return Ok(mapLayerCategory);
        }

        // PUT: api/MapLayerCategory/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMapLayerCategory(int id, MapLayerCategory mapLayerCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mapLayerCategory.id)
            {
                return BadRequest();
            }

            db.Entry(mapLayerCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MapLayerCategoryExists(id))
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

        // POST: api/MapLayerCategory
        [ResponseType(typeof(MapLayerCategory))]
        public IHttpActionResult PostMapLayerCategory(MapLayerCategory mapLayerCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mapLayerCategory.createDate = DateTime.Now;

            db.MapLayerCategories.Add(mapLayerCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApiPost", new { id = mapLayerCategory.id }, mapLayerCategory);
        }

        // DELETE: api/MapLayerCategory/5
        [ResponseType(typeof(MapLayerCategory))]
        public IHttpActionResult DeleteMapLayerCategory(int id)
        {
            MapLayerCategory mapLayerCategory = db.MapLayerCategories.Find(id);
            if (mapLayerCategory == null)
            {
                return NotFound();
            }

            db.MapLayerCategories.Remove(mapLayerCategory);
            db.SaveChanges();

            return Ok(mapLayerCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MapLayerCategoryExists(int id)
        {
            return db.MapLayerCategories.Count(e => e.id == id) > 0;
        }
    }
}