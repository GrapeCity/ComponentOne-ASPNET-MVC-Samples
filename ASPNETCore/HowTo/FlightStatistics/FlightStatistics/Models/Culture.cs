using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FlightStatistics.Models
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
            var list = new List<Culture>
            {
                new Culture(Localization.Resource.CultureEnglish, "en"),
                new Culture(Localization.Resource.CultureSpanish, "es"),
                new Culture(Localization.Resource.CultureItalian, "it"),
                new Culture(Localization.Resource.CultureFrench, "fr"),
                new Culture(Localization.Resource.CultureGerman, "de"),
                new Culture(Localization.Resource.CultureDutch, "nl"),
                new Culture(Localization.Resource.CultureJapanese, "ja"),
                new Culture(Localization.Resource.CultureKorean, "ko"),
                new Culture(Localization.Resource.CultureChinese, "zh-HK")
            };
            return list;
        }

        public static string GetCurrentCulture(HttpContext context)
        {
            return context.Session.GetString("C1Culture");
        }

        public static void SetCurrentCulture(HttpContext context, string value)
        {
            context.Session.SetString("C1Culture", string.IsNullOrEmpty(value) ? GetDefaultCulture(context) : value);
        }

        private static string GetDefaultCulture(HttpContext context)
        {
            return CultureInfo.CurrentCulture.Name.StartsWith("ja", StringComparison.OrdinalIgnoreCase) ? "ja" : "en";
        }

        public static int GetCurrentCultureIndex(HttpContext context)
        {
            var currentCulture = GetCurrentCulture(context);
            return Math.Max(0, _all.FindIndex(t => string.Equals(t.Key, currentCulture, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
