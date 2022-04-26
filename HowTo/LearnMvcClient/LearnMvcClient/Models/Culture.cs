using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

namespace LearnMvcClient.Models
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
            list.Add(new Culture(Resources.Resource.English, "en"));
            list.Add(new Culture(Resources.Resource.Spanish, "es"));
            list.Add(new Culture(Resources.Resource.Italian, "it"));
            list.Add(new Culture(Resources.Resource.French, "fr"));
            list.Add(new Culture(Resources.Resource.German, "de"));
            list.Add(new Culture(Resources.Resource.Dutch, "nl"));
            list.Add(new Culture(Resources.Resource.Japanese, "ja"));
            list.Add(new Culture(Resources.Resource.Korean, "ko"));
            list.Add(new Culture(Resources.Resource.Chinese, "zh-HK"));
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
                if (string.IsNullOrEmpty(value))
                {
                    value = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ja" ? "ja" : "en";
                }
                HttpContext.Current.Session["C1Culture"] = value;
            }
        }

        public static int GetCurrentCultureIndex()
        {
            var currentCulture = CurrentCulture;
            return Math.Max(0, _all.FindIndex(t => string.Equals(t.Key, currentCulture, StringComparison.OrdinalIgnoreCase)));
        }
    }
}