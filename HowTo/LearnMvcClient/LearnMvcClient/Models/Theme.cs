using C1.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace LearnMvcClient.Models
{
    public class Theme
    {
        private readonly static List<Theme> _all = InitThemes();
        private readonly string _value;

        public Theme(string name, string value)
        {
            Name = name;
            _value = value;
        }

        public string Name { get; private set; }

        public string Path
        {
            get
            {
                return string.Format("?theme={0}", _value ?? string.Empty);
            }
        }

        public string DisplayText
        {
            get
            {
                return Name;
            }
        }

        public static IEnumerable<Theme> GetAll()
        {
            return _all;
        }

        private static List<Theme> InitThemes()
        {
            var fields = typeof(Themes).GetFields(BindingFlags.Static | BindingFlags.Public);
            var themes = new List<Theme>();
            foreach (var field in fields)
            {
                themes.Add(new Theme(field.Name, (string)field.GetValueDirect(default(TypedReference))));
            }

            return themes;
        }

        public static int GetCurrentThemeIndex()
        {
            var currentTheme = CurrentTheme;
            return Math.Max(0, _all.FindIndex(t => string.Equals(t._value, currentTheme, StringComparison.OrdinalIgnoreCase)));
        }

        public static string CurrentTheme
        {
            get
            {
                return (string)HttpContext.Current.Session["C1Theme"];
            }
            set
            {
                HttpContext.Current.Session["C1Theme"] = string.IsNullOrEmpty(value) ? "default" : value;
            }
        }
    }
}