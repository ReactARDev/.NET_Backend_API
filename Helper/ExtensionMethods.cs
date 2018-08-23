using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Net.Http;
using System.IO;

namespace AMPS9000_WebAPI.Helper
{
    public static class ExtensionMethods
    {
        public static string GetDictionaryValue(this IDictionary<string, string> keyValuePairs, string key, string defaultValue = "")
        {
            return keyValuePairs.ContainsKey(key) ? keyValuePairs[key] : defaultValue;
        }

        public static void UpdateFileProperties(this Object obj, PropertyInfo[] properties, HttpRequestMessage request, IDictionary<string, string> listofFiles)
        {
            object uploadUrl;
            request.Properties.TryGetValue("uploadUrl", out uploadUrl);
            if (uploadUrl != null)
            {
                foreach (var prop in properties)
                {
                    var val = prop.GetValue(obj) ?? string.Empty;
                    prop.SetValue(obj, Path.GetFileName(listofFiles.GetDictionaryValue(prop.Name, val.ToString())));
                }
            }
        }

        public static void RemoveUrl(this Object obj, PropertyInfo[] properties, HttpRequestMessage request)
        {
            object uploadUrl;
            request.Properties.TryGetValue("uploadUrl", out uploadUrl);
            if (uploadUrl != null)
            {
                foreach (var prop in properties)
                {
                    var val = prop.GetValue(obj);
                    if (val != null)
                        prop.SetValue(obj, val.ToString().Replace(uploadUrl.ToString(), string.Empty));
                }
            }
        }

        public static void AddUrl(this Object obj, PropertyInfo[] properties, HttpRequestMessage request)
        {
            object uploadUrl;
            request.Properties.TryGetValue("uploadUrl", out uploadUrl);
            if (uploadUrl != null)
            {
                foreach (var prop in properties)
                {
                    var val = prop.GetValue(obj);
                    if (val != null && !string.IsNullOrWhiteSpace(val.ToString()))
                        prop.SetValue(obj, string.Concat(uploadUrl, val.ToString()));
                }
            }
        }

        public static void DeleteFiles(this Object obj, PropertyInfo[] properties, string path)
        {
            string uploadPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Uploadfiles"), path);

            if (!string.IsNullOrWhiteSpace(path))
            {
                foreach (var prop in properties)
                {
                    var val = prop.GetValue(obj);
                    if (val != null && !string.IsNullOrWhiteSpace(val.ToString()))
                    {
                        try
                        {
                            File.Delete(Path.Combine(uploadPath, val.ToString()));
                        }
                        catch {
                            // TODO: Need to log exception but can't throw as need to continue deletion for other files
                        }
                    }
                }
            }
        }


    }
}