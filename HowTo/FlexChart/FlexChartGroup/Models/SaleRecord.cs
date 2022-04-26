using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexChartGroup.Models
{
    public class SaleRecord
    {
        public SaleRecord()
        { }

        public int ID { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Date { get; set; }
        public DateTime date { get; set; }
        public double Amount { get; set; }

        public static List<SaleRecord> GetSaleRecordsData()
        {
            List<SaleRecord> _saleRecords = new List<SaleRecord>() { };

            string[] countries = "US,Germany,UK,Japan,Italy,Greece".Split(',');
            Dictionary<string, string[]> citysByCountry = new Dictionary<string, string[]>
                    {
                        {"US", new string[]{"New York", "Los Angeles", "Chicago", "Phoenix", "Dallas"}},
                        {"Germany", new string[]{"Berlin", "Hamburg", "Munich", "Cologne", "Frankfurt"}},
                        {"UK", new string[]{"London", "Birmingham", "Leeds", "Glasgow", "Sheffield"}},
                        {"Japan", new string[]{"Tokyo", "Kanagawa", "Osaka", "Aichi", "Hokkaido"}},
                        {"Italy", new string[]{"Rome", "Milan", "Naples", "Turin", "Palermo"}},
                        {"Greece", new string[]{"Athens", "Thessaloniki", "Patras", "Heraklion", "Larissa"}},
                    };
            int[] years = new int[] { 2010, 2011, 2012, 2013, 2014 };
            int countriesLen = countries.Count();
            string country; int yearIndex, cityIndex;

            var rand = new Random();
            for (int i = 0; i < 500; i++)
            {
                yearIndex = (int)(rand.NextDouble() * 5);
                cityIndex = (int)(rand.NextDouble() * 5);
                country = countries[i % countriesLen];
                _saleRecords.Add(new SaleRecord
                {
                    ID = i,
                    Country = country,
                    City = citysByCountry[country][cityIndex],
                    Date = years[yearIndex].ToString(),
                    date = new DateTime(years[yearIndex], i % 12 + 1, i % 27 + 1),
                    Amount = rand.NextDouble() * 10000
                });
            }
            return _saleRecords;
        }
    }
}