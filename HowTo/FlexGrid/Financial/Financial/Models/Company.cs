using System;
using System.Collections.Generic;
using System.Linq;

namespace Financial.Models
{
    public class Company
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public int Bid { get; set; }
        public int Ask { get; set; }
        public int LastSale { get; set; }
        public int BidSize { get; set; }
        public int AskSize { get; set; }
        public int LastSize { get; set; }
        public int Volume { get; set; }
        public DateTime QuoteTime { get; set; }
        public DateTime TradeTime { get; set; }
        public int[] AskHistory { get; set; }
        public int[] BidHistory { get; set; }
        public int[] SaleHistory { get; set; }

        public static IEnumerable<Company> GetData()
        {
            var rand = new Random(0);
            return CompanyInfo.GetData().Select(ci =>
            {
                var bid = rand.Next(1, 10000) / 100;
                var ask = bid + rand.Next(0, 100) / 100;

                return new Company
                {
                    Symbol = ci.Symbol,
                    Name = ci.Name,
                    Bid = bid,
                    Ask = ask,
                    LastSale = bid,
                    BidSize = rand.Next(10,500),
                    AskSize = rand.Next(10, 500),
                    LastSize = rand.Next(10, 500),
                    Volume = rand.Next(10000, 20000),
                    QuoteTime = DateTime.Now,
                    TradeTime = DateTime.Now,
                    AskHistory = new int[2] {ask, ask},
                    BidHistory = new int[2] {bid, bid},
                    SaleHistory = new int[2] {bid, bid}
                };
            });
        }
    }

    public class CompanyInfo
    {
        public string Symbol { get; set; }
        public string Name { get; set; }

        public static IEnumerable<CompanyInfo> GetData()
        {
            return new List<CompanyInfo>
            {
                new CompanyInfo{ Symbol = "RDSA", Name = "Royal Dutch Shell"},
                new CompanyInfo{ Symbol = "ULVR", Name = "Unilever"},
                new CompanyInfo{ Symbol = "HSBA", Name = "HSBC"},
                new CompanyInfo{ Symbol = "BATS", Name = "British American Tobacco"},
                new CompanyInfo{ Symbol = "GSK", Name = "GlaxoSmithKline"},
                new CompanyInfo{ Symbol = "SAB", Name = "SABMiller"},
                new CompanyInfo{ Symbol = "BP", Name = "BP"},
                new CompanyInfo{ Symbol = "VOD", Name = "Vodafone Group"},
                new CompanyInfo{ Symbol = "AZN", Name = "AstraZeneca"},
                new CompanyInfo{ Symbol = "RB", Name = "Reckitt Benckiser"},
                new CompanyInfo{ Symbol = "DGE", Name = "Diageo"},
                new CompanyInfo{ Symbol = "BT.A", Name = "BT Group"},
                new CompanyInfo{ Symbol = "LLOY", Name = "Lloyds Banking Group"},
                new CompanyInfo{ Symbol = "BLT", Name = "BHP Billiton"},
                new CompanyInfo{ Symbol = "NG", Name = "National Grid plc"},
                new CompanyInfo{ Symbol = "IMB", Name = "Imperial Brands"},
                new CompanyInfo{ Symbol = "RIO", Name = "Rio Tinto Group"},
                new CompanyInfo{ Symbol = "PRU", Name = "Prudential plc"},
                new CompanyInfo{ Symbol = "RBS", Name = "Royal Bank of Scotland Group"},
                new CompanyInfo{ Symbol = "BARC", Name = "Barclays"},
                new CompanyInfo{ Symbol = "ABF", Name = "Associated British Foods"},
                new CompanyInfo{ Symbol = "REL", Name = "RELX Group"},
                new CompanyInfo{ Symbol = "REX", Name = "Rexam"},
                new CompanyInfo{ Symbol = "CCL", Name = "Carnival Corporation & plc"},
                new CompanyInfo{ Symbol = "SHP", Name = "Shire plc"},
                new CompanyInfo{ Symbol = "CPG", Name = "Compass Group"},
                new CompanyInfo{ Symbol = "WPP", Name = "WPP plc"},
                new CompanyInfo{ Symbol = "AV.", Name = "Aviva"},
                new CompanyInfo{ Symbol = "SKY", Name = "Sky plc"},
                new CompanyInfo{ Symbol = "GLEN", Name = "Glencore"},
                new CompanyInfo{ Symbol = "BA.", Name = "BAE Systems"},
                new CompanyInfo{ Symbol = "TSCO", Name = "Tesco"},
                new CompanyInfo{ Symbol = "SSE", Name = "SSE plc"},
                new CompanyInfo{ Symbol = "STAN", Name = "Standard Chartered"},
                new CompanyInfo{ Symbol = "LGEN", Name = "Legal & General"},
                new CompanyInfo{ Symbol = "ARM", Name = "ARM Holdings"},
                new CompanyInfo{ Symbol = "RR.", Name = "Rolls-Royce Holdings"},
                new CompanyInfo{ Symbol = "EXPN", Name = "Experian"},
                new CompanyInfo{ Symbol = "IAG", Name = "International Consolidated Airlines Group SA"},
                new CompanyInfo{ Symbol = "CRH", Name = "CRH plc"},
                new CompanyInfo{ Symbol = "CNA", Name = "Centrica"},
                new CompanyInfo{ Symbol = "SN.", Name = "Smith & Nephew"},
                new CompanyInfo{ Symbol = "ITV", Name = "ITV plc"},
                new CompanyInfo{ Symbol = "WOS", Name = "Wolseley plc"},
                new CompanyInfo{ Symbol = "OML", Name = "Old Mutual"},
                new CompanyInfo{ Symbol = "LAND", Name = "Land Securities"},
                new CompanyInfo{ Symbol = "LSE", Name = "London Stock Exchange Group"},
                new CompanyInfo{ Symbol = "KGF", Name = "Kingfisher plc"},
                new CompanyInfo{ Symbol = "CPI", Name = "Capita"},
                new CompanyInfo{ Symbol = "BLND", Name = "British Land"},
                new CompanyInfo{ Symbol = "WTB", Name = "Whitbread"},
                new CompanyInfo{ Symbol = "MKS", Name = "Marks & Spencer"},
                new CompanyInfo{ Symbol = "FRES", Name = "Fresnillo plc"},
                new CompanyInfo{ Symbol = "NXT", Name = "Next plc"},
                new CompanyInfo{ Symbol = "SDR", Name = "Schroders"},
                new CompanyInfo{ Symbol = "SL", Name = "Standard Life"},
                new CompanyInfo{ Symbol = "PSON", Name = "Pearson PLC"},
                new CompanyInfo{ Symbol = "BNZL", Name = "Bunzl"},
                new CompanyInfo{ Symbol = "MNDI", Name = "Mondi"},
                new CompanyInfo{ Symbol = "UU", Name = "United Utilities"},
                new CompanyInfo{ Symbol = "PSN", Name = "Persimmon plc"},
                new CompanyInfo{ Symbol = "SGE", Name = "Sage Group"},
                new CompanyInfo{ Symbol = "EZJ", Name = "EasyJet"},
                new CompanyInfo{ Symbol = "AAL", Name = "Anglo American plc"},
                new CompanyInfo{ Symbol = "TW.", Name = "Taylor Wimpey"},
                new CompanyInfo{ Symbol = "TUI", Name = "TUI Group"},
                new CompanyInfo{ Symbol = "WPG", Name = "Worldpay"},
                new CompanyInfo{ Symbol = "RRS", Name = "Randgold Resources"},
                new CompanyInfo{ Symbol = "HL", Name = "Hargreaves Lansdown"},
                new CompanyInfo{ Symbol = "BDEV", Name = "Barratt Developments"},
                new CompanyInfo{ Symbol = "IHG", Name = "InterContinental Hotels Group"},
                new CompanyInfo{ Symbol = "BRBY", Name = "Burberry"},
                new CompanyInfo{ Symbol = "DC.", Name = "Dixons Carphone"},
                new CompanyInfo{ Symbol = "DLG", Name = "Direct Line Group"},
                new CompanyInfo{ Symbol = "CCH", Name = "Coca-Cola HBC AG" },
                new CompanyInfo{ Symbol = "SVT", Name = "Severn Trent"},
                new CompanyInfo{ Symbol = "DCC", Name = "DCC plc"},
                new CompanyInfo{ Symbol = "SBRY", Name = "Sainsbury\"s"},
                new CompanyInfo{ Symbol = "ADM", Name = "Admiral Group"},
                new CompanyInfo{ Symbol = "GKN", Name = "GKN"},
                new CompanyInfo{ Symbol = "JMAT", Name = "Johnson Matthey"},
                new CompanyInfo{ Symbol = "PFG", Name = "Provident Financial"},
                new CompanyInfo{ Symbol = "ANTO", Name = "Antofagasta"},
                new CompanyInfo{ Symbol = "STJ", Name = "St. James\"s Place plc"},
                new CompanyInfo{ Symbol = "ITRK", Name = "Intertek"},
                new CompanyInfo{ Symbol = "BAB", Name = "Babcock International"},
                new CompanyInfo{ Symbol = "BKG", Name = "Berkeley Group Holdings"},
                new CompanyInfo{ Symbol = "ISAT", Name = "Inmarsat"},
                new CompanyInfo{ Symbol = "TPK", Name = "Travis Perkins" },
                new CompanyInfo{ Symbol = "HMSO", Name = "Hammerson"},
                new CompanyInfo{ Symbol = "MERL", Name = "Merlin Entertainments"},
                new CompanyInfo{ Symbol = "RMG", Name = "Royal Mail"},
                new CompanyInfo{ Symbol = "AHT", Name = "Ashtead Group"},
                new CompanyInfo{ Symbol = "RSA", Name = "RSA Insurance Group"},
                new CompanyInfo{ Symbol = "III", Name = "3i"},
                new CompanyInfo{ Symbol = "INTU", Name = "Intu Properties"},
                new CompanyInfo{ Symbol = "SMIN", Name = "Smiths Group"},
                new CompanyInfo{ Symbol = "HIK", Name = "Hikma Pharmaceuticals"},
                new CompanyInfo{ Symbol = "ADN", Name = "Aberdeen Asset Management"},
                new CompanyInfo{ Symbol = "SPD", Name = "Sports Direct"}
            };
        }
    }
}