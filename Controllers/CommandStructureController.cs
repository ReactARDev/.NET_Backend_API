using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AMPS9000_WebAPI.Controllers
{
    /// <summary>
    /// Org Structure
    /// </summary>
    public class CommandStructureController : ApiController
    {
        private AMPS9000DB db = new AMPS9000DB();

        /// <summary>
        /// Returns Command Structure based on branch
        /// </summary>
        /// <remarks>
        /// Units are "tied" together by using their <c>CommandEntityStructID</c>.  
        /// 
        /// The top of tree has a null for it's <c>superiorCommandStructID</c>, the next layer down has the root's <c>CommandStructID</c> in their <c>superiorCommandStructID</c> field.
        /// 
        /// The <c>tier</c> value will indicate how deep they are in the tree.
        /// </remarks>
        /// <param name="branchID">(int) The ID of the service branch (Required)</param>
        /// <param name="CommandRelationship">(int) Default:Organic = 1 / Assigned (Deployed) = 2 (Optional)</param>
        /// <param name="rootUnitID">(int) Starts the Command Structure at a specific unit and lists all of it's dependants (Optional)</param>
        public IHttpActionResult GetCommandStructureByBranch(int branchID, int CommandRelationship = 1, int? rootUnitID = null)
        {
            List<CommandStructureDTO> structures = null;
            string query;
            SqlParameter[] sqlParms;

            try
            {
                sqlParms = new SqlParameter[]
                {
                    new SqlParameter{ParameterName = "@branchID", Value = branchID, Direction = System.Data.ParameterDirection.Input },
                    new SqlParameter{ParameterName = "@rootUnit", Value = (object)rootUnitID ?? DBNull.Value, Direction = System.Data.ParameterDirection.Input},
                    new SqlParameter{ParameterName = "@commandRelationship", Value = CommandRelationship, Direction = System.Data.ParameterDirection.Input}
                };

                query = "exec GetCommandStructureByBranch @branchID, @rootUnit, @commandRelationship";

                structures = db.Database.SqlQuery<CommandStructureDTO>(query, sqlParms).ToList<CommandStructureDTO>();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(structures);
        }

        /// <summary>
        /// Returns the unit directly above the <c>unitID</c>
        /// </summary>
        /// <remarks>
        /// Returns the unit directly above the <code>unitID</code>
        /// </remarks>
        /// <param name="unitID">(int) The unit whose superior will be returned</param>
        /// <param name="CommandRelationship">(int) Default:Organic = 1 / Assigned (Deployed) = 2 (Optional)</param>
        /// <returns><c>unitID</c> and <c>unitName</c> for the superior unit</returns>
        public IHttpActionResult GetSuperiorUnit(int unitID, int CommandRelationship = 1)
        {
            var result = (from a in db.CommandStructure
                          join c in db.Units on a.unitID equals c.id into lojUnit
                          from d in lojUnit.DefaultIfEmpty()
                          where a.commandRelationship == CommandRelationship &&
                          a.id == (from b in db.CommandStructure
                                   where b.commandRelationship == CommandRelationship &&
                                   b.unitID == unitID select b.superiorCommandStructID).FirstOrDefault()
                          select new
                          {
                              unitID = a.unitID,
                              unitName = d.description.Trim() ?? "Unknown"
                          });
            return Ok(result);
        }
    }
}
