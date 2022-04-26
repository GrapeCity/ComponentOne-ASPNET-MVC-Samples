using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace MvcExplorer.Models
{
    public class Culture
    {
        private readonly static List<Culture> _all = InitCultures();

        public Culture(string name, string key)
        {
            Name = name;
            Key = key;
        }

        public string Name { get; set; }
        public string Key { get; set; }

        public string Path
        {
            get
            {
                return string.Format("?culture={0}", Key ?? string.Empty);
            }
        }

        public string DisplayText
        {
            get
            {
                return Name;
            }
        }

        public static IEnumerable<Culture> GetAll()
        {
            return _all;
        }

        private static List<Culture> InitCultures()
        {
            var list = new List<Culture>();
            list.Add(new Culture(Resources.Resource.CultureEnglish, "en"));
            list.Add(new Culture(Resources.Resource.CultureSpanish, "es"));
            list.Add(new Culture(Resources.Resource.CultureItalian, "it"));
            list.Add(new Culture(Resources.Resource.CultureFrench, "fr"));
            list.Add(new Culture(Resources.Resource.CultureGerman, "de"));
            list.Add(new Culture(Resources.Resource.CultureDutch, "nl"));
            list.Add(new Culture(Resources.Resource.CultureJapanese, "ja"));
            list.Add(new Culture(Resources.Resource.CultureKorean, "ko"));
            list.Add(new Culture(Resources.Resource.CultureChinese, "zh-HK"));
            return list;
        }

        public static string CurrentCulture
        {
            get
            {
                return (string)HttpContext.Current.Session["C1Culture"];
            }
            set
            {
                HttpContext.Current.Session["C1Culture"] = string.IsNullOrEmpty(value) ? GetDefaultCulture() : value;
            }
        }

        private static string GetDefaultCulture()
        {
            return Thread.CurrentThread.CurrentCulture.Name.StartsWith("ja", StringComparison.InvariantCultureIgnoreCase)? "ja" : "en";
        }

        public static int GetCurrentCultureIndex()
        {
            var currentCulture = CurrentCulture;
            return Math.Max(0, _all.FindIndex(t => string.Equals(t.Key, currentCulture, StringComparison.OrdinalIgnoreCase)));
        }
    }
}