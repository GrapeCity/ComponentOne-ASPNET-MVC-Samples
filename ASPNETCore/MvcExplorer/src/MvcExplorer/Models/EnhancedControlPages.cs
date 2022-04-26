using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace MvcExplorer.Models
{
    public static class EnhancedControlPages
    {
        private static List<ControlGroup> _enhancedGroups;
        private static IDictionary<string, List<ControlPage>> _enhancedPagesTreeDic;
        private static readonly object _locker = new object();

        public static IEnumerable<ControlGroup> EnhancedControlGroups
        {
            get
            {
                filterEnhancedGroups();
                return _enhancedGroups;
            }
        }

        private static void filterEnhancedGroups()
        {
            if (_enhancedGroups != null)
            {
                return;
            }

            lock (_locker)
            {
                if (_enhancedGroups != null)
                {
                    return;
                }

                _enhancedGroups = new List<ControlGroup>();
                _enhancedPagesTreeDic = new Dictionary<string, List<ControlPage>>(StringComparer.OrdinalIgnoreCase);

                foreach (var group in ControlPages.ControlGroups)
                {
                    var enhancedPageGroups = new List<ControlPageGroup>();

                    foreach (var pageGroup in group.Controls)
                    {
                        var enhancedPages = new List<ControlPage>();
                        filterEnhancedPages(pageGroup.Pages, enhancedPages);
                        if (pageGroup.IsEnhanced || enhancedPages.Count > 0)
                        {
                            enhancedPageGroups.Add(new ControlPageGroup
                            {
                                ControlNameEn = pageGroup.ControlNameEn,
                                ControlNameJp = pageGroup.ControlNameJp,
                                ControlGroupName = pageGroup.ControlGroupName,
                                DocumentationEn = pageGroup.DocumentationEn,
                                DocumentationJp = pageGroup.DocumentationJp,
                                Pages = enhancedPages,
                                LinkEn = pageGroup.LinkEn,
                                LinkJp = pageGroup.LinkJp,
                                IsPopular = pageGroup.IsPopular,
                                IsNew = pageGroup.IsNew,
                                TextEn = pageGroup.TextEn,
                                TextJp = pageGroup.TextJp,
                                IsEnhanced = pageGroup.IsEnhanced,
                                EnhanceTipEn = pageGroup.EnhanceTipEn,
                                EnhanceTipJp = pageGroup.EnhanceTipJp
                            });
                        }
                    }

                    if (enhancedPageGroups.Count > 0)
                    {
                        _enhancedGroups.Add(new ControlGroup
                        {
                            GroupNameEn = group.GroupNameEn,
                            GroupNameJp = group.GroupNameJp,
                            Visible = group.Visible,
                            Controls = enhancedPageGroups
                        });
                    }
                }
            }
        }

        private static void filterEnhancedPages(IEnumerable<ControlPage> pages, List<ControlPage> toPages)
        {
            foreach (var page in pages)
            {
                var toSubPages = new List<ControlPage>();
                if (page.SubPages != null)
                {
                    filterEnhancedPages(page.SubPages, toSubPages);
                }

                if (page.IsEnhanced || toSubPages.Count > 0)
                {
                    toPages.Add(new ControlPage
                    {
                        TextEn = page.TextEn,
                        TextJp = page.TextJp,
                        Name = page.Name,
                        ControlName = page.ControlName,
                        IsEnhanced = page.IsEnhanced,
                        EnhanceTipEn = page.EnhanceTipEn,
                        EnhanceTipJp = page.EnhanceTipJp,
                        SubPages = toSubPages
                    });
                }
            }
        }

        public static List<ControlPage> getEnhancedPagesTree(ControlGroup controlGroup)
        {
            var key = controlGroup.GroupName;
            if (_enhancedPagesTreeDic.ContainsKey(key))
            {
                return _enhancedPagesTreeDic[key];
            }

            var pagesTree = new List<ControlPage>();
            foreach (var controlPageGroup in controlGroup.Controls)
            {
                pagesTree.Add(new ControlPage
                {
                    TextEn = controlPageGroup.TextEn,
                    TextJp = controlPageGroup.TextJp,
                    ControlName = controlPageGroup.ControlNameEn,
                    Name = "Index",
                    IsEnhanced = controlPageGroup.IsEnhanced,
                    EnhanceTipEn = controlPageGroup.EnhanceTipEn,
                    EnhanceTipJp = controlPageGroup.EnhanceTipJp,
                    Path = string.IsNullOrEmpty(controlPageGroup.Link) ? null : "#" + controlPageGroup.Link,
                    SubPages = controlPageGroup.Pages.ToList()
                });
            }
            _enhancedPagesTreeDic.Add(controlGroup.GroupName, pagesTree);
            return pagesTree;
        }

        public static void setPagePath(IUrlHelper urlHelper, ControlPage page)
        {
            if (page.SubPages != null && page.SubPages.Count > 0)
            {
                foreach (var subPage in page.SubPages)
                {
                    setPagePath(urlHelper, subPage);
                }
            }
            else if (String.IsNullOrEmpty(page.Path))
            {
                page.Path = string.IsNullOrEmpty(page.Name) ? null : urlHelper.Action(page.Name, page.ControlName).ToString();
            }
        }

        public static void setPagesPath(ActionContext ctx, IEnumerable<ControlPage> pages)
        {
            var urlHelper = new UrlHelperFactory().GetUrlHelper(ctx);
            foreach (var page in pages)
            {
                setPagePath(urlHelper, page);
            }
        }

        private static bool hasEnhancements()
        {
            IEnumerable<ControlGroup> groups = ControlPages.ControlGroups;
            foreach (var group in groups)
            {
                foreach (var pageGroup in group.Controls)
                {
                    if (pageGroup.IsEnhanced) return true;
                    else if (hasEnhancements(pageGroup.Pages)) return true;
                }
            }
            return false;
        }

        private static bool hasEnhancements(IEnumerable<ControlPage> pages)
        {
            foreach (var page in pages)
            {
                if (page.IsEnhanced)
                {
                    return true;
                }
                else if (page.SubPages != null && hasEnhancements(page.SubPages))
                {
                    return true;
                }
            }
            return false;
        }
    }
}