using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AMPS9000_WebAPI.Filters
{
    public class GetBaseUrlAttribute : ActionFilterAttribute
    {
        private readonly string modulePath;
        public GetBaseUrlAttribute(string modulePath)
        {
            this.modulePath = modulePath;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // pre-processing
            string baseUrl = actionContext.Request.RequestUri.AbsoluteUri.Substring(0, actionContext.Request.RequestUri.AbsoluteUri.IndexOf("api"));
            if(actionContext.Request.Properties.ContainsKey("uploadUrl"))
            {
                actionContext.Request.Properties.Remove("uploadUrl");
            }

            if (!actionContext.Request.Properties.ContainsKey("baseUrl"))
            {
                actionContext.Request.Properties.Add("baseUrl", baseUrl);
            }
                       
            actionContext.Request.Properties.Add("uploadUrl", Path.Combine(baseUrl, "Content/UploadFiles/", modulePath));
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
        }
    }
}