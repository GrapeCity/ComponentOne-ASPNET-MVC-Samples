using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MultiRowLOB.Models
{
    public static class ControlPages
    {
        private static List<ControlPageGroup> _pageGroups;
        private static List<ControlPage> _pages;
        private static List<ControlGroup> _controlGroups;
        private static IDictionary<string, ControlPage> _pageDic;
        private const string PagesFileInServer = "~/Content/ControlPages.xml";
        private static readonly object _locker = new object();

        public static IDictionary<string, string> GetPageSources(string controllerName, string actionName)
        {
            var pageSources = new Dictionary<string, string>();
            var page = GetControlPage(controllerName, actionName);

            var controllerFileName = actionName + "Controller.cs";
            var controllerFilePath = HttpContext.Current.Server.MapPath(
                string.Format("~/Controllers/{0}/{1}", controllerName, controllerFileName));
            var controllerFileHtml = GetFileAsHtmlContent(controllerFilePath);
            pageSources.Add(controllerFileName, controllerFileHtml);

            var viewFileName = actionName + ".cshtml";
            var viewFilePath = HttpContext.Current.Server.MapPath(
                string.Format("~/Views/{0}/{1}", controllerName, viewFileName));
            var viewFileHtml = GetFileAsHtmlContent(viewFilePath);
            pageSources.Add(viewFileName, viewFileHtml);

            if (controllerName == "Validation" || actionName == "UnobtrusiveValidation")
            {
                var modelFileName = "UserInfo.cs";
                var modelFilePath = HttpContext.Current.Server.MapPath(
                    string.Format("~/Models/{0}", modelFileName));
                var modelFileHtml = GetFileAsHtmlContent(modelFilePath);
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

                var root = XElement.Load(HttpContext.Current.Server.MapPath(PagesFileInServer));
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

        private static List<ControlPage> GetControlPagesFromXEle(XElement controlElement, string controlName)
        {
            var pages = controlElement.Elements("action").Select(e =>
            {
                ControlPage page = new ControlPage
                {
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
        private const string DocumentationRoot = "https://www.grapecity.com/componentone/docs/mvc/online-mvc/overview.html";
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
        public string Text
        {
            get
            {
                return Localization.Resources.ResourceManager.GetString("Action_" + Name, Localization.Resources.Culture);
            }
        }
        public string Path { get; set; }
        public string ControlName { get; set; }
        public List<ControlPage> SubPages { get; set; }
    }
}