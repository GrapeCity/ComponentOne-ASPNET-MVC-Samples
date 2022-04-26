using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcExplorer.Models
{
    public class SalesAnalysis
    {
        public string Country { get; set; }
        public List<int> Sales { get; set; }
        public List<int> Downloads { get; set; }
        public List<int> Queries { get; set; }

        public SalesAnalysis(string country, int[] downloads, int[] sales, int[] queries)
        {
            Country = country;
            Sales = sales.ToList();
            Downloads = downloads.ToList();
            Queries = queries.ToList();
        }

        public static List<SalesAnalysis> GetData()
        {
            int[][][] stats = {
                new int[][]{ new int[] { 8, 22, 24, 61, 77 }, new int[] { 30, 49, 80, 110, 130 }, new int[] { 6, 20, 32, 40, 76 } },
                new int[][]{ new int[] { 4, 4, 29, 39, 77 }, new int[] { 11, 30, 36, 99, 119 }, new int[] { 15, 26, 30, 34, 44 } },
                new int[][]{ new int[] { 45, 72, 87, 89, 94 }, new int[] { 5, 9, 47, 60, 104 }, new int[] { 3, 13, 63, 66, 73 } },
                new int[][]{ new int[] { 5, 10, 45, 51, 97 }, new int[] { 3, 35, 39, 56, 100 }, new int[] { 5, 31, 41, 76, 90 } },
                new int[][]{ new int[] { 32, 43, 53, 78, 80 }, new int[] { 7, 20, 61, 74, 95 }, new int[] { 20, 22, 49, 80, 91 } },
                new int[][]{ new int[] { 22, 28, 53, 63, 92 }, new int[] { 18, 50, 72, 100, 112 }, new int[] { 6, 18, 30, 42, 82 } }
            };

            var countries = new string[] { "US", "Germany", "UK", "Japan", "France", "China" };
            var data = new List<SalesAnalysis>();
            for (var i = 0; i < countries.Length; i++)
            {
                data.Add(new SalesAnalysis(countries[i], stats[i][0], stats[i][1], stats[i][2]));
            }
            return data;
        }
    }
}