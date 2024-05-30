using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace TransposedGridExplorer.Models
{
    public static class ControlPages
    {
        private static List<ControlPageGroup> _pageGroups;
        private static List<ControlPage> _pages;
        private static List<ControlGroup> _controlGroups;
        private static IDictionary<string, ControlPage> _pageDic;
        private static readonly object _locker = new object();

        public static string MapPath
        {
            get
            {
                return Startup.Environment.ContentRootPath;
            }
        }

        public static IDictionary<string, string> GetPageSources(string controllerName, string actionName)
        {
            var pageSources = new Dictionary<string, string>();

            var controllerFileName = actionName + "Controller.cs";
            var controllerFilePath = string.Format("TransposedGridExplorer.Controllers.{0}.{1}", controllerName, controllerFileName);
            var controllerFileHtml = GetResourceContent(controllerFilePath);
            pageSources.Add(controllerFileName, controllerFileHtml);

            var viewFileName = actionName + ".cshtml";
            var viewFilePath = Path.Combine(MapPath,
                string.Format("Views/{0}/{1}", controllerName, viewFileName));
            var viewFileHtml = GetFileAsHtmlContent(viewFilePath);
            pageSources.Add(viewFileName, viewFileHtml);

            if (controllerName == "Validation" || actionName == "UnobtrusiveValidation")
            {
                var modelFileName = "UserInfo.cs";
                var modelFilePath = string.Format("TransposedGridExplorer.Models.{0}", modelFileName);
                var modelFileHtml = GetResourceContent(modelFilePath);
                pageSources.Add(modelFileName, modelFileHtml);
            }

            return pageSources;
        }

        public static ControlPageGroup GetControlPageGroup(string controlName)
        {
            EnsurePages();
            return _pageGroups.FirstOrDefault(pageGroup => pageGroup.ControlName.ToLower() == controlName.ToLower());
        }

        public static ControlPage GetControlPage(string controlName, string actionName)
        {
            var group = GetControlPageGroup(controlName);
            if (group == null)
            {
                return null;
            }

            actionName = string.IsNullOrEmpty(actionName) ? "Index" : actionName;
            return group.GetPage(actionName);
        }

        private static string GetFileAsHtmlContent(string controllerFilePath)
        {
            return System.IO.File.ReadAllText(controllerFilePath);
        }

        public static string GetPageText(string controllerName, string actionName)
        {
            EnsurePages();
            var key = CreatePageKey(controllerName, actionName);
            return _pageDic.ContainsKey(key) ? _pageDic[key].Text : "Overview";
        }

        private static string CreatePageKey(string controllerName, string actionName)
        {
            return string.Format("{0}-{1}", controllerName, actionName);
        }

        public static IEnumerable<ControlPage> Pages
        {
            get
            {
                EnsurePages();
                return _pages;
            }
        }

        public static ControlGroup GetGroup(string name)
        {
            return ControlGroups.FirstOrDefault(g => string.Equals(g.GroupName, name, StringComparison.OrdinalIgnoreCase));
        }

        public static IEnumerable<ControlGroup> ControlGroups
        {
            get
            {
                EnsurePages();
                return _controlGroups;
            }
        }

        public static IEnumerable<ControlGroup> VisibleControlGroups
        {
            get
            {
                return ControlGroups.Where(g => g.Visible);
            }
        }

        private static void EnsurePages()
        {
            if (_pages != null)
            {
                return;
            }

            lock (_locker)
            {
                if (_pages != null)
                {
                    return;
                }

                using (var stream = GetPageConfigStream())
                {
                    var root = XElement.Load(stream);
                    _controlGroups = new List<ControlGroup>();
                    _pages = new List<ControlPage>();
                    _pageGroups = new List<ControlPageGroup>();
                    foreach (var controlElement in root.Elements("ControlGroup"))
                    {
                        var pageGroups = new List<ControlPageGroup>();
                        var visibleAttr = controlElement.Attribute("visible");
                        var visible = visibleAttr == null || visibleAttr.Value != "false";
                        ControlGroup controlGroup = new ControlGroup
                        {
                            GroupNameEn = controlElement.Attribute("name").Value,
                            GroupNameJp = controlElement.Attribute("name.ja")?.Value,
                            Visible = visible,
                            Controls = pageGroups
                        };

                        foreach (var pageElement in controlElement.Elements("Control"))
                        {
                            var currentControl = pageElement.Attribute("name").Value;
                            var currentControlJa = pageElement.Attribute("name.ja")?.Value;
                            var docElement = pageElement.Attribute("documentation");
                            var documentation = docElement == null ? null : docElement.Value;
                            var documentationJa = pageElement.Attribute("documentation.ja")?.Value;
                            var linkAttr = pageElement.Attribute("link");
                            var linkJa = pageElement.Attribute("link.ja")?.Value;

                            var pages = GetControlPagesFromXEle(pageElement, currentControl);
                            _pages.AddRange(pages);
                            var controlPageGroup = new ControlPageGroup
                            {
                                ControlNameEn = currentControl,
                                ControlNameJp = currentControlJa,
                                ControlGroupName = controlGroup.GroupName,
                                DocumentationEn = documentation,
                                DocumentationJp = documentationJa,
                                Pages = pages,
                                LinkEn = linkAttr != null ? linkAttr.Value : null,
                                LinkJp = linkJa,

                            };

                            pageGroups.Add(controlPageGroup);
                        }
                        _pageGroups.AddRange(pageGroups);
                        _controlGroups.Add(controlGroup);
                    }

                    _pageDic = new Dictionary<string, ControlPage>(StringComparer.OrdinalIgnoreCase);
                    EnsurePageDic(_pages);
                }
            }
        }

        private static void EnsurePageDic(IEnumerable<ControlPage> pages)
        {
            foreach (var page in pages)
            {
                var key = CreatePageKey(page.ControlName, page.Name);
                if (!_pageDic.ContainsKey(key))
                {
                    _pageDic.Add(key, page);
                }

                if (page.SubPages != null)
                {
                    EnsurePageDic(page.SubPages);
                }
            }
        }

        private static Stream GetPageConfigStream()
        {
            return GetResourceStream("ControlPages.xml");
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

        private static List<ControlPage> GetControlPagesFromXEle(XElement controlElement, string controlName)
        {
            var pages = controlElement.Elements("action").Select(e =>
            {
                ControlPage page = new ControlPage
                {
                    TextEn = e.Attribute("text").Value,
                    TextJp = e.Attribute("text.ja")?.Value,
                    Name = e.Attribute("name") != null ? e.Attribute("name").Value : "",
                    ControlName = controlName
                };
                if (e.Element("subactions") != null)
                {
                    List<ControlPage> subPages = GetControlPagesFromXEle(e.Element("subactions"), controlName);
                    if (subPages.Count() > 0)
                    {
                        page.SubPages = subPages;
                    }
                }

                return page;
            }).ToList();
            return pages;
        }

    }

    public class ControlGroup
    {
        public ControlGroup()
        {
            Visible = true;
            Controls = new List<ControlPageGroup>();
        }

        public IList<ControlPageGroup> Controls { get; set; }
        internal string GroupNameEn { get; set; }
        internal string GroupNameJp { get; set; }
        public string GroupName
        {
            get
            {
                return IsJpCulture ? GroupNameJp ?? GroupNameEn : GroupNameEn;
            }
        }
        public bool Visible { get; set; }

        internal static bool IsJpCulture
        {
            get
            {
#if NETCOREAPP1_0
                var culture = System.Globalization.CultureInfo.CurrentCulture;
#else
                var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
#endif
                return culture.TwoLetterISOLanguageName == "ja";
            }
        }
    }

    public class ControlPageGroup
    {
        private const string DocumentationRootEn = "https://developer.mescius.com/componentone/docs/mvc/online-mvc-core/overview.html";
        private const string DocumentationRootJa = "https://docs.mescius.jp/help/c1/aspnet-mvc/aspmvc_helpers/#overview.html";

        internal string DocumentationEn { get; set; }
        internal string DocumentationJp { get; set; }
        public string Documentation
        {
            get
            {
                if (ControlGroup.IsJpCulture)
                {
                    return DocumentationJp ?? DocumentationRootJa;
                }
                else
                {
                    return DocumentationEn ?? DocumentationRootEn;
                }
            }
        }

        public int GetSelectedPageIndex(string actionName)
        {
            int sIndex = 0, index = 0;
            foreach (var page in Pages)
            {
                if (page.Name.ToLower() == actionName.ToLower())
                    return index;
                index++;
            }
            return sIndex;
        }

        public IEnumerable<ControlPage> Pages { get; set; }
        internal string ControlNameEn { get; set; }
        internal string ControlNameJp { get; set; }
        public string ControlName
        {
            get
            {
                return ControlGroup.IsJpCulture ? ControlNameJp ?? ControlNameEn : ControlNameEn;
            }
        }
        public string ControlGroupName { get; set; }
        public ControlPage GetPage(string actionName)
        {
            return GetPage(Pages, actionName);
        }

        private ControlPage GetPage(IEnumerable<ControlPage> pages, string name)
        {
            foreach (var page in pages)
            {
                if (string.Equals(page.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return page;
                }

                if (page.SubPages != null)
                {
                    var subPage = GetPage(page.SubPages, name);
                    if (subPage != null)
                    {
                        return subPage;
                    }
                }
            }

            return null;
        }

        internal string LinkEn { get; set; }
        internal string LinkJp { get; set; }
        public string Link
        {
            get
            {
                return ControlGroup.IsJpCulture ? LinkJp ?? LinkEn : LinkEn;
            }
        }
    }

    public class ControlPage
    {
        public string Name { get; set; }
        internal string TextEn { get; set; }
        internal string TextJp { get; set; }
        public string Text
        {
            get
            {
                return ControlGroup.IsJpCulture ? TextJp ?? TextEn : TextEn;
            }
        }
        public string Path { get; set; }
        public string ControlName { get; set; }
        public List<ControlPage> SubPages { get; set; }
    }
}