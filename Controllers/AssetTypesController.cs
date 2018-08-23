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
    public class AssetTypesController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/AssetTypes
        public IQueryable<DropDownDTO> GetAssetTypes()
        {
            var result = (from a in db.AssetTypes
                          orderby a.description ascending
                          select new DropDownDTO { id = a.id.ToString(), description = a.description }).AsQueryable();
            return result;
        }

        // GET: api/AssetTypes/5
        [ResponseType(typeof(AssetType))]
        public IHttpActionResult GetAssetType(int id)
        {
            AssetType assetTypes = db.AssetTypes.Find(id);
            if (assetTypes == null)
            {
                return NotFound();
            }

            return Ok(assetTypes);
        }

        // PUT: api/PutAssetType/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAssetType(int id, AssetType assetTypes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assetTypes.id)
            {
                return BadRequest();
            }

            db.Entry(assetTypes).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetTypesExists(id))
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

        // POST: api/AssetType
        [ResponseType(typeof(AssetType))]
        public IHttpActionResult PostAssetType(AssetType assetTypes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AssetTypes.Add(assetTypes);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApiPost", new { id = assetTypes.id }, assetTypes);
        }

        // DELETE: api/AssetType/5
        [ResponseType(typeof(AssetType))]
        public IHttpActionResult DeleteAssetType(int id)
        {
            AssetType assetType = db.AssetTypes.Find(id);
            if (assetType == null)
            {
                return NotFound();
            }

            db.AssetTypes.Remove(assetType);
            db.SaveChanges();

            return Ok(assetType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssetTypesExists(int id)
        {
            return db.AssetTypes.Count(e => e.id == id) > 0;
        }
    }
}