using C1.Web.Mvc;
using FlexChartGroup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Drawing;

namespace FlexChartGroup.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            InitialData(null);
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            InitialData(form);
            return View();
        }

        private void InitialData(FormCollection form)
        {
            ViewBag.Palette = new Color[] { Color.FromArgb(178, 136, 189, 230), Color.FromArgb(178, 251, 178, 88), Color.FromArgb(178, 144, 205, 151) };
            
            ViewBag.GroupBySet = GroupBySet;
            ViewBag.AggregateSet = AggregateSet;
            ViewBag.ChartTypeSet = ChartTypeSet;
            string groupby = form != null && !string.IsNullOrEmpty(form["groupby"]) ? form["groupby"] : "Country and City";
            string aggregate = form != null && !string.IsNullOrEmpty(form["aggregate"]) ? form["aggregate"] : "Sum";
            string charttype = form != null && !string.IsNullOrEmpty(form["charttype"]) ? form["charttype"] : "Column";
            ViewBag.GroupBy = groupby;
            ViewBag.Aggregate = aggregate;
            ViewBag.ChartType = charttype;
            ViewBag.SelectedIndexes = new int[] 
            { 
                GroupBySet.Keys.ToList().IndexOf(groupby), 
                AggregateSet.Keys.ToList().IndexOf(aggregate), 
                ChartTypeSet.Keys.ToList().IndexOf(charttype) 
            };

            ViewBag.Navigator1 = string.Empty;
            ViewBag.Navigator2 = string.Empty;

            List<SaleRecord> saleRecords = null;
            if (form == null)
            {
                ViewBag.TargetFields = string.Empty;
                ViewBag.TargetLevel = 0;
                ViewBag.CanClickDeep = true;
                
                ViewBag.BindingX = "Country";
                ViewBag.ChartTitle = "Sum By Country";

                saleRecords = GetAggregatedRecords(GetGroupedRecordsWithID(SaleRecords, "country"), "Sum");
            }
            else
            {
                string[] groupPath = GroupBySet[groupby].Split(',');
                string[] targetFields = form["tfields"].Split(',');
                ViewBag.TargetFields = form["tfields"];
                int targetLevel = int.Parse(form["tlevel"]);
                ViewBag.TargetLevel = targetLevel;
                ViewBag.CanClickDeep = targetLevel < groupPath.Length - 1;

                if (targetLevel > 0)
                {
                    ViewBag.Navigator1 = groupPath[0] == "date" ? "Year" : GetPropertyName(groupPath[0]);
                }
                if (targetLevel > 1)
                {
                    ViewBag.Navigator2 = targetFields[0];
                }
                string headerGroup = groupPath[targetLevel] == "date" ? "Year" : GetPropertyName(groupPath[targetLevel]);
                ViewBag.ChartTitle = (targetLevel > 0 ? (targetFields[targetLevel - 1] + " - ") : "") + aggregate + " By " + headerGroup;

                Dictionary<string, List<int>> group = null;
                saleRecords = SaleRecords;
                group = GetGroupedRecordsWithID(saleRecords, groupPath[0]);
                ViewBag.BindingX = GetPropertyName(groupPath[0]);

                for (int i = 1; i <= targetLevel; i++)
                {
                    saleRecords = new List<SaleRecord>() { };
                    foreach (int idx in group[targetFields[i-1]]) 
                    {
                        saleRecords.Add(SaleRecords[idx]);
                    }
                    group = GetGroupedRecordsWithID(saleRecords, groupPath[i]);
                    ViewBag.BindingX = GetPropertyName(groupPath[i]);
                }
                saleRecords = GetAggregatedRecords(group, AggregateSet[aggregate]);
            }
            ViewBag.ItemsSource = saleRecords;
        }


        private Dictionary<string, string> GroupBySet = new Dictionary<string, string> 
        { 
            {"Country and City", "country,city"}, 
            {"Country and Year", "country,date"}, 
            {"Year and Country", "date,country"}, 
            {"Country , City and Year", "country,city,date"}, 
            {"Year, Country and City", "date,country,city"} 
        };
        private Dictionary<string, string> AggregateSet = new Dictionary<string, string> 
        { 
            {"Sum", "Sum"}, 
            {"Average", "Avg"}, 
            {"Maximum", "Max"}, 
            {"Minimum", "Min"}
        };
        private Dictionary<string, string> ChartTypeSet = new Dictionary<string, string> 
        { 
            {"Column", "column"}, 
            {"Pie", "pie"}, 
        };
        private string GetPropertyName(string field)
        {
            return field.Substring(0, 1).ToUpper() + field.Substring(1);
        }

        private static List<SaleRecord> _saleRecords = null;
        private List<SaleRecord> SaleRecords
        {
            get
            {
                if (_saleRecords == null)
                {
                    _saleRecords = SaleRecord.GetSaleRecordsData();
                }

                return _saleRecords;
            }
        }

        private Dictionary<string, List<int>> GetGroupedRecordsWithID(List<SaleRecord> sourceRecords, string groupField) 
        {
            Dictionary<string, List<int>> groupedRecordsID = new Dictionary<string, List<int>>() { };

            switch (groupField)
            { 
                case "country":
                    IEnumerable<IGrouping<string, int>> queryCountry = sourceRecords.GroupBy(sr => sr.Country, sr => sr.ID);
                    foreach (IGrouping<string, int> recordGroup in queryCountry)
                    {
                        groupedRecordsID[recordGroup.Key] = recordGroup.ToList();
                    }
                    break;
                case "city":
                    IEnumerable<IGrouping<string, int>> queryCity = sourceRecords.GroupBy(sr => sr.City, sr => sr.ID);
                    foreach (IGrouping<string, int> recordGroup in queryCity)
                    {
                        groupedRecordsID[recordGroup.Key] = recordGroup.ToList();
                    }
                    break;
                case "date":
                    IEnumerable<IGrouping<string, int>> queryDate = sourceRecords.GroupBy(sr => sr.Date, sr => sr.ID);
                    foreach (IGrouping<string, int> recordGroup in queryDate)
                    {
                        groupedRecordsID[recordGroup.Key] = recordGroup.ToList();
                    }
                    break;
            }

            return groupedRecordsID;
        }

        private List<SaleRecord> GetAggregatedRecords(Dictionary<string, List<int>> groupedSourceRecords, string aggField)
        {
            List<SaleRecord> aggRecords = new List<SaleRecord>() { };

            bool isDate = false;
            for (int i = 0; i < groupedSourceRecords.Keys.Count; i++)
            {
                string key = groupedSourceRecords.Keys.ElementAt(i);
                double[] amountArray = new double[] { };
                double amount = GetAggregatedValue(groupedSourceRecords[key], aggField);

                SaleRecord sr = new SaleRecord { Country = key, City = key, Date=key, Amount = amount };
                DateTime dt;
                if (DateTime.TryParse(key + "-1-1", out dt))
                {
                    isDate = true;
                    sr.date = dt;
                }

                aggRecords.Add(sr);
            }
            if (isDate)
            {
                aggRecords.Sort((left, right) =>
                {
                    if (left.date > right.date)
                        return 1;
                    else
                        return -1;
                });
            }

            return aggRecords;
        }

        private double GetAggregatedValue(List<int> saleRecordsID, string aggField)
        {
            double result = SaleRecords[saleRecordsID[0]].Amount; 
            bool first = true;
            foreach (int idx in saleRecordsID)
            {
                if (first)
                {
                    first = false;
                    continue;
                }
                SaleRecord sr = SaleRecords[idx];
                if (aggField == "Max" || aggField == "Min")
                {
                    if (aggField == "Max" && result <= sr.Amount ||
                        aggField == "Min" && result >= sr.Amount)
                    {
                        result = sr.Amount;
                    }
                }
                else
                {
                    result += sr.Amount;
                }
            }
            if (aggField == "Avg")
            {
                result = result / saleRecordsID.Count;
            }

            return result;
        }
    }
}
