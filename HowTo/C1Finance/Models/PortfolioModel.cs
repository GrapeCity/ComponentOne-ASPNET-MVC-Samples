using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C1.Web.Mvc.Serialization;
using C1.Web.Mvc;
using System.IO;
using C1Finance.Models;

namespace C1Finance.Models
{
    //Portfolio Model
    public class PortfolioModel
    {
        //Declaration of Portfolio Model
        public IDictionary<string, object[]> Settings { get; set; }
        public List<PortfolioStatic.PortfolioFGridClass> PortfolioListFGrid { get; set; }
        public List<PortfolioStatic.PortfolioAllClass> PortfolioListAll { get; set; }

        //Constructor for Initialization of object of Portfolio Class
        public PortfolioModel()
        {
            Settings = new Dictionary<string, object[]>
            {
                {"ChartType", new object[]{ "Line","Candlestick","HighLowOpenClose"}}
            };
            PortfolioListAll = PortfolioStatic.GetPortfolioAllList();
            PortfolioListFGrid = PortfolioStatic.GetPortfolioFGridList();
        }
    }
}