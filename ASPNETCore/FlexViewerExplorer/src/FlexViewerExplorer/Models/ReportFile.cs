using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System;

namespace FlexViewerExplorer.Models
{
    public static class ReportFiles
    {
        private const string REPORTSROOT = "ReportsRoot";
        private const string SLASH = "/";

        private static List<DocumentItem> _reportFiles;
        private static DocumentItem _selectedItem;
        private static readonly object _locker = new object();

        private static void EnsureInit()
        {
            lock (_locker)
            {
                if (_reportFiles != null)
                {
                    return;
                }
                _reportFiles = new List<DocumentItem>();
                var reportInfos = XElement.Load(Startup.Environment.WebRootPath + SLASH + "ReportInfos.xml");
                var selectedReportCategoryName = reportInfos.Element("SelectedReport").Element("CategoryName").Value;
                var selectedReportName = reportInfos.Element("SelectedReport").Element("ReportName").Value;

                foreach (var category in reportInfos.Elements("Category"))
                {
                    var categoryName = category.Attribute("Name").Value;
                    var categoryText = category.Attribute("Text").Value;
                    var categoryTextJa = category.Attribute("Text.ja").Value;
                    var categoryImage = category.Attribute("Image").Value;

                    var folder = new DocumentItem() { Kind = DocumentItem.DocumentKindFolder, Name = categoryName, TitleEn = categoryText, TitleJp = categoryTextJa };

                    foreach (var report in category.Elements("Report"))
                    {
                        var reportName = report.Element("ReportName").Value;
                        var fileName = report.Element("FileName").Value;
                        var reportTitle = report.Element("ReportTitle").Value;
                        var reportTitleJa = report.Element("ReportTitle.ja") == null ? null : report.Element("ReportTitle.ja").Value;
                        var imageName = report.Element("ImageName").Value;

                        var file = new DocumentItem() { Kind = DocumentItem.DocumentKindFlexReport, Name = reportName, TitleEn = reportTitle, TitleJp = reportTitleJa, FullPath = REPORTSROOT + Documents.PATH_SLASH + categoryName + Documents.PATH_SLASH + fileName };
                        folder.Children.Add(file);

                        if (selectedReportCategoryName == categoryName && selectedReportName == reportName)
                        {
                            _selectedItem = file;
                        }
                    }
                    folder.Children.Sort(new Comparison<DocumentItem>((r1, r2) => r1.Title.CompareTo(r2.Title)));
                    _reportFiles.Add(folder);
                }
                _reportFiles.Sort(new Comparison<DocumentItem>((r1, r2) => r1.Title.CompareTo(r2.Title)));
            }
        }

        public static IEnumerable<DocumentItem> Reports
        {
            get
            {
                EnsureInit();
                return _reportFiles;
            }
        }

        public static string SelectedReportPath
        {
            get
            {
                EnsureInit();
                return _selectedItem == null ? string.Empty : _selectedItem.FullPath;
            }
        }

        public static string SelectedReportName
        {
            get
            {
                EnsureInit();
                return _selectedItem == null ? string.Empty : _selectedItem.Name;
            }
        }

        public static string SelectedReportTitle
        {
            get
            {
                EnsureInit();
                return _selectedItem == null ? string.Empty : _selectedItem.Title;
            }
        }
    }
}
