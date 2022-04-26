using C1.Web.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace TransposedGridExplorer.Models
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
                var name = Name ?? string.Empty;
                var text = string.Empty;
                foreach (var c in name)
                {
                    if (char.IsUpper(c) && text.Length != 0)
                    {
                        text += " ";
                    }

                    text += c;
                }

                return string.Format("{0} Theme", text);
            }
        }

        public static IEnumerable<Theme> GetAll()
        {
            return _all;
        }

        private static List<Theme> InitThemes()
        {
            var fields = typeof(Themes).GetTypeInfo().GetFields(BindingFlags.Static | BindingFlags.Public);
            var themes = new List<Theme>();
            foreach (var field in fields)
            {
                themes.Add(new Theme(field.Name, (string)field.GetValue(null)));
            }

            return themes;
        }

        public static int GetCurrentThemeIndex(HttpContext context)
        {
            var currentTheme = GetCurrentTheme(context);
            return Math.Max(0, _all.FindIndex(t => string.Equals(t.Name, currentTheme, StringComparison.OrdinalIgnoreCase)));
        }

        public static string GetCurrentTheme(HttpContext context)
        {
            return context.Session.GetString("C1Theme");
        }

        public static void SetCurrentTheme(HttpContext context, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                context.Session.SetString("C1Theme", value);
            }
        }
    }
}
