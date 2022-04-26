using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiRowLOB.Models
{
    public static class CultureHelper
    {
        private static readonly IList<string> _cultures;

        static CultureHelper()
        {
            _cultures = C1Culture.GetAll().Select(c => c.Name).ToList();
        }

        public static string GetImplementedCulture(string name)
        {
            if (string.IsNullOrEmpty(name))
                return GetDefaultCulture();

            if (_cultures.Where(c => c.Equals(name, StringComparison.InvariantCultureIgnoreCase)).Count() > 0)
                return name;

            var n = GetNeutralCulture(name);
            foreach (var c in _cultures)
                if (c.StartsWith(n))
                    return c;
            return GetDefaultCulture(); 
        }

        public static string GetDefaultCulture()
        {
            return _cultures[0];
        }

        public static string GetNeutralCulture(string name)
        {
            if (!name.Contains("-")) return name;

            return name.Split('-')[0];
        }
    }
}