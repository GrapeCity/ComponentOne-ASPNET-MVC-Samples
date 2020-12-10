using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace FinancialChartExplorer.Models
{
    public static class ControlPages
    {
        private static List<ControlGroup> _controlGroups;
        private static IDictionary<string, ControlPage> _pages;
        private static readonly object _locker = new object();

        public static string MapPath
        {
            get
            {
                return Startup.Environment.ContentRootPath;
            }
        }

        private static void EnsurePages()
        {
            if (_controlGroups != null)
            {
                return;
            }

            lock (_locker)
            {
                if (_controlGroups != null)
                {
                    return;
                }

                using (var stream = GetPageConfigStream())
                {
                    var root = XElement.Load(stream);
                    _controlGroups = new List<ControlGroup>();
                    _pages = new Dictionary<string, ControlPage>(StringComparer.OrdinalIgnoreCase);
                    foreach (var controlgroupElement in root.Elements("ControlGroup"))
                    {
                        var controlGroup = new ControlGroup();
                        var name = controlgroupElement.Attribute("name");
                        if (name != null)
                        {
                            controlGroup.Name = name.Value;
                        }

                        foreach (var controlElement in controlgroupElement.Elements("Control"))
                        {
                            var control = new Control
                            {
                                Name = controlElement.Attribute("name").Value
                            };
                            foreach (var pageGroupElement in controlElement.Elements("PageGroup"))
                            {
                                var pageGroup = new PageGroup
                                {
                                    Name = pageGroupElement.Attribute("name").Value,
                                    JpText = pageGroupElement.Attribute("text.ja").Value
                                };
                                foreach (var pageElement in pageGroupElement.Elements("Action"))
                                {
                                    var page = new ControlPage
                                    {
                                        Name = pageElement.Attribute("name").Value,
                                        EnText = pageElement.Attribute("text").Value,
                                        JpText = pageElement.Attribute("text.ja").Value
                                    };
                                    pageGroup.Pages.Add(page);
                                    var key = CreatePageKey(controlGroup.Name, control.Name, page.Name);
                                    _pages.Add(key, page);
                                }
                                control.PageGroups.Add(pageGroup);
                            }
                            controlGroup.Controls.Add(control);
                        }
                        _controlGroups.Add(controlGroup);
                    }
                }
            }
        }

        private static string CreatePageKey(string groupName, string controlName, string pageName)
        {
            return string.Format("{0}-{1}-{2}", groupName, controlName, pageName);
        }

        private static Stream GetPageConfigStream()
        {
            return GetResourceStream("ControlPages.xml");
        }

        private static string GetPageText(string groupName, string controlName, string pageName)
        {
            EnsurePages();
            var key = CreatePageKey(groupName, controlName, pageName);
            return _pages.ContainsKey(key) ? _pages[key].Text : "Introduction";
        }

        private static IEnumerable<PageGroup> GetPageGroups(string groupName, string controlName)
        {
            return GetControl(groupName, controlName).PageGroups;
        }

        public static Control GetControl(string groupName, string controlName)
        {
            EnsurePages();
            var controlGroup = string.IsNullOrEmpty(groupName) ? _controlGroups[0] : _controlGroups.FirstOrDefault(cg => cg.Name.ToLower() == groupName.ToLower());
            var control = controlGroup.Controls.FirstOrDefault(c => c.Name.ToLower() == controlName.ToLower());
            return control;
        }

        public static IEnumerable<ControlPage> GetPages(string groupName, string controlName)
        {
            var pageGroups = GetPageGroups(groupName, controlName);
            var pages = new List<ControlPage>();
            foreach (var pageGroup in pageGroups)
            {
                pages.AddRange(pageGroup.Pages);
            }
            return pages;
        }

        public static IDictionary<string, string> GetPageSources(string controllerName, string actionName)
        {
            var pageSources = new Dictionary<string, string>();

            var controllerFileName = actionName + "Controller.cs";
            var controllerFilePath = string.Format("FinancialChartExplorer.Controllers.{0}.{1}", controllerName, controllerFileName);
            var controllerFileHtml = GetResourceContent(controllerFilePath);
            pageSources.Add(controllerFileName, controllerFileHtml);

            var viewFileName = actionName + ".cshtml";
            var viewFilePath = Path.Combine(MapPath,
                string.Format("Views/{0}/{1}", controllerName, viewFileName));
            var viewFileHtml = GetFileAsHtmlContent(viewFilePath);
            pageSources.Add(viewFileName, viewFileHtml);

            return pageSources;
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

        private static string GetFileAsHtmlContent(string controllerFilePath)
        {
            return File.ReadAllText(controllerFilePath);
        }

        private static Stream GetResourceStream(string name)
        {
            var assembly = typeof(ControlPages).GetTypeInfo().Assembly;
            var res = assembly.GetManifestResourceNames().Where(resName => resName.Contains(name)).ToList();
            if (res.Count == 0)
            {
                throw new ArgumentOutOfRangeException("name");
            }
            return assembly.GetManifestResourceStream(res[0]);
        }

        #region For FinancialChartExplorer

        public static string GetPageText(string pageName)
        {
            return GetPageText(null, "FinancialChart", pageName);
        }

        public static IEnumerable<PageGroup> GetPageGroups()
        {
            return GetPageGroups(null, "FinancialChart");
        }

        public static Control GetControl(string controlName)
        {
            return GetControl(null, controlName);
        }

        public static IEnumerable<ControlPage> GetPages(string controlName)
        {
            return GetPages(null, controlName);
        }
        #endregion For FinancialChartExplorer
    }

    public class ControlGroup
    {
        public IList<Control> Controls { get; set; }
        public string Name { get; set; }

        public ControlGroup()
        {
            Controls = new List<Control>();
        }
    }

    public class Control
    {
        private const string DocumentationRoot = "https://www.grapecity.com/componentone/docs/mvc/online-mvc-core/overview.html";
        private const string DocumentationRootJa = "http://docs.grapecity.com/help/c1/aspnet-mvc/aspmvc_helpers/";
        private string _documentation;

        public string Name { get; set; }
        public IList<PageGroup> PageGroups { get; set; }
        public string Documentation
        {
            get
            {
                if (string.IsNullOrEmpty(_documentation))
                {
                    return IsJpCulture() ? DocumentationRootJa : DocumentationRoot;
                }

                return _documentation;
            }
            set { _documentation = value; }
        }

        public Control()
        {
            PageGroups = new List<PageGroup>();
        }

        internal static bool IsJpCulture()
        {
#if NETCOREAPP1_0
            var culture = System.Globalization.CultureInfo.CurrentCulture;
#else
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
#endif
            return culture.TwoLetterISOLanguageName == "ja";
        }
    }

    public class PageGroup
    {
        public string Name { get; set; }
        public string JpText { get; set; }
        public string Text
        {
            get
            {
                return Control.IsJpCulture() ? JpText : Name;
            }
        }
        public IList<ControlPage> Pages { get; set; }

        public PageGroup()
        {
            Pages = new List<ControlPage>();
        }

        public int GetSelectedPageIndex(string actionName)
        {
            int sIndex = -1, index = 0;
            foreach (var page in Pages)
            {
                if (page.Name.ToLower() == actionName.ToLower())
                    return index;
                index++;
            }
            return sIndex;
        }
    }

    public class ControlPage
    {
        public string Name { get; set; }
        public string EnText { get; set; }
        public string JpText { get; set; }
        public string Text
        {
            get
            {
                return Control.IsJpCulture() ? JpText : EnText;
            }
        }
        public string Path { get; set; }
    }
}