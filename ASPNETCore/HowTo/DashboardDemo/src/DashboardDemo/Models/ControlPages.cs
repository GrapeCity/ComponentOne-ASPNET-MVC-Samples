using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace DashboardDemo
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
            var controllerFilePath = string.Format("DashboardDemo.Controllers.{0}.{1}", controllerName, controllerFileName);
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
                var modelFilePath = string.Format("DashboardDemo.Models.{0}", modelFileName);
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
            return group.Pages.FirstOrDefault(p => string.Equals(p.Name, actionName, StringComparison.OrdinalIgnoreCase));
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
                            GroupName = controlElement.Attribute("name").Value,
                            Visible = visible,
                            Controls = pageGroups
                        };

                        foreach (var pageElement in controlElement.Elements("Control"))
                        {
                            var currentControl = pageElement.Attribute("name").Value;
                            var docElement = pageElement.Attribute("documentation");
                            var documentation = docElement == null ? null : docElement.Value;
                            var linkAttr = pageElement.Attribute("link");

                            var pages = GetControlPagesFromXEle(pageElement, currentControl);
                            _pages.AddRange(pages);
                            var controlPageGroup = new ControlPageGroup
                            {
                                ControlName = currentControl,
                                ControlGroupName = controlGroup.GroupName,
                                Documentation = documentation,
                                Pages = pages,
                                Link = linkAttr != null ? linkAttr.Value : null
                            };

                            pageGroups.Add(controlPageGroup);
                        }
                        _pageGroups.AddRange(pageGroups);
                        _controlGroups.Add(controlGroup);
                    }

                    _pageDic = new Dictionary<string, ControlPage>(StringComparer.OrdinalIgnoreCase);
                    foreach (var page in _pages)
                    {
                        var key = CreatePageKey(page.ControlName, page.Name);
                        if (!_pageDic.ContainsKey(key))
                        {
                            _pageDic.Add(key, page);
                        }
                    }
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
                    Text = Resource.ResourceManager.GetString(e.Attribute("text").Value),
                    Icon = string.Format("~/Content/images/24X24_{0}.png", e.Attribute("icon").Value),
                    Name = e.Attribute("name").Value,
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
        public string GroupName { get; set; }
        public bool Visible { get; set; }
    }

    public class ControlPageGroup
    {
        private const string DocumentationRoot = "https://www.grapecity.com/componentone/docs/mvc/online-mvc-core/overview.html";
        private string _documentation;

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

        public IEnumerable<ControlPage> Pages { get; set; }
        public string ControlName { get; set; }
        public string ControlGroupName { get; set; }
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

        public string Link { get; set; }
    }

    public class ControlPage
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Path { get; set; }
        public string ControlName { get; set; }
        public string Icon { get; set; }
        public List<ControlPage> SubPages { get; set; }
    }
}