using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace StockChart.Models
{
    public partial class StockData
    {
        public DateTime TimeSlab { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }

        public double HighChg { get; set; }
        public double LowChg { get; set; }
        public double OpenChg { get; set; }
        public double CloseChg { get; set; }

        public StockData(DateTime dt, double h, double l, double o, double c, double v)
        {
            TimeSlab = dt;
            High = h;
            Low = l;
            Open = o;
            Close = c;
            Volume = v;
        }

        public StockData()
            : this(new DateTime(), 0, 0, 0, 0, 0)
        { }

        public StockData(string dataInfos)
        {
            var items = dataInfos.Split(',');
            TimeSlab = DateTime.Parse(items[0]);
            Open = ParseValue(items[1]);
            High = ParseValue(items[2]);
            Low = ParseValue(items[3]);
            Close = ParseValue(items[4]);
            Volume = ParseValue(items[5]);
        }

        internal static double ParseValue(string value)
        {
            double result;
            if (string.IsNullOrEmpty(value))
                return 0;
            var parseResult = double.TryParse(value, out result);

            // quick fix
            // culture dependent decimal part separator
            if (!parseResult)
                result = double.Parse(value.Replace('.', ','));
            return Math.Round(result, 2);
        }
    }

    public partial class StockEvent
    {
        public DateTime Date { get; set; }
        public string Title { get; set; }
    }

    //Class for Full List of Stock symbol details
    public partial class StockSymbol
    {
        public string symbol { get; set; }
        public string name { get; set; }
        public string symbolname { get; set; }

        //Class Constructor
        public StockSymbol(string P_Symbol, string P_Name)
        {
            symbol = P_Symbol;
            name = P_Name;
            symbolname = "<b>" + P_Symbol + "</b>: " + P_Name;
        }

        public StockSymbol() : this(string.Empty, string.Empty) { }
    }


    //StockData Static Model
    public static class StockDataStatic
    {
        public static string[] Company_Palette = new string[]{
            "#79aad1", "#79aad1", "#DC3912", "#109618", "#990099", "#3B3EAC", "#0099C6",
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
        private static Dictionary<string, StockSymbol> _stockSymbolListAll;
        public static Dictionary<string, StockSymbol> StockSymbolListAll
        {
            get
            {
                if (_stockSymbolListAll == null || _stockSymbolListAll.Count == 0)
                {
                    _stockSymbolListAll = new Dictionary<string, StockSymbol>();
                    var path = HttpContext.Current.Server.MapPath("~/Content/Resources/symbolNames.txt");
                    using (var sr = new StreamReader(path))
                    {
                        int i = 0;
                        for (var line = sr.ReadLine(); line != null; line = sr.ReadLine())
                        {
                            var parts = line.Split('\t');
                            if (parts.Length >= 2)
                            {
                                var key = parts[0].Trim();
                                var value = parts[1].Trim();
                                if (key.Length > 0 && value.Length > 0)
                                {
                                    _stockSymbolListAll.Add(key, new StockSymbol(key, value));
                                    i++;
                                }
                            }
                        }
                    }
                }
                return _stockSymbolListAll;
            }
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

            public SymbolPriceHistory()
                : this(new DateTime(), string.Empty, 0, 0)
            {
            }

        }

        private static Dictionary<string, List<StockData>> stockDataDic = new Dictionary<string, List<StockData>>(StringComparer.OrdinalIgnoreCase);

        public static List<StockData> GetStockPricesBySymbol(string symbol)
        {
            List<StockData> stockDataList;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; //TLS 1.2
            if (stockDataDic.TryGetValue(symbol, out stockDataList))
            {
                return stockDataList;
            }

            stockDataList = new List<StockData>();
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
        var fmt = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={0}&apikey=EQ8R2LTG732VP7HE&datatype=csv&outputsize=full";

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
                    return stockDataList;
                }
            }

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

                    // read each line
                    for (var line = sr.ReadLine(); line != null; line = sr.ReadLine())
                    {
                        if (dataCacheStream != null) dataCacheStream.WriteLine(line);

                        if (symbol == "^IXIC")
                        {
                            var items = line.Split(',');
                            var timeSlab = DateTime.Parse(items[0]);
                            var high = StockData.ParseValue(items[2]);
                            var low = StockData.ParseValue(items[3]);
                            var open = StockData.ParseValue(items[1]);
                            var close = open;
                            var volume = StockData.ParseValue(items[5]);
                            stockDataList.Add(new StockData(timeSlab, high, low, open, close, volume));
                        }
                        else
                        {
                            stockDataList.Add(new StockData(line));
                        }
                    }
                    int length = stockDataList.Count;
                    double firstValue = stockDataList[length - 1].Close;
                    for (int i = length - 1; i >= 0; i--)
                    {
                        stockDataList[i].HighChg = (stockDataList[i].High - firstValue) / firstValue;
                        stockDataList[i].LowChg = (stockDataList[i].Low - firstValue) / firstValue;
                        stockDataList[i].OpenChg = (stockDataList[i].Open - firstValue) / firstValue;
                        stockDataList[i].CloseChg = (stockDataList[i].Close - firstValue) / firstValue;
                    }
                }

                stockDataDic[symbol] = stockDataList;
            }
            finally
            {
                if (dataCacheStream != null) dataCacheStream.Close();
            }

            return stockDataList;
        }

        private static Dictionary<string, List<StockEvent>> stockEventDic = new Dictionary<string, List<StockEvent>>(StringComparer.OrdinalIgnoreCase);
        public static List<StockEvent> GetStockEventsBySymbol(string symbol)
        {
            List<StockEvent> events;

            if (stockEventDic.TryGetValue(symbol, out events))
            {
                return events;
            }
            events = new List<StockEvent>();
            try
            {
                var fmt = "http://articlefeeds.nasdaq.com/nasdaq/symbols?symbol={0}";
                var url = string.Format(fmt, symbol);
                var wc = new WebClient();

                IWebProxy defaultProxy = WebRequest.DefaultWebProxy;
                if (defaultProxy != null)
                {
                    defaultProxy.Credentials = CredentialCache.DefaultCredentials;
                    wc.Proxy = defaultProxy;
                }

                using (var sr = new StreamReader(wc.OpenRead(url)))
                {
                    XDocument doc = XDocument.Parse(sr.ReadToEnd().Trim());
                    foreach (XElement c in doc.Descendants("item"))
                    {
                        DateTime dt = DateTime.Parse((string)c.Element("pubDate"));
                        StockEvent ev = new StockEvent
                        {
                            Date = new DateTime(dt.Year, dt.Month, dt.Day),
                            Title = (string)c.Element("title")
                        };
                        events.Add(ev);
                    }
                }
                stockEventDic[symbol] = events;
            }
            catch
            {

            }
            return events;
        }
    }
}