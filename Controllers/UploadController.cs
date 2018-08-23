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
using System.Threading.Tasks;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;
using AMPS9000_WebAPI.Helper;

namespace AMPS9000_WebAPI.Controllers
{
    public class UploadController<T> : ApiController
    {
        public IDictionary<string, string> ListofFiles
        {
            get; set;
        }

        public UploadController()
        {
            this.ListofFiles = new Dictionary<string, string>();
        }

        public async Task<T> PostFileWithData(string itemKey, string path = "")
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var root = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Uploadfiles"), path);
            Directory.CreateDirectory(root);
            var provider = new MultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);
            var model = result.FormData[itemKey];

            //get the posted files  
            foreach (var file in result.FileData)
            {
                var name = (string.IsNullOrWhiteSpace(file.Headers.ContentDisposition.Name) ? file.Headers.ContentDisposition.FileName : file.Headers.ContentDisposition.Name).Replace("\"", string.Empty);
                var fileName = file.LocalFileName.Replace("BodyPart", Path.GetFileNameWithoutExtension(file.Headers.ContentDisposition.FileName.Replace("\"", string.Empty)));
                if (string.IsNullOrWhiteSpace(Path.GetExtension(fileName)))
                {
                    fileName = string.Concat(fileName, Path.GetExtension(file.Headers.ContentDisposition.FileName.Replace("\"", string.Empty)));
                    File.Move(file.LocalFileName, fileName);
                }

                this.ListofFiles.Add(name, fileName);
            }

            if (model == null)
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(model);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
