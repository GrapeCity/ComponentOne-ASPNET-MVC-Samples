using System;
using System.Collections.Generic;
using System.Web;

namespace FlexViewerExplorer.Models
{
    public static class PageSources
    {
        public static IDictionary<string, string> GetPageSources(string controllerName, string actionName)
        {
            var pageSources = new Dictionary<string, string>();

            var controllerFileName = actionName + "Controller.cs";
            var controllerFilePath = HttpContext.Current.Server.MapPath(
                string.Format("~/Controllers/{0}", controllerFileName));
            var controllerFileHtml = GetFileAsHtmlContent(controllerFilePath);
            pageSources.Add(controllerFileName, controllerFileHtml);

            var viewFileName = actionName + ".cshtml";
            var viewFilePath = HttpContext.Current.Server.MapPath(
                string.Format("~/Views/{0}/{1}", controllerName, viewFileName));
            var viewFileHtml = GetFileAsHtmlContent(viewFilePath);
            pageSources.Add(viewFileName, viewFileHtml);

            const string startupFileName = "Startup.cs";
            var startUpFilePath = HttpContext.Current.Server.MapPath("~/" + startupFileName);
            var startUpFileHtml = GetFileAsHtmlContent(startUpFilePath);
            pageSources.Add(startupFileName, startUpFileHtml);

            return pageSources;
        }

        private static string GetFileAsHtmlContent(string controllerFilePath)
        {
            return System.IO.File.ReadAllText(controllerFilePath);
        }
    }
}