using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ImageCombo.Models
{
    public static class ControlPages
    {
        public static IDictionary<string, string> GetPageSources()
        {
            var pageSources = new Dictionary<string, string>();

            var controlFiles = new string[] { "ImageCombo.cs", "HtmlHelper.cs" };

            foreach (var controlFile in controlFiles)
            {
                var control = HttpContext.Current.Server.MapPath(
                string.Format("~/Controls/{0}", controlFile));
                pageSources.Add(controlFile, GetFileAsHtmlContent(control));
            }

            var controllerFileName = "HomeController.cs";
            var controllerFilePath = HttpContext.Current.Server.MapPath(
                string.Format("~/Controllers/{0}", controllerFileName));
            pageSources.Add(controllerFileName, GetFileAsHtmlContent(controllerFilePath));

            var viewFileName = "Index.cshtml";
            var viewFilePath = HttpContext.Current.Server.MapPath(
                string.Format("~/Views/Home/{0}", viewFileName));
            pageSources.Add(viewFileName, GetFileAsHtmlContent(viewFilePath));

            return pageSources;
        }

        private static string GetFileAsHtmlContent(string controllerFilePath)
        {
            return System.IO.File.ReadAllText(controllerFilePath);
        }
    }
}