using System;
using System.Collections.Generic;
using System.Linq;

namespace GridInForm.Models
{
    public class Company
    {
        public string Name { get; set; }
        public string WebSite { get; set; }
        public string Headquarters { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Industry { get; set; }
        public DateTime Founded { get; set; }
        public List<Sale> Sales { get; set; }
    }
}