using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexSheet101.Models
{
    public class FlexSheetModel
    {
        public IEnumerable<Sale> CountryData { get; set; }

        //Add for editing source
        public static List<Sale> Source = Sale.GetData(500).ToList<Sale>();

        public FlexSheetModel()
        {

        }
    }
}