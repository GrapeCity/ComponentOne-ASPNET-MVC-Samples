using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;

namespace FlexViewerExplorer.Models
{
    public static class PageSources
    {
        public static IDictionary<string, string> GetPageSources(string controllerName, string actionName)
        {
            var pageSources = new Dictionary<string, string>();
            var mapPath = Startup.Environment.ContentRootPath;
            var controllerFileName = actionName + "Controller.cs";
            var controllerFilePath = string.Format("Controllers.{0}", controllerFileName);
            var controllerFileHtml = GetResourceContent(controllerFilePath);
            pageSources.Add(controllerFileName, controllerFileHtml);

            var viewFileName = actionName + ".cshtml";
            var viewFilePath = Path.Combine(mapPath,
                string.Format("Views/{0}/{1}", controllerName, viewFileName));
            var viewFileHtml = GetFileAsHtmlContent(viewFilePath);
            pageSources.Add(viewFileName, viewFileHtml);

            const string startupFileName = "Startup.cs";
            var startUpFileHtml = GetResourceContent(startupFileName);
            pageSources.Add(startupFileName, startUpFileHtml);

            return pageSources;
        }

        public static string toCamelCase(this string content)
        {
            return content.Substring(0, 1).ToLower() + content.Substring(1);
        }

        private static string GetFileAsHtmlContent(string controllerFilePath)
        {
            return File.Exists(controllerFilePath) ?
                File.ReadAllText(controllerFilePath) : null;
        }

        private static string GetResourceContent(string name)
        {
            using (var stream = GetResourceStream(name))
            {
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static Stream GetResourceStream(string name)
        {
            var assembly = typeof(PageSources).GetTypeInfo().Assembly;
            var ress = assembly.GetManifestResourceNames();
            var res = assembly.GetManifestResourceNames().Where(resName => resName.Contains(name)).ToList();
            if (res.Count == 0)
            {
                throw new ArgumentOutOfRangeException("name");
            }
            return assembly.GetManifestResourceStream(res[0]);
        }
    }
}