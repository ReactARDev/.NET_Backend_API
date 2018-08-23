using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using AMPS9000_WebAPI;
using System.Web.Http.Cors;
using System.Web.Http.Routing;
using System.Net.Http;

namespace AMPS9000_WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            config.MapHttpAttributeRoutes();

            ODataModelBuilder builder = new ODataConventionModelBuilder();
            config.Count().Filter().OrderBy().Select();
            config.AddODataQueryFilter();
            config.EnableDependencyInjection();

            builder.EntitySet<Alert>("Alerts");
            builder.EntitySet<BranchOfService>("BranchOfService");
            builder.EntitySet<Personnel>("Personnel");
            builder.EntitySet<PayGrade>("PayGrades");
            builder.EntitySet<Country>("Countries");
            builder.EntitySet<Rank>("Ranks");
            builder.EntitySet<MOS_Desc>("MOS");
            builder.EntitySet<Company>("Companies");
            builder.EntitySet<DutyPosition>("DutyPosition");
            builder.EntitySet<Unit>("Units");
            builder.EntitySet<COCOM>("COCOM");
            builder.EntitySet<Personnel>("Personnel");
            builder.EntitySet<Location>("Locations");
            builder.EntitySet<Region>("Regions");
            builder.EntitySet<Payload>("Payload");
            builder.EntitySet<Munition>("Munition");
            builder.EntitySet<MunitionsInventory>("MunitionInventory");
            builder.EntitySet<Platform>("Platform");
            builder.EntitySet<PlatformInventory>("PlatformInventory");
            builder.EntitySet<MissionType>("MissionType");
            builder.EntitySet<EEIThreat>("EEIThreat");
            builder.EntitySet<LIMIDSReq>("LIMIDSReq");
            builder.EntitySet<IntelReqEEI>("IntelReqEEI");
            builder.EntitySet<IntelRequest>("IntelRequest");
            builder.EntitySet<Payload>("Payload");
            builder.EntitySet<PayloadInventory>("PayloadInventory");
            builder.EntitySet<MapLayer>("MapLayer");
            builder.EntitySet<MapLayerCategory>("MapLayerCategory");
            builder.EntitySet<PlatformRole>("PlatformRoles");
            builder.EntitySet<MunitionRole>("MunitionRoles");
            builder.EntitySet<SpecialQualification>("SpecQuals");
            builder.EntitySet<IC_ISM_Classifications>("Clearance");
            builder.EntitySet<ComsType>("ComsType");
            builder.EntitySet<PayloadType>("PayloadType");
            builder.EntitySet<PayloadRole>("PayloadRole");
            builder.EntitySet<LocationCategory>("LocationCategory");
            builder.EntitySet<Order>("Order");
            builder.EntitySet<OrderType>("OrderType");
            builder.EntitySet<PlatformCategory>("PlatformCategory");
            builder.EntitySet<PlatformStatu>("PlatformStatus");
            builder.EntitySet<PayloadStatu>("PayloadStatus");
            builder.EntitySet<MunitionStatu>("MunitionStatus");
            builder.EntitySet<GroundControlSystem>("GroundControlSystem");
            builder.EntitySet<PersonnelStatu>("PersonnelStatus");
            builder.EntitySet<CCIRPIR>("CCIRPIR");
            config.MapODataServiceRoute("odata", "api", builder.GetEdmModel());

            // Web API routes
            //config.MapHttpAttributeRoutes();
            /*
            config.Routes.MapHttpRoute(
                name: "DefaultApiWithId",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { id = @"\d+" }
            );*/

            config.Routes.MapHttpRoute("DefaultApiWithId", "api/{controller}/{id}", new { id = RouteParameter.Optional }, new { id = @"\d+" });
            config.Routes.MapHttpRoute("DefaultApiWithAction", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional });
            config.Routes.MapHttpRoute("DefaultApiGet", "api/{controller}", new { action = "Get" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
            config.Routes.MapHttpRoute("DefaultApiPost", "api/{controller}", new { action = "Post" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });
            config.Routes.MapHttpRoute("DefaultApiPut", "api/{controller}/{id}", new { action = "Put" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Put), id = @"\d+" });
            config.Routes.MapHttpRoute("DefaultApiDelete", "api/{controller}/{id}", new { action = "Delete" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete), id = @"\d+" });
        }
    }
}
