using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace C1Finance.Models
{
    //Portfolio Static Model
    public static class PortfolioStatic
    {
        //Declaration of Portfolio Static Model
        static Dictionary<string, string> prices = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        //Color set for Portfolios
        static string[] Company_Palette = new string[]{
            "#3366CC", "#DC3912", "#FF9900", "#109618", "#990099", "#3B3EAC", "#0099C6",
            "#DD4477", "#66AA00", "#B82E2E", "#316395", "#994499", "#22AA99", "#AAAA11",
            "#6633CC", "#E67300", "#8B0707", "#329262", "#5574A6", "#3B3EAC", "#000000",
            "#FFBE00", "#94D752", "#00B652", "#00B6EF", "#0075C6", "#002263", "#73359C",
            "#B53D9C", "#BD3D6B", "#AD65BD", "#DE6D33", "#FFB638", "#CE6DA5", "#FF8E38",
            "#525D6B", "#FF8633", "#739ADE", "#B52B15", "#F7CF2B", "#ADBAD6", "#737D84",
            "#424452", "#737DA5", "#9CBACE", "#D6DB7B", "#FFDB7B", "#BD8673", "#8C726B",
            "#424C22", "#A5B694", "#F7A642", "#E7BE2B", "#D692A5", "#9C86C6", "#849EC6",
            "#4A2215", "#3892A5", "#FFBA00", "#C62B2B", "#84AA33", "#944200", "#42598C",
            "#383838", "#6BA2B5", "#CEAE00", "#8C8AA5", "#738663", "#9C9273", "#7B868C",
            "#15487B", "#4A82BD", "#C6504A", "#9CBA5A", "#8465A5", "#4AAEC6", "#F79642",
            "#6B656B", "#CEBA63", "#9CB284", "#6BB2CE", "#6386CE", "#7B69CE", "#A578BD",
            "#332E33", "#F77D00", "#382733", "#15597B", "#4A8642", "#63487B", "#C69A5A",
            "#636984", "#D6604A", "#CEB600", "#28AEAD", "#8C7873", "#8CB28C", "#0E924A"};

        public static string CacheFolder = "";
        public static int CompanyCounter = 0;
        public static List<PortfolioFGridClass> PortfolioListFGrid { get; set; } //= new List<PortfolioFGridClass>();
        public static List<PortfolioAllClass> PortfolioListAll { get; set; }//= new List<PortfolioAllClass>();


        //Class for Portfolio to View (Grid & Chart)
        public partial class PortfolioFGridClass
        {
            public int Counter { get; set; }
            public string Color { get; set; }
            public string Name { get; set; }
            public string Symbol { get; set; }
            public bool Chart { get; set; }
            public double LastPrice { get; set; }
            public double Change { get; set; }
            public int Shares { get; set; }
            public double Price { get; set; }
            public double Cost { get; set; }
            public double Value { get; set; }
            public double Gain { get; set; }
            public List<SymbolPriceHistory> PriceHistory { get; set; }

            //Class Constructor
            public PortfolioFGridClass(int P_Counter, string P_Color, string P_Name, string P_Symbol, bool P_Chart = true, double P_LastPrice = 0, double P_Change = 0, int P_Shares = 0, double P_Price = 0, double P_Cost = 0, double P_Value = 0, double P_Gain = 0, List<SymbolPriceHistory> P_PriceHistory = null)
            {
                Counter = P_Counter;
                Color = P_Color;
                Name = P_Name;
                Symbol = P_Symbol;
                Chart = P_Chart;
                LastPrice = P_LastPrice;
                Change = P_Change;
                Shares = P_Shares;
                Price = P_Price;
                Cost = P_Cost;
                Value = P_Value;
                Gain = P_Gain;
                PriceHistory = P_PriceHistory;
            }

            public PortfolioFGridClass() : this(0, string.Empty, string.Empty, string.Empty)
            {
            }
        }

        //Class for Full List of Portfolio details
        public partial class PortfolioAllClass
        {
            public string symbol { get; set; }
            public string name { get; set; }
            public string symbolname { get; set; }

            //Class Constructor
            public PortfolioAllClass(string P_Symbol, string P_Name)
            {
                symbol = P_Symbol;
                name = P_Name;
                symbolname = P_Symbol + ": " + P_Name;
            }

            public PortfolioAllClass():this(string.Empty, string.Empty){}
        }

        //Class for Price History of Portfolio
        public partial class SymbolPriceHistory
        {
            public DateTime TimeSlab { get; set; }
            public string TimeSlabStr { get; set; }
            public double Price { get; set; }
            public double PriceGrowth { get; set; }

            //Class Constructor
            public SymbolPriceHistory(DateTime P_TimeSlab, string P_TimeSlabStr, double P_Price, double P_PriceGrowth)
            {
                TimeSlab = P_TimeSlab;
                TimeSlabStr = P_TimeSlabStr;
                Price = P_Price;
                PriceGrowth = P_PriceGrowth;
            }

            public SymbolPriceHistory() : this(new DateTime(), string.Empty, 0, 0)
            {
            }

        }

        //Function to get list of all Portfolios
        public static List<PortfolioAllClass> GetPortfolioAllList()
        {
            if (PortfolioListAll == null || PortfolioListAll.Count == 0)
            {
                PortfolioListAll = new List<PortfolioAllClass>();
                var path = HttpContext.Current.Server.MapPath("~/resources/symbolNames.txt");
                using (var sr = new StreamReader(path))
                {
                    for (var line = sr.ReadLine(); line != null; line = sr.ReadLine())
                    {
                        var parts = line.Split('\t');
                        if (parts.Length >= 2)
                        {
                            var key = parts[0].Trim();
                            var value = parts[1].Trim();
                            if (key.Length > 0 && value.Length > 0)
                            {
                                PortfolioListAll.Add(new PortfolioAllClass(key, value));
                            }
                        }
                    }
                }
            }
            return PortfolioListAll;
        }

        //Function to get Portfolio list for view (Grid & Chart)
        public static List<PortfolioFGridClass> GetPortfolioFGridList()
        {
            if (PortfolioListFGrid == null || PortfolioListFGrid.Count == 0)
            {
                PortfolioListFGrid = new List<PortfolioFGridClass>();
                int Shares = 0;
                double LastPrice = 0, LastPrice2 = 0, Change = 0, Price = 0, Cost = 0, Value = 0;
                double Gain = 0;
                if (CompanyCounter > 0)
                    CompanyCounter++;
                List<SymbolPriceHistory> PriceHistory = new List<SymbolPriceHistory>();
                Price = 5214;
                GetPrices("AMZN", Price, out LastPrice, out LastPrice2, out PriceHistory);
                if (LastPrice2 > 0)
                    Change = (LastPrice - LastPrice2) / LastPrice2;
                Shares = 10;
                Cost = Price * Shares;
                Value = LastPrice * Shares;
                if (Price > 0)
                    Gain = (LastPrice - Price) / Price;
                PortfolioListFGrid.Add(new PortfolioFGridClass(CompanyCounter, Company_Palette[CompanyCounter % Company_Palette.Length], PortfolioListAll.Where(x => x.symbol == "AMZN").Select(x => x.name).SingleOrDefault(), "AMZN", true, LastPrice, Change, Shares, Price, Cost, Value, Gain, PriceHistory));

                CompanyCounter++;
                PriceHistory = new List<SymbolPriceHistory>();
                Price = 240;
                GetPrices("AAPL", Price, out LastPrice, out LastPrice2, out PriceHistory);
                if (LastPrice2 > 0)
                    Change = (LastPrice - LastPrice2) / LastPrice2;
                Shares = 34;
                Cost = Price * Shares;
                Value = LastPrice * Shares;
                if (Price > 0)
                    Gain = (LastPrice - Price) / Price;
                PortfolioListFGrid.Add(new PortfolioFGridClass(CompanyCounter, Company_Palette[CompanyCounter % Company_Palette.Length], PortfolioListAll.Where(x => x.symbol == "AAPL").Select(x => x.name).SingleOrDefault(), "AAPL", true, LastPrice, Change, Shares, Price, Cost, Value, Gain, PriceHistory));

                CompanyCounter++;
                PriceHistory = new List<SymbolPriceHistory>();
                Price = 214;
                GetPrices("GOOG", Price, out LastPrice, out LastPrice2, out PriceHistory);
                if (LastPrice2 > 0)
                    Change = (LastPrice - LastPrice2) / LastPrice2;
                Shares = 10;
                Cost = Price * Shares;
                Value = LastPrice * Shares;
                if (Price > 0)
                    Gain = (LastPrice - Price) / Price;
                PortfolioListFGrid.Add(new PortfolioFGridClass(CompanyCounter, Company_Palette[CompanyCounter % Company_Palette.Length], PortfolioListAll.Where(x => x.symbol == "GOOG").Select(x => x.name).SingleOrDefault(), "GOOG", true, LastPrice, Change, Shares, Price, Cost, Value, Gain, PriceHistory));

                CompanyCounter++;
                PriceHistory = new List<SymbolPriceHistory>();
                Price = 1497;
                GetPrices("FBABX", Price, out LastPrice, out LastPrice2, out PriceHistory);
                if (LastPrice2 > 0)
                    Change = (LastPrice - LastPrice2) / LastPrice2;
                Shares = 41;
                Cost = Price * Shares;
                Value = LastPrice * Shares;
                if (Price > 0)
                    Gain = (LastPrice - Price) / Price;
                PortfolioListFGrid.Add(new PortfolioFGridClass(CompanyCounter, Company_Palette[CompanyCounter % Company_Palette.Length], PortfolioListAll.Where(x => x.symbol == "FBABX").Select(x => x.name).SingleOrDefault(), "FBABX", true, LastPrice, Change, Shares, Price, Cost, Value, Gain, PriceHistory));

                CompanyCounter++;
                PriceHistory = new List<SymbolPriceHistory>();
                Price = 3474;
                GetPrices("GM", Price, out LastPrice, out LastPrice2, out PriceHistory);
                if (LastPrice2 > 0)
                    Change = (LastPrice - LastPrice2) / LastPrice2;
                Shares = 56;
                Cost = Price * Shares;
                Value = LastPrice * Shares;
                if (Price > 0)
                    Gain = (LastPrice - Price) / Price;
                PortfolioListFGrid.Add(new PortfolioFGridClass(CompanyCounter, Company_Palette[CompanyCounter % Company_Palette.Length], PortfolioListAll.Where(x => x.symbol == "GM").Select(x => x.name).SingleOrDefault(), "GM", true, LastPrice, Change, Shares, Price, Cost, Value, Gain, PriceHistory));
            }
            return PortfolioListFGrid;
        }

        //Function to get prices of a symbol
        public static string GetPrices(string symbol, double purchaseprice, out double LastPrice, out double LastPrice2, out List<SymbolPriceHistory> PriceHistory)
        {
			ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; //TLS 1.2
            LastPrice = LastPrice2 = 0;
            PriceHistory = new List<SymbolPriceHistory>();
            string content = null;
            if (prices.TryGetValue(symbol, out content))
            {
                return content;
            }

            DateTime t = DateTime.Today;
            Stream dataStream = null;
            StreamWriter dataCacheStream = null;

            // check the file cache as well
            var filePath = Path.Combine(CacheFolder, symbol + ".dataCache");
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists && fileInfo.LastWriteTime.Date == t)
            {
                try { dataStream = fileInfo.OpenRead(); }
                catch { dataStream = null; }
            }

            // not in cache, get now
            if (dataStream == null)
            {
               
        var fmt = "https://gc-demo-dataservice.azurewebsites.net/alphavantage/api/v1/timeSeries?function=Daily&symbol={0}";
                

        var url = string.Format(fmt, symbol);

                try
                {
                    var wc = new WebClient();

                    IWebProxy defaultProxy = WebRequest.DefaultWebProxy;
                    if (defaultProxy != null)
                    {
                        defaultProxy.Credentials = CredentialCache.DefaultCredentials;
                        wc.Proxy = defaultProxy;
                    }

                    dataStream = wc.OpenRead(url);
                    dataCacheStream = fileInfo.CreateText();
                }
                catch { dataStream = null; }
            }

            // get content
            if (dataStream == null)
            {
                string msg = "Data for \"" + symbol + "\" not available.";
                if (fileInfo.Exists)
                {
                    try
                    {
                        dataStream = fileInfo.OpenRead();
                        msg = "New Data for \"" + symbol + "\" not available.\r\n" +
                            "Using cached data from " + fileInfo.LastWriteTime.Date.ToShortDateString();
                    }
                    catch
                    {
                        dataStream = null;
                    }
                }

                if (dataStream == null)
                {
                    //throw new Exception(msg);
                    return content;
                }
            }

            List<SymbolPriceHistory> TempList = new List<SymbolPriceHistory>();
            var sb = new StringBuilder();

            try
            {
                using (var sr = new StreamReader(dataStream))
                {
                    // skip headers
                    if (dataCacheStream != null)
                    {
                        dataCacheStream.WriteLine(sr.ReadLine());
                    }
                    else
                    {
                        sr.ReadLine();
                    }

                    int i = 0;
                    // read each line
                    for (var line = sr.ReadLine(); line != null; line = sr.ReadLine())
                    {
                        if (dataCacheStream != null) dataCacheStream.WriteLine(line);

                        // append date (field 0) and open price (field 1)
                        var items = line.Split(',');

                        double value;
                        var parseResult = double.TryParse(items[1], out value);

                        // quick fix
                        // culture dependent decimal part separator
                        if (!parseResult)
                            value = double.Parse(items[1].Replace('.', ','));

                        sb.AppendFormat("{0}\t{1:#.##}\r", items[0], value);
                        if (i == 0)
                        {
                            LastPrice = value;
                        }
                        if (i == 1)
                        {
                            LastPrice2 = value;
                        }
                        i++;
                        TempList.Add(new SymbolPriceHistory(DateTime.Parse(items[0].ToString()), DateTime.Parse(items[0].ToString()).Month.ToString() + "-" + DateTime.Parse(items[0].ToString()).Year.ToString(), value, 0));
                    }
                    double lastprice = LastPrice, currentprice = 0, pricegrowth = 0;
                    for (int tcount = TempList.Count - 1; tcount >= 0; tcount--)
                    {
                        currentprice = TempList[tcount].Price;
                        pricegrowth = lastprice > 0 ? (currentprice / lastprice) - 1 : -1;
                        PriceHistory.Add(new SymbolPriceHistory(TempList[tcount].TimeSlab, TempList[tcount].TimeSlabStr, TempList[tcount].Price, pricegrowth));
                    }
                }

                // save result in cache
                content = sb.ToString().Trim();
                prices[symbol] = content;

                // done
                Debug.WriteLine("returning {0} bytes", content.Length);
            }
            finally
            {
                if (dataCacheStream != null) dataCacheStream.Close();
            }

            return content;
        }

        //Function to add selected portfolio to list for view
        public static void AddToPortfolioFGridList(string SymbolToAdd)
        {
            if (SymbolToAdd != null && PortfolioListFGrid.Where(x => x.Symbol == SymbolToAdd).Count() <= 0)
            {
                int Shares = 0;
                double LastPrice = 0, LastPrice2 = 0, Change = 0, Price = 0, Cost = 0, Value = 0;
                double Gain = 0;
                CompanyCounter = PortfolioListFGrid.Count() > 0 ? PortfolioListFGrid.Max(x => x.Counter) + 1 : 0;
                List<SymbolPriceHistory> PriceHistory = new List<SymbolPriceHistory>();
                Price = 1000;
                GetPrices(SymbolToAdd, Price, out LastPrice, out LastPrice2, out PriceHistory);
                if (LastPrice2 > 0)
                    Change = (LastPrice - LastPrice2) / LastPrice2;
                Shares = 10;
                Cost = Price * Shares;
                Value = LastPrice * Shares;
                if (Price > 0)
                    Gain = (LastPrice - Price) / Price;
                PortfolioListFGrid.Add(new PortfolioFGridClass(CompanyCounter, Company_Palette[CompanyCounter % Company_Palette.Length], PortfolioListAll.Where(x => x.symbol == SymbolToAdd).Select(x => x.name).SingleOrDefault(), SymbolToAdd, true, LastPrice, Change, Shares, Price, Cost, Value, Gain, PriceHistory));
            }
        }
    }
}