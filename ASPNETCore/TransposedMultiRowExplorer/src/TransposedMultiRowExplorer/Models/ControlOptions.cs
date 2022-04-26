using Microsoft.AspNetCore.Http;
using C1.Web.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace TransposedMultiRowExplorer.Models
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

        public void LoadPostData(IFormCollection data, bool camelCase = true)
        {
            foreach (var option in Options)
            {
                var optionName = camelCase ? ToOptionName(option.Key) : option.Key;
                if (!data.ContainsKey(optionName)) continue;
                var value = data[optionName];
                option.Value.CurrentValue = value;
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