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
    public class StatusCodesController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        // GET: api/StatusCodes
        public IQueryable<DropDownDTO> GetPersonnelStatusCodes()
        {
            var results = (from a in db.StatusCodes
                           where a.type == 1
                           orderby a.displayOrder, a.description
                           select new DropDownDTO
                           {
                               id = a.id.ToString(),
                               description = a.description.Trim()
                           });

            return results;
        }

        public IQueryable<DropDownDTO> GetIntelReqStatusCodes()
        {
            var results = (from a in db.StatusCodes
                           where a.type == 2
                           orderby a.displayOrder, a.description
                           select new DropDownDTO
                           {
                               id = a.id.ToString(),
                               description = a.description.Trim()
                           });

            return results;
        }

        public IQueryable<DropDownDTO> GetAssetStatusCodes()
        {
            var results = (from a in db.StatusCodes
                           where a.type == 3
                           orderby a.displayOrder, a.description
                           select new DropDownDTO
                           {
                               id = a.id.ToString(),
                               description = a.description.Trim()
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