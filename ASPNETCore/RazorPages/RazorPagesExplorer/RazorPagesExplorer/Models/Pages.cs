using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RazorPagesExplorer.Models
{
    public static class ControlPages
    {
        private static string _prjName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;

        public static List<Page> Pages = new List<Page>() {
            new Page{ Name="Overview", Text="Overview" },
            new Page{ Name="FlexGrid", Text="FlexGrid" },
            new Page{ Name="BatchEditing", Text="Batch Editing" },
            new Page{ Name="UnobtrusiveValidation", Text="Unobtrusive Validation" },
            new Page{ Name="FlexChart", Text="FlexChart" },
            new Page{ Name="Input", Text="Input" }
        };

        public static int GetSelectedPageIndex(string pageName)
        {
            int sIndex = 0, index = 0;
            foreach (var page in Pages)
            {
                if (page.Name.ToLower() == pageName.TrimStart('/').ToLower())
                    return index;
                index++;
            }
            return sIndex;
        }

        public static string GetPageText(string pageName)
        {
            foreach (var page in Pages)
            {
                if (page.Name.ToLower() == pageName.TrimStart('/').ToLower())
                    return page.Text;
            }
            return string.Empty;
        }

        public static IDictionary<string, string> GetPageSources(string pageName)
        {
            var pageSources = new Dictionary<string, string>();
            pageName = pageName.TrimStart('/');

            var pageHtmlFileName = pageName + ".cshtml";
            var pageHtmlFilePath = _prjName + ".Pages." + pageHtmlFileName;
            var pageHtmlFileContent= GetResourceContent(pageHtmlFilePath);
            pageSources.Add(pageHtmlFileName, pageHtmlFileContent);

            var pageCsFileName = pageName + ".cshtml.cs";
            var pageCsFilePath = _prjName + ".Pages." + pageCsFileName;
            var pageCsFileContent = GetResourceContent(pageCsFilePath);
            pageSources.Add(pageCsFileName, pageCsFileContent);

            if (pageName == "UnobtrusiveValidation")
            {
                var modelFileName = "UserInfo.cs";
                var modelFilePath = string.Format(_prjName + ".Models.{0}", modelFileName);
                var modelFileHtml = GetResourceContent(modelFilePath);
                pageSources.Add(modelFileName, modelFileHtml);

                var viewFileName = "UnobtrusiveValidationPartial.cshtml";
                var viewFilePath = string.Format(_prjName + ".Pages.{0}", viewFileName);
                var viewFileHtml = GetResourceContent(viewFilePath);
                pageSources.Add(viewFileName, viewFileHtml);
            }

            return pageSources;
        }

        private static Stream GetResourceStream(string name)
        {
            var assembly = typeof(ControlPages).GetTypeInfo().Assembly;
            var res = assembly.GetManifestResourceNames().Where(resName => resName == name).ToList();
            if (res.Count == 0)
            {
                throw new ArgumentOutOfRangeException("name");
            }
            return assembly.GetManifestResourceStream(res[0]);
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
    }

    public class Page
    {
        private const string DocumentationRoot = "https://developer.mescius.com/componentone/docs/mvc/online-mvc-core/overview.html";
        private string _documentation;

        public string Name { get; set; }
        public string Text { get; set; }
        public string Path { get; set; }

        public string Documentation
        {
            get
            {
                if (string.IsNullOrEmpty(_documentation))
                {
                    return DocumentationRoot;
                }

                return _documentation;
            }
            set { _documentation = value; }
        }
    }
}