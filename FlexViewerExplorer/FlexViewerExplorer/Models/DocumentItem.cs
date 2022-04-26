using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace FlexViewerExplorer.Models
{
    public class DocumentItem
    {
        private const string SLASH = "/";

        internal const string DocumentKindFolder = "Folder";
        internal const string DocumentKindPdf = "Pdf";
        internal const string DocumentKindFlexReport = "FlexReport";
        internal const string DocumentKindSsrsReport = "SsrsReport";
        internal const string DocumentKindArReport = "ArReport";
        private List<DocumentItem> _children;
        private string _fullPath;

        public string Kind { get; set; }
        internal string TitleEn { get; set; }
        internal string TitleJp { get; set; }
        public string Title
        {
            get
            {
                return IsJpCulture ? TitleJp ?? TitleEn : TitleEn;
            }
        }
        public string Name { get; set; }
        public string FullPath
        {
            get
            {
                return string.IsNullOrEmpty(_fullPath) ? Name : _fullPath;
            }
            set
            {
                _fullPath = value;
            }
        }
        public List<DocumentItem> Children
        {
            get
            {
                return _children ?? (_children = new List<DocumentItem>());
            }
            set { _children = value; }
        }

        internal static bool IsJpCulture
        {
            get
            {
                var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
                return culture.TwoLetterISOLanguageName == "ja";
            }
        }
    }
}