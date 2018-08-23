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
    public class CompaniesController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/Companies
        public IQueryable<DropDownDTO> GetCompanies()
        {
            var result = (from a in db.Companies
                          orderby a.DisplayOrder ascending
                          select new DropDownDTO { id = a.id.ToString(), description = a.description }).AsQueryable();
            return result;
        }

        // GET: api/Companies/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult GetCompanies(int id)
        {
            Company companies = db.Companies.Find(id);
            if (companies == null)
            {
                return NotFound();
            }

            return Ok(new DropDownDTO
            {
                id = companies.id.ToString(),
                description = companies.description
            });
        }

        // PUT: api/Companies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompanies(int id, Company companies)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != companies.id)
            {
                return BadRequest();
            }

            db.Entry(companies).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompaniesExists(id))
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

        // POST: api/Companies
        [ResponseType(typeof(Company))]
        public IHttpActionResult PostCompanies(Company companies)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Companies.Add(companies);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApiPost", new { id = companies.id }, companies);
        }

        // DELETE: api/Companies/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult DeleteCompanies(int id)
        {
            Company companies = db.Companies.Find(id);
            if (companies == null)
            {
                return NotFound();
            }

            db.Companies.Remove(companies);
            db.SaveChanges();

            return Ok(companies);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompaniesExists(int id)
        {
            return db.Companies.Count(e => e.id == id) > 0;
        }
    }
}