﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MvcExplorer.Models
{
    public static class ControlPages
    {
        private static List<ControlPageGroup> _pageGroups;
        private static List<ControlPage> _pages;
        private static List<ControlGroup> _controlGroups;
        private static IDictionary<string, ControlPage> _pageDic;
        private const string PagesFileInServer = "~/Content/ControlPages.xml";
        private static readonly object _locker = new object();
        private static ControlGroup _popularGroup;
        private static ControlGroup _newGroup;

        public static IDictionary<string, string> GetPageSources(string controllerName, string actionName)
        {
            var pageSources = new Dictionary<string, string>();

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

            if(controllerName == "DashboardLayout" && actionName != "CustomTile")
            {
                var dlViewFileName = "_DashboardElements.cshtml";
                var dlViewFilePath = HttpContext.Current.Server.MapPath(
                    string.Format("~/Views/Shared/{0}", dlViewFileName));
                var dlViewFileHtml = GetFileAsHtmlContent(dlViewFilePath);
                pageSources.Add(dlViewFileName, dlViewFileHtml);
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
            if(group == null)
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

                var root = XElement.Load(HttpContext.Current.Server.MapPath(PagesFileInServer));
                _controlGroups = new List<ControlGroup>();
                _pages = new List<ControlPage>();
                _pageGroups = new List<ControlPageGroup>();
                _newGroup = new ControlGroup { GroupNameEn = Resources.Resource.NewControls };
                _popularGroup = new ControlGroup { GroupNameEn = Resources.Resource.PopularControls };
                foreach (var controlElement in root.Elements("ControlGroup"))
                {
                    var pageGroups = new List<ControlPageGroup>();
                    var visibleAttr=controlElement.Attribute("visible");
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
                        var newAttr = pageElement.Attribute("new");
                        var isNew = false;
                        if (newAttr != null)
                        {
                            bool.TryParse(newAttr.Value, out isNew);
                        }

                        var popularAttr = pageElement.Attribute("popular");
                        var popular = false;
                        if (popularAttr != null)
                        {
                            bool.TryParse(popularAttr.Value, out popular);
                        }

                        var enhancedAttr = pageElement.Attribute("enhanced");
                        var isEnhanced = false;
                        if (enhancedAttr != null)
                        {
                            bool.TryParse(enhancedAttr.Value, out isEnhanced);
                        }
                        var textAttr = pageElement.Attribute("text");
                        var textJaAttr = pageElement.Attribute("text.ja");
                        var enhanceTip = pageElement.Attribute("enhancetip")?.Value;
                        var enhanceTipJa = pageElement.Attribute("enhancetip.ja")?.Value;

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
                            IsPopular = popular,
                            IsNew = isNew,
                            IsEnhanced = isEnhanced,
                            TextEn = textAttr == null ? currentControl : textAttr.Value,
                            TextJp = textJaAttr == null ? currentControlJa : textJaAttr.Value,
                            EnhanceTipEn = enhanceTip,
                            EnhanceTipJp = enhanceTipJa
                        };

                        if (isNew)
                        {
                            _newGroup.Controls.Add(controlPageGroup);
                        }

                        if (popular)
                        {
                            _popularGroup.Controls.Add(controlPageGroup);
                        }

                        pageGroups.Add(controlPageGroup);
                    }
                    _pageGroups.AddRange(pageGroups);
                    _controlGroups.Add(controlGroup);
                }

                _pageDic = new Dictionary<string, ControlPage>(StringComparer.OrdinalIgnoreCase);
                EnsurePageDic(_pages);
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

        private static List<ControlPage> GetControlPagesFromXEle(XElement controlElement, string controlName)
        {
            var pages = controlElement.Elements("action").Select(e =>
            {
                var enhancedAttr = e.Attribute("enhanced");
                var isEnhanced = false;
                if (enhancedAttr != null) {
                    bool.TryParse(enhancedAttr.Value, out isEnhanced);
                }
                ControlPage page = new ControlPage
                {
                    TextEn = e.Attribute("text").Value,
                    TextJp = e.Attribute("text.ja")?.Value,
                    Name = e.Attribute("name") != null ? e.Attribute("name").Value : "",
                    ControlName = controlName,
                    IsEnhanced = isEnhanced,
                    EnhanceTipEn = e.Attribute("enhancetip")?.Value,
                    EnhanceTipJp = e.Attribute("enhancetip.ja")?.Value
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
                
        public static ControlGroup NewGroup 
        {
            get
            {
                EnsurePages();
                return _newGroup;
            }
        }

        public static ControlGroup PopularGroup
        {
            get
            {
                EnsurePages();
                return _popularGroup;
            }
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
                var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
                return culture.TwoLetterISOLanguageName == "ja";
            }
        }
    }

    public class ControlPageGroup
    {
        private const string DocumentationRootEn = "https://developer.mescius.com/componentone/docs/mvc/online-mvc/overview.html";
        private const string DocumentationRootJp = "http://docs.mescius.jp/help/c1/aspnet-mvc/aspmvc_helpers/";

        internal string DocumentationEn { get; set; }
        internal string DocumentationJp { get; set; }

        public string Documentation
        {
            get
            {
                if (ControlGroup.IsJpCulture)
                {
                    return DocumentationJp ?? DocumentationRootJp;
                }
                else
                {
                    return DocumentationEn ?? DocumentationRootEn;
                }
            }
        }

        public IEnumerable<ControlPage> Pages { get; set; }
        public string ControlNameEn { get; set; }
        internal string ControlNameJp { get; set; }
        public string ControlName
        {
            get
            {                
                return ControlGroup.IsJpCulture ? ControlNameJp ?? ControlNameEn : ControlNameEn;
            }
        }
        public string ControlGroupName { get; set; }
        internal string TextEn { get; set; }
        internal string TextJp { get; set; }
        public string Text
        {
            get
            {
                return ControlGroup.IsJpCulture ? TextJp ?? TextEn : TextEn;
            }
        }
        public ControlPage GetPage(string actionName)
        {
            return GetPage(Pages, actionName);
        }

        private ControlPage GetPage(IEnumerable<ControlPage> pages, string name)
        {
            foreach(var page in pages)
            {
                if(string.Equals(page.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return page;
                }

                if (page.SubPages != null)
                {
                    var subPage =  GetPage(page.SubPages, name);
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

        public bool IsNew { get; set; }

        public bool IsPopular { get; set; }

        public bool IsEnhanced { get; set; }
        internal string EnhanceTipEn { get; set; }
        internal string EnhanceTipJp { get; set; }
        public string EnhanceTip
        {
            get
            {
                return ControlGroup.IsJpCulture ? EnhanceTipJp ?? EnhanceTipEn : EnhanceTipEn;
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
        public bool IsEnhanced { get; set; }
        internal string EnhanceTipEn { get; set; }
        internal string EnhanceTipJp { get; set; }
        public string EnhanceTip
        {
            get
            {
                return ControlGroup.IsJpCulture ? EnhanceTipJp ?? EnhanceTipEn : EnhanceTipEn;
            }
        }
        public List<ControlPage> SubPages { get; set; }
    }
}