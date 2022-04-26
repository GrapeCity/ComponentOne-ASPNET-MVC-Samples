using System.Web.Mvc;
using C1.Web.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MultiRowExplorer.Models
{
    public class OptionItem
    {
        public List<string> Values { get; set; }
        public string CurrentValue { get; set; }
    }

    public class ControlOptions
    {
        public static string ToOptionName(string name)
        {
            return name == null ? null : name.Replace(" ", "").ToLower();
        }

        public OptionDictionary Options = new OptionDictionary();

        public IEnumerable<MenuItem> OptionItemToMenuItem(string key)
        {
            return Options[key].Values.Select(option => new MenuItem { Header = option }).ToList();
        }

        public void LoadPostData(IValueProvider data)
        {
            foreach (var option in Options)
            {
                var optionName = ToOptionName(option.Key);
                if (!data.ContainsPrefix(optionName)) continue;
                var value = data.GetValue(optionName);
                option.Value.CurrentValue = (string)value.ConvertTo(typeof(string));
            }
        }
    }

    public class OptionDictionary : Dictionary<string, OptionItem>
    {
        private static readonly IEqualityComparer<string> DefaultOptionNameComparer = new OptionNameComparer();

        public OptionDictionary()
            : base(DefaultOptionNameComparer)
        {
        }

        private class OptionNameComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return ControlOptions.ToOptionName(x) == ControlOptions.ToOptionName(y);
            }

            public int GetHashCode(string obj)
            {
                return obj == null ? 0 : ControlOptions.ToOptionName(obj).GetHashCode();
            }
        }
    }
}