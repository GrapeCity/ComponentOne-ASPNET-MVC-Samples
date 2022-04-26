using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelImportExport.Models
{
    //FlexGrid Model Class Declaration
    public class FlexGridModel
    {
        public IEnumerable<Sale> CountryData { get; set; }
        public string[] GroupBy { get; set; }

        //Class Constructor
        public FlexGridModel()
        {

        }
    }
}