using System.Collections.Generic;

namespace MvcExplorer.Models
{
    //Portfolio Model
    public static class PortfolioModel
    {

         //Declaration of Portfolio Model
        public static IDictionary<string, object[]> Settings
        {
            get
            {
                return
                new Dictionary<string, object[]>
                {
                    {"ChartType", new object[]{ "Line","Candlestick","HighLowOpenClose"}}
                };
            }
        }
        public static List<PortfolioStatic.Portfolio> PortfolioList
        {
            get
            {
                return PortfolioStatic.GetPortfolioList();
            }
        }
        public static List<PortfolioStatic.StockSymbol> SymbolList
        {
            get
            { return PortfolioStatic.GetSymbolList(); }
        }

      
    }
}