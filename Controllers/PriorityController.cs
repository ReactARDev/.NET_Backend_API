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
    public class PriorityController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/StatusCodes
        public IQueryable<DropDownDTO> Get()
        {
            var results = (from a in db.Priorities
                           select new DropDownDTO
                           {
                               id = a.Id.ToString(),
                               description = a.Description.Trim()
                           });

            return results;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}