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

namespace AMPS9000_WebAPI.Controllers
{
    public class CountriesController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/Countries
        public IQueryable<DropDownDTO> GetCountries()
        {
            var result = (from a in db.Countries
                          orderby a.displayOrder, a.description
                          select new DropDownDTO { id = a.id.ToString(), description = a.description.Trim() }).AsQueryable();
            return result;
        }

        // GET: api/Countries/5
        [ResponseType(typeof(Country))]
        public IHttpActionResult GetCountries(string id)
        {
            Country countries = db.Countries.Find(id);
            if (countries == null)
            {
                return NotFound();
            }

            return Ok(new DropDownDTO
            {
                id = countries.id,
                description = countries.description
            });
        }

        // PUT: api/Countries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCountries(string id, Country countries)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != countries.id)
            {
                return BadRequest();
            }

            db.Entry(countries).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountriesExists(id))
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

        // POST: api/Countries
        [ResponseType(typeof(Country))]
        public IHttpActionResult PostCountries(Country countries)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Countries.Add(countries);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CountriesExists(countries.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApiPost", new { id = countries.id }, countries);
        }

        // DELETE: api/Countries/5
        [ResponseType(typeof(Country))]
        public IHttpActionResult DeleteCountries(string id)
        {
            Country countries = db.Countries.Find(id);
            if (countries == null)
            {
                return NotFound();
            }

            db.Countries.Remove(countries);
            db.SaveChanges();

            return Ok(countries);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CountriesExists(string id)
        {
            return db.Countries.Count(e => e.id == id) > 0;
        }
    }
}