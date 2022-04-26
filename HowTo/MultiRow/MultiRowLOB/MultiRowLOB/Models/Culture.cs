using C1.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MultiRowLOB.Models
{
    public class C1Culture
    {
        private readonly static List<C1Culture> _all = InitCultures();

        public C1Culture(string name, string text)
        {
            Name = name;
            Text = text;
        }

        public string Name { get; private set; }

        public string Text { get; private set; }

        public string Path
        {
            get
            {
                return string.Format("?culture={0}", Name ?? string.Empty);
            }
        }

        public string DisplayText
        {
            get
            {
                return string.IsNullOrEmpty(Text) ? Name :Text;
            }
        }

        public static IEnumerable<C1Culture> GetAll()
        {
            return _all;
        }

        private static List<C1Culture> InitCultures()
        {
            var cultures = new List<C1Culture>();
            cultures.Add(new C1Culture("en", "English"));
            cultures.Add(new C1Culture("ja", "日文"));
            return cultures;
        }

        public static int GetCurrentCultureIndex()
        {
            var currentCulture = CurrentCulture;
            return Math.Max(0, _all.FindIndex(c => string.Equals(c.Name, currentCulture, StringComparison.OrdinalIgnoreCase)));
        }

        public static string CurrentCulture
        {
            get
            {
                return (string)HttpContext.Current.Session["C1Culture"];
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    HttpContext.Current.Session["C1Culture"] = value;
                }
            }
        }
    }
}