using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace FlexViewerExplorer.Models
{
    public class Documents
    {
        public const string PATH_SLASH = "/";

        private static Documents _ssrsReports = new Documents("SsrsReportInfos.xml", "c1ssrs", DocumentItem.DocumentKindSsrsReport);
        private static Documents _pdfs = new Documents("PdfInfos.xml", "PdfsRoot", DocumentItem.DocumentKindPdf);
        private static Documents _arReports = new Documents("ArReportInfos.xml", "", DocumentItem.DocumentKindArReport);
        private static List<DocumentItem> _allItems;

        private readonly string _xmlFilePath;
        private readonly string _rootKey;
        private readonly string _itemKind;
        private List<DocumentItem> _items;
        private DocumentItem _selectedItem;

        private readonly object _locker = new object();

        public Documents(string xmlFilePath, string rootKey, string itemKind)
        {
            _xmlFilePath = xmlFilePath;
            _rootKey = rootKey;
            _itemKind = itemKind;
        }

        public static Documents ArReports
        {
            get { return _arReports; }
        }

        public static Documents SsrsReports
        {
            get { return _ssrsReports; }
        }

        public static Documents Pdfs
        {
            get { return _pdfs; }
        }

        public static IEnumerable<DocumentItem> AllItems
        {
            get
            {
                if (_allItems == null)
                {
                    var pdfs = new DocumentItem { Kind = DocumentItem.DocumentKindFolder, Name = "PdfFiles", TitleEn = "PDF Files" };
                    pdfs.Children.AddRange(_pdfs.Items);
                    var ssrsReports = new DocumentItem { Kind = DocumentItem.DocumentKindFolder, Name = "SsrsReports", TitleEn = "SSRS Reports" };
                    ssrsReports.Children.AddRange(_ssrsReports.Items);
                    var arReports = new DocumentItem { Kind = DocumentItem.DocumentKindFolder, Name = "ArReports", TitleEn = "ActiveReports" };
                    arReports.Children.AddRange(_arReports.Items);
                    var flexReports = new DocumentItem { Kind = DocumentItem.DocumentKindFolder, Name = "FlexReports", TitleEn = "FlexReports" };
                    flexReports.Children.AddRange(ReportFiles.Reports);

                    _allItems = new List<DocumentItem> { flexReports, arReports, ssrsReports, pdfs };
                }

                return _allItems;
            }
        }

        public IEnumerable<DocumentItem> Items
        {
            get
            {
                EnsureInit();
                return _items;
            }
        }

        public string SelectedDocumentPath
        {
            get
            {
                EnsureInit();
                return _selectedItem.FullPath;
            }
        }

        public string SelectedDocumentName
        {
            get
            {
                EnsureInit();
                return _selectedItem.Name;
            }
        }

        public string SelectedDocumentTitle
        {
            get
            {
                EnsureInit();
                return _selectedItem.Title;
            }
        }

        private void EnsureInit()
        {
            if (_items == null)
            {
                lock (_locker)
                {
                    if (_items == null)
                    {
                        InitItems();
                    }
                }
            }
        }

        private void InitItems()
        {
            var items = new List<DocumentItem>();
            var docInfos = XElement.Load(Startup.Environment.WebRootPath + PATH_SLASH + _xmlFilePath);
            var selectedCategoryName = docInfos.Element("SelectedDocument").Element("CategoryName").Value;
            var selectedDocumentName = docInfos.Element("SelectedDocument").Element("DocumentName").Value;

            foreach (var category in docInfos.Elements("Category"))
            {
                var categoryName = category.Attribute("Name").Value;
                var categoryTitle = category.Attribute("Title").Value;
                var categoryTitleJa = category.Attribute("Title.ja").Value;

                var folder = new DocumentItem() { Name = categoryName, TitleEn = categoryTitle, TitleJp = categoryTitleJa, Kind = DocumentItem.DocumentKindFolder };
                items.Add(folder);

                foreach (var doc in category.Elements("Document"))
                {
                    var docName = doc.Element("DocumentName").Value;
                    var docPath = doc.Element("DocumentPath").Value;
                    var docTitle = doc.Element("DocumentTitle").Value;
                    var docTitleJa = doc.Element("DocumentTitle.ja") == null ? null : doc.Element("DocumentTitle.ja").Value;
                    var fullPath = string.IsNullOrEmpty(_rootKey) ? docPath : _rootKey + PATH_SLASH + docPath;

                    var item = new DocumentItem() { Kind = _itemKind, Name = docName, TitleEn = docTitle, TitleJp = docTitleJa, FullPath = fullPath };
                    folder.Children.Add(item);

                    if (selectedCategoryName == categoryName && selectedDocumentName == docName)
                    {
                        _selectedItem = item;
                    }
                }
            }

            _items = items.Count == 1 ? items[0].Children : items;
        }
    }

}