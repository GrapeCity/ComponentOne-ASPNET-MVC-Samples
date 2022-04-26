using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcExplorer.Models
{
    public class DashboardData
    {
        private static string[] Countries = { "US", "Germany", "UK", "Japan", "China", "India" };
        private static int[] Years = { DateTime.Today.Year - 5, DateTime.Today.Year - 4, DateTime.Today.Year - 3, DateTime.Today.Year - 2, DateTime.Today.Year - 1, DateTime.Today.Year };

        private static IEnumerable<Expenses> ExpensesData = GenerateExpenses();
        private static IEnumerable<Expenses> GenerateExpenses()
        {
            var rand = new Random(0);
            var result = new List<Expenses>();
            var minDate = DateTime.Today.AddMonths(-12);
            var maxDate = DateTime.Today.AddDays(-1);
            while (minDate <= maxDate)
            {
                var list = Countries.Select((country, id) =>
                {
                    var expense = rand.Next(250, 3000) * rand.Next(500, 100000);
                    return new Expenses
                    {
                        ID = id + 1,
                        TxnDate = minDate,
                        Month = minDate.Month,
                        Year = minDate.Year,
                        Country = country,
                        Expense = expense
                    };
                });
                result.AddRange(list);
                minDate = minDate.AddDays(1);
            }
            return result;
        }

        private static IEnumerable<BudgetedCost> BudgetedCost = GenerateBudgetedCost();
        private static IEnumerable<BudgetedCost> GenerateBudgetedCost()
        {
            var rand = new Random(0);
            List<BudgetedCost> List = new List<BudgetedCost>();
            for (int yy = 0; yy < Years.Length; yy++)
            {
                var list = Countries.Select((country, id) =>
                {
                    double budget = rand.Next(250, 3000) * rand.Next(500, 100000) * 36500000;
                    if (budget < 0)
                        budget = budget * (-1);
                    return new BudgetedCost
                    {
                        ID = id + 1,
                        Year = Years[yy],
                        Country = country,
                        Budget = budget
                    };
                });
                List.AddRange(list);
            }
            return List;
        }

        private static IEnumerable<Sales> Sales = GenerateSales();
        private static IEnumerable<Sales> GenerateSales()
        {
            var rand = new Random(0);
            var result = new List<Sales>();
            var minDate = DateTime.Today.AddMonths(-12);
            var maxDate = DateTime.Today.AddDays(-1);
            while (minDate <= maxDate)
            {
                var list = Countries.Select((country, id) =>
                {
                    var sale = rand.Next(500, 5000) * rand.Next(200, 150000);
                    return new Sales
                    {
                        ID = id + 1,
                        TxnDate = minDate,
                        Month = minDate.Month,
                        Year = minDate.Year,
                        Country = country,
                        Sale = sale
                    };
                });
                result.AddRange(list);
                minDate = minDate.AddDays(1);
            }
            return result;
        }

        private static IEnumerable<ProjectedSales> ProjectedSales = GenerateProjectedSales();
        private static IEnumerable<ProjectedSales> GenerateProjectedSales()
        {
            var rand = new Random(0);
            var result = new List<ProjectedSales>();
            for (int yy = 0; yy < Years.Length; yy++)
            {
                var list = Countries.Select((country, id) =>
                {
                    var sales = rand.Next(700, 2000) * 15000;
                    return new ProjectedSales
                    {
                        ID = id + 1,
                        Year = Years[yy],
                        Country = country,
                        Sales = sales
                    };
                });
                result.AddRange(list);
            }
            return result;
        }


        public IDictionary<string, object[]> Settings
        {
            get
            {
                var settings = new Dictionary<string, object[]>
                {
                    {"ChartType", new object[]{"Column", "Bar", "Scatter", "Line",
                        "LineSymbols", "Area", "Spline", "SplineSymbols", "SplineArea"}},
                    {"TrendLine", new object[]{ "AverageX", "AverageY", "Exponential",
                        "Fourier", "Linear", "Logarithmic", "MaxX", "MaxY", "MinX", "MinY", "Polynomial", "Power"}}
                };
                return settings;
            }
        }

        public string KPIsStr
        {
            get
            {
                return DateTime.Today.Year.ToString();
            }
        }
        private IEnumerable<KPIs> _kpisData;
        public IEnumerable<KPIs> KPIsData
        {
            get
            {
                if (_kpisData == null)
                {
                    var result = new List<KPIs>();
                    var salesListByCountry = Sales.GroupBy(t => new { Year = t.Year, Country = t.Country })
                        .Select(t =>
                        new
                        {
                            Year = t.Key.Year,
                            Country = t.Key.Country,
                            TotalSales = t.Sum(tc => tc.Sale)
                        }).Where(x => x.Year == DateTime.Today.Year);

                    var expensesListByCountry = ExpensesData.GroupBy(t => new { Year = t.Year, Country = t.Country })
                        .Select(t =>
                        new
                        {
                            Year = t.Key.Year,
                            Country = t.Key.Country,
                            TotalExpenses = t.Sum(tc => tc.Expense)
                        }).Where(x => x.Year == DateTime.Today.Year);

                    var finalList = salesListByCountry
                        .Join(expensesListByCountry, o => o.Year + o.Country, i => i.Year + i.Country, (o, i) =>
                        new
                        {
                            Year = o.Year,
                            Country = o.Country,
                            Sales = o.TotalSales,
                            Expenses = i.TotalExpenses,
                            Profit = o.TotalSales - i.TotalExpenses,
                            Max = o.TotalSales > i.TotalExpenses ? o.TotalSales : i.TotalExpenses
                        }).ToList();

                    for (int rCount = 0; rCount < finalList.Count; rCount++)
                    {
                        result.Add(new KPIs
                        {
                            Country = finalList[rCount].Country,
                            Expenses = finalList[rCount].Expenses,
                            Max = finalList[rCount].Max,
                            Profit = finalList[rCount].Profit,
                            Sales = finalList[rCount].Sales
                        });
                    }
                    result.Add(new KPIs
                    {
                        Country = "All",
                        Expenses = finalList.Sum(x => x.Expenses),
                        Max = finalList.Max(x => x.Max),
                        Profit = finalList.Sum(x => x.Profit),
                        Sales = finalList.Sum(x => x.Sales)
                    });
                    _kpisData = result;
                }

                return _kpisData;
            }
        }

        public string CostBudgetingStr
        {
            get
            {
                return (DateTime.Today.Year).ToString();
            }
        }
        public string CostBudgetingChartType
        {
            get
            {
                return "Column";
            }
        }

        private IEnumerable<CostBudgeting> _costBudgetingData;
        public IEnumerable<CostBudgeting> CostBudgetingData
        {
            get
            {
                if (_costBudgetingData == null)
                {

                    var resultList = new List<CostBudgeting>();
                    var GroupByExpenses = ExpensesData.GroupBy(t => new { Country = t.Country, Year = t.TxnDate.Year })
                        .Select(t =>
                        new
                        {
                            Country = t.Key.Country,
                            Year = t.Key.Year,
                            TotalExpenses = t.Sum(tc => tc.Expense)
                        });
                    var temp = GroupByExpenses.Join(BudgetedCost, o => (o.Year + o.Country), s => (s.Year + s.Country), (o, s) =>
                        new
                        {
                            Country = o.Country,
                            Year = o.Year,
                            Expenses = o.TotalExpenses,
                            Budget = s.Budget
                        }).Where(x => x.Year == DateTime.Today.Year).OrderBy(t => t.Year).ThenBy(t => t.Country);

                    for (int RCount = 0; RCount < temp.Count(); RCount++)
                    {
                        resultList.Add(new CostBudgeting
                        {
                            Country = temp.ToList()[RCount].Country,
                            Year = temp.ToList()[RCount].Year,
                            Expenses = temp.ToList()[RCount].Expenses,
                            Budget = temp.ToList()[RCount].Budget
                        });
                    }
                    _costBudgetingData = resultList;
                }

                return _costBudgetingData;
            }
        }


        public string MonthlySalesStr
        {
            get
            {
                return MonthlySalesData.LastOrDefault().MonthName.ToString() 
                    + " - " 
                    + MonthlySalesData.FirstOrDefault().MonthName.ToString();
            }
        }
        public string MonthlySalesChartType
        {
            get
            {
                return "Line";
            }
        }
        public string MonthlySalesChartTrendLine
        {
            get
            {
                return "AverageY";
            }
        }
        private IEnumerable<MonthlySales> _monthlySalesData;
        public IEnumerable<MonthlySales> MonthlySalesData
        {
            get
            {
                if (_monthlySalesData == null)
                {
                    var resultList = new List<MonthlySales>();
                    var minDate = DateTime.Today.AddMonths(-4);
                    var maxDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1);
                    var groupBySales = Sales.Where(x => x.TxnDate >= minDate && x.TxnDate <= maxDate)
                        .GroupBy(t => new { TxnDate = t.TxnDate, Country = t.Country })
                        .Select(t =>
                        new
                        {
                            Country = t.Key.Country,
                            TxnDate = t.Key.TxnDate,
                            Month = t.Key.TxnDate.Month,
                            Year = t.Key.TxnDate.Year,
                            TotalSales = t.Sum(tc => tc.Sale)
                        }).OrderByDescending(x => x.TxnDate)
                        .Select(x =>
                        new
                        {
                            Country = x.Country,
                            TxnDate = x.TxnDate,
                            MonthName = x.Month.ToString() + "/" + x.Year.ToString(),
                            Sales = x.TotalSales
                        });

                    var groupByExpenses = ExpensesData.Where(x => x.TxnDate >= minDate && x.TxnDate <= maxDate)
                        .GroupBy(t => new { TxnDate = t.TxnDate, Country = t.Country })
                        .Select(t =>
                        new
                        {
                            Country = t.Key.Country,
                            TxnDate = t.Key.TxnDate,
                            Month = t.Key.TxnDate.Month,
                            Year = t.Key.TxnDate.Year,
                            TotalExpenses = t.Sum(tc => tc.Expense)
                        }).OrderByDescending(x => x.TxnDate)
                        .Select(x =>
                        new
                        {
                            Country = x.Country,
                            TxnDate = x.TxnDate,
                            MonthName = x.Month.ToString() + "/" + x.Year.ToString(),
                            Expenses = x.TotalExpenses
                        });

                    var finalList = groupBySales
                        .Join(groupByExpenses, o => o.TxnDate + o.Country, i => i.TxnDate + i.Country, (o, i) =>
                            new
                            {
                                Country = o.Country,
                                TxnDate = o.TxnDate,
                                MonthName = o.MonthName,
                                Sales = o.Sales,
                                Expenses = i.Expenses
                            });
                    foreach (var item in finalList)
                    {
                        MonthlySales temp = new MonthlySales();
                        temp.Country = item.Country;
                        temp.TxnDate = item.TxnDate;
                        temp.MonthName = item.MonthName;
                        temp.Sales = item.Sales;
                        temp.Expenses = item.Expenses;
                        resultList.Add(temp);
                    }

                    var monthNames = resultList.GroupBy(p => p.TxnDate)
                        .Select(g => g.First()).ToList();
                    for (int Mcount = 0; Mcount < monthNames.Count(); Mcount++)
                    {
                        resultList.Add(new MonthlySales
                        {
                            Country = "All",
                            TxnDate = monthNames[Mcount].TxnDate,
                            MonthName = monthNames[Mcount].MonthName,
                            Sales = resultList.Where(x => x.TxnDate == monthNames[Mcount].TxnDate).Sum(x => x.Sales),
                            Expenses = resultList.Where(x => x.TxnDate == monthNames[Mcount].TxnDate).Sum(x => x.Expenses)
                        });
                    }

                    _monthlySalesData = resultList;
                }

                return _monthlySalesData;
            }
        }


        public string MonthlyProfitStr
        {
            get
            {
                return MonthlyProfitData.FirstOrDefault().MonthName.ToString() 
                    + " - " 
                    + MonthlyProfitData.LastOrDefault().MonthName.ToString();
            }
        }
        public string MonthlyProfitChartType
        {
            get
            {
                return "Column";
            }
        }

        private IEnumerable<MonthlyProfit> _monthlyProfitData;
        public IEnumerable<MonthlyProfit> MonthlyProfitData
        {
            get
            {
                if (_monthlyProfitData == null)
                {
                    var resultList = new List<MonthlyProfit>();
                    var groupBySales = Sales.GroupBy(t => new { Country = t.Country, Month = t.Month, Year = t.Year })
                        .Select(t =>
                        new
                        {
                            Country = t.Key.Country,
                            Month = t.Key.Month,
                            Year = t.Key.Year,
                            TotalSales = t.Sum(tc => tc.Sale)
                        }).OrderByDescending(x => x.Year).ThenByDescending(x => x.Month)
                        .Take(BaseData.countries.Count() * 12).OrderBy(x => x.Year).ThenBy(x => x.Month)
                        .Select(x =>
                        new
                        {
                            Country = x.Country,
                            MonthName = x.Month.ToString() + "/" + x.Year.ToString(),
                            Sales = x.TotalSales
                        });

                    var groupByExpenses = ExpensesData.GroupBy(t => new { Country = t.Country, Month = t.Month, Year = t.Year })
                        .Select(t =>
                        new
                        {
                            Country = t.Key.Country,
                            Month = t.Key.Month,
                            Year = t.Key.Year,
                            TotalExpenses = t.Sum(tc => tc.Expense)
                        }).OrderByDescending(x => x.Year).ThenByDescending(x => x.Month)
                        .Take(BaseData.countries.Count() * 12).OrderBy(x => x.Year).ThenBy(x => x.Month)
                        .Select(x =>
                        new
                        {
                            Country = x.Country,
                            MonthName = x.Month.ToString() + "/" + x.Year.ToString(),
                            Expenses = x.TotalExpenses
                        });
                    var finalList = groupBySales
                        .Join(groupByExpenses, o => (o.MonthName + o.Country), i => (i.MonthName + i.Country), (o, i) =>
                                   new
                                   {
                                       Country = o.Country,
                                       MonthName = o.MonthName,
                                       Sales = o.Sales,
                                       Expenses = i.Expenses,
                                       Profit = o.Sales - i.Expenses
                                   });
                    for (int rCount = 0; rCount < finalList.Count(); rCount++)
                    {
                        resultList.Add(new MonthlyProfit
                        {
                            Country = finalList.ToList()[rCount].Country,
                            MonthName = finalList.ToList()[rCount].MonthName,
                            Sales = finalList.ToList()[rCount].Sales,
                            Expenses = finalList.ToList()[rCount].Expenses,
                            Profit = finalList.ToList()[rCount].Profit
                        });
                    }
                    var monthNames = resultList.GroupBy(p => p.MonthName)
                        .Select(g => g.First().MonthName).ToList();
                    for (int mcount = 0; mcount < monthNames.Count(); mcount++)
                    {
                        resultList.Add(new MonthlyProfit
                        {
                            Country = "All",
                            MonthName = monthNames[mcount].ToString(),
                            Sales = resultList.Where(x => x.MonthName == monthNames[mcount].ToString()).Sum(x => x.Sales),
                            Expenses = resultList.Where(x => x.MonthName == monthNames[mcount].ToString()).Sum(x => x.Expenses),
                            Profit = resultList.Where(x => x.MonthName == monthNames[mcount].ToString()).Sum(x => x.Profit)
                        });
                    }
                    _monthlyProfitData = resultList;
                }

                return _monthlyProfitData;
            }
        }


        public string CountrywiseSalesStr
        {
            get
            {
                return CountrywiseSalesData.FirstOrDefault().Year.ToString();
            }
        }
        public string CountrywiseSalesChartType
        {
            get
            {
                return "Bar";
            }
        }
        private IEnumerable<CountrywiseSales> _countrywiseSalesData;
        public IEnumerable<CountrywiseSales> CountrywiseSalesData
        {
            get
            {
                if (_countrywiseSalesData == null)
                {
                    var resultList = new List<CountrywiseSales>();
                    var groupBySales = Sales.GroupBy(t => new { Year = t.Year, Country = t.Country })
                        .Select(t =>
                        new
                        {
                            Year = t.Key.Year,
                            Country = t.Key.Country,
                            TotalSales = t.Sum(tc => tc.Sale)
                        }).Where(x => x.Year == DateTime.Today.Year);

                    var groupByExpenses = ExpensesData.GroupBy(t => new { Year = t.Year, Country = t.Country })
                    .Select(t =>
                    new
                    {
                        Year = t.Key.Year,
                        Country = t.Key.Country,
                        TotalExpenses = t.Sum(tc => tc.Expense)
                    }).Where(x => x.Year == DateTime.Today.Year);


                    var finalList = groupBySales
                        .Join(groupByExpenses, o => o.Year + o.Country, i => i.Year + i.Country, (o, i) =>
                                   new
                                   {
                                       Year = o.Year,
                                       Country = o.Country,
                                       Sales = o.TotalSales,
                                       Expenses = i.TotalExpenses
                                   });
                    for (int rCount = 0; rCount < finalList.Count(); rCount++)
                    {
                        resultList.Add(new CountrywiseSales
                        {
                            Year = finalList.ToList()[rCount].Year,
                            Country = finalList.ToList()[rCount].Country,
                            Sales = finalList.ToList()[rCount].Sales,
                            Expenses = finalList.ToList()[rCount].Expenses
                        });
                    }
                    _countrywiseSalesData = resultList;
                }

                return _countrywiseSalesData;
            }
        }


        public string SalesDashboardStr
        {
            get
            {
                return SalesDashboardData.FirstOrDefault().Year.ToString();
            }
        }
        private IEnumerable<SalesDashboard> _salesDashboardData;
        public IEnumerable<SalesDashboard> SalesDashboardData
        {
            get
            {
                if (_salesDashboardData == null)
                {
                    var resultList = new List<SalesDashboard>();
                    var actualSalesList = Sales.GroupBy(t => new { Year = t.Year, Country = t.Country })
                        .Select(t =>
                        new
                        {
                            Year = t.Key.Year,
                            Country = t.Key.Country,
                            TotalSales = t.Sum(tc => tc.Sale)
                        }).Where(x => x.Year == DateTime.Today.Year);

                    var projectedSalesList = BaseData.GenerateProjectedSales().Where(x => x.Year == DateTime.Today.Year)
                        .Select(x =>
                        new
                        {
                            Country = x.Country,
                            Year = x.Year,
                            ProjectedSales = x.Sales,
                            Max = ProjectedSales.Max(s => s.Sales)
                        });

                    var finalList = actualSalesList
                        .Join(projectedSalesList, o => o.Year + o.Country, i => i.Year + i.Country, (o, i) =>
                        new
                        {
                            Year = o.Year,
                            Country = o.Country,
                            Sales = o.TotalSales,
                            Projected = i.ProjectedSales,
                            Max = i.Max
                        }).ToList();
                    for (int rCount = 0; rCount < finalList.Count(); rCount++)
                    {
                        resultList.Add(new SalesDashboard
                        {
                            Year = finalList.ToList()[rCount].Year,
                            Country = finalList.ToList()[rCount].Country,
                            Sales = finalList.ToList()[rCount].Sales,
                            Projected = finalList.ToList()[rCount].Projected,
                            BulletGValue = finalList.ToList()[rCount].Sales,
                            BulletGTarget = finalList.ToList()[rCount].Projected,
                            BulletGMax = finalList.ToList()[rCount].Max,
                            BulletGBad = finalList.ToList()[rCount].Projected / 2,
                            BulletGGood = finalList.ToList()[rCount].Projected * 80 / 100,
                            Last12MonthsSales = GetLast12MonthsSalesData(finalList.ToList()[rCount].Country),
                            KPIsData = KPIsData.Where(x => x.Country == finalList.ToList()[rCount].Country).FirstOrDefault()
                        });
                    }
                    resultList.Insert(0, new SalesDashboard
                    {
                        Year = finalList.ToList()[0].Year,
                        Country = "All",
                        Sales = finalList.Sum(x => x.Sales),
                        Projected = finalList.Sum(x => x.Projected),
                        BulletGValue = finalList.Sum(x => x.Sales),
                        BulletGTarget = finalList.Sum(x => x.Projected),
                        BulletGMax = finalList.Sum(x => x.Max),
                        BulletGBad = finalList.Sum(x => x.Projected) / 2,
                        BulletGGood = finalList.Sum(x => x.Projected) * 80 / 100,
                        Last12MonthsSales = GetLast12MonthsSalesData("All"),
                        KPIsData = KPIsData.Where(x => x.Country == "All").FirstOrDefault()
                    });
                    _salesDashboardData = resultList;
                }

                return _salesDashboardData;
            }
        }
        private IEnumerable<Last12MonthsSales> GetLast12MonthsSalesData(string country)
        {
            var resultList = new List<Last12MonthsSales>();
            if (country == "All")
            {
                var groupBySales = Sales.GroupBy(t => new { Month = t.Month, Year = t.Year })
                .Select(t =>
                new
                {
                    Country = "All",
                    Month = t.Key.Month,
                    Year = t.Key.Year,
                    TotalSales = t.Sum(tc => tc.Sale)
                })
                .OrderByDescending(x => x.Year).ThenByDescending(x => x.Month)
                .Take(12).OrderBy(x => x.Year).ThenBy(x => x.Month)
                .Select(x =>
                new
                {
                    Country = x.Country,
                    MonthName = x.Month.ToString() + "/" + x.Year.ToString(),
                    Sales = x.TotalSales
                });
                for (int rCount = 0; rCount < groupBySales.Count(); rCount++)
                {
                    resultList.Add(new Last12MonthsSales
                    {
                        Country = groupBySales.ToList()[rCount].Country,
                        MonthName = groupBySales.ToList()[rCount].MonthName,
                        Sales = groupBySales.ToList()[rCount].Sales
                    });
                }
            }
            else
            {
                var groupBySales = Sales.GroupBy(t => new { Country = t.Country, Month = t.Month, Year = t.Year })
                .Select(t =>
                new
                {
                    Country = t.Key.Country,
                    Month = t.Key.Month,
                    Year = t.Key.Year,
                    TotalSales = t.Sum(tc => tc.Sale)
                })
                .Where(x => x.Country == country)
                .OrderByDescending(x => x.Year).ThenByDescending(x => x.Month)
                .Take(12).OrderBy(x => x.Year).ThenBy(x => x.Month)
                .Select(x =>
                new
                {
                    Country = x.Country,
                    MonthName = x.Month.ToString() + "/" + x.Year.ToString(),
                    Sales = x.TotalSales
                });
                for (int rCount = 0; rCount < groupBySales.Count(); rCount++)
                {
                    resultList.Add(new Last12MonthsSales
                    {
                        Country = groupBySales.ToList()[rCount].Country,
                        MonthName = groupBySales.ToList()[rCount].MonthName,
                        Sales = groupBySales.ToList()[rCount].Sales
                    });
                }
            }
            return resultList;
        }

    }

    public class BaseData
    {
        public static string[] countries = { "US", "Germany", "UK", "Japan", "China", "India" };
        public static int[] years = { DateTime.Today.Year - 5, DateTime.Today.Year - 4, DateTime.Today.Year - 3, DateTime.Today.Year - 2, DateTime.Today.Year - 1, DateTime.Today.Year };


        public static IEnumerable<ProjectedRevenue> GenerateProjectedRevenue()
        {
            var rand = new Random(0);
            List<ProjectedRevenue> List = new List<ProjectedRevenue>();
            for (int yy = 0; yy < years.Length; yy++)
            {
                var list = countries.Select((country, id) =>
                {
                    var projected = rand.Next(500, 1000) * rand.Next(5000, 20000);
                    return new ProjectedRevenue
                    {
                        ID = id + 1,
                        Year = years[yy],
                        Country = country,
                        Projected = projected
                    };
                });
                List.AddRange(list);
            }
            return List;
        }

        public static IEnumerable<BudgetedCost> GenerateBudgetedCost()
        {
            var rand = new Random(0);
            List<BudgetedCost> List = new List<BudgetedCost>();
            for (int yy = 0; yy < years.Length; yy++)
            {
                var list = countries.Select((country, id) =>
                {
                    double budget = rand.Next(250, 3000) * rand.Next(500, 100000) * 36500000;
                    if (budget < 0)
                        budget = budget * (-1);
                    return new BudgetedCost
                    {
                        ID = id + 1,
                        Year = years[yy],
                        Country = country,
                        Budget = budget
                    };
                });
                List.AddRange(list);
            }
            return List;
        }

        public static IEnumerable<ProjectedSales> GenerateProjectedSales()
        {
            var rand = new Random(0);
            List<ProjectedSales> List = new List<ProjectedSales>();
            for (int yy = 0; yy < years.Length; yy++)
            {
                var list = countries.Select((country, id) =>
                {
                    var sales = rand.Next(700, 2000) * 15000;
                    return new ProjectedSales
                    {
                        ID = id + 1,
                        Year = years[yy],
                        Country = country,
                        Sales = sales
                    };
                });
                List.AddRange(list);
            }
            return List;
        }

        public static IEnumerable<Expenses> GenerateExpenses()
        {
            var rand = new Random(0);
            List<Expenses> List = new List<Expenses>();
            DateTime MinDate = DateTime.Today.AddMonths(-12), MaxDate = DateTime.Today.AddDays(-1);
            while (MinDate <= MaxDate)
            {
                var list = countries.Select((country, id) =>
                {
                    var expense = rand.Next(250, 3000) * rand.Next(500, 100000);
                    return new Expenses
                    {
                        ID = id + 1,
                        TxnDate = MinDate,
                        Month = MinDate.Month,
                        Year = MinDate.Year,
                        Country = country,
                        Expense = expense
                    };
                });
                List.AddRange(list);
                MinDate = MinDate.AddDays(1);
            }
            return List;
        }

        public static IEnumerable<Sales> GenerateSales()
        {
            var rand = new Random(0);
            List<Sales> List = new List<Sales>();
            DateTime MinDate = DateTime.Today.AddMonths(-12), MaxDate = DateTime.Today.AddDays(-1);
            while (MinDate <= MaxDate)
            {
                var list = countries.Select((country, id) =>
                {
                    var sale = rand.Next(500, 5000) * rand.Next(200, 150000);
                    return new Sales
                    {
                        ID = id + 1,
                        TxnDate = MinDate,
                        Month = MinDate.Month,
                        Year = MinDate.Year,
                        Country = country,
                        Sale = sale
                    };
                });
                List.AddRange(list);
                MinDate = MinDate.AddDays(1);
            }
            return List;
        }
    }

    public class ProjectedRevenue
    {
        public int ID { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public double Projected { get; set; }
    }

    public class BudgetedCost
    {
        public int ID { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public double Budget { get; set; }
    }

    public class ProjectedSales
    {
        public int ID { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public double Sales { get; set; }
    }

    public class Expenses
    {
        public int ID { get; set; }
        public DateTime TxnDate { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public double Expense { get; set; }
    }

    public class Sales
    {
        public int ID { get; set; }
        public DateTime TxnDate { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public double Sale { get; set; }
    }

    public class CostBudgeting
    {
        public string Country { get; set; }
        public int Year { get; set; }
        public double Expenses { get; set; }
        public double Budget { get; set; }
    }

    public class MonthlySales
    {
        public string Country { get; set; }
        public string MonthName { get; set; }
        public DateTime TxnDate { get; set; }
        public double Sales { get; set; }
        public double Expenses { get; set; }
    }

    public class MonthlyProfit
    {
        public string Country { get; set; }
        public string MonthName { get; set; }
        public double Sales { get; set; }
        public double Expenses { get; set; }
        public double Profit { get; set; }
    }

    public class CountrywiseSales
    {
        public int Year { get; set; }
        public string Country { get; set; }
        public double Sales { get; set; }
        public double Expenses { get; set; }
    }

    public class Last12MonthsSales
    {
        public string Country { get; set; }
        public string MonthName { get; set; }
        public double Sales { get; set; }
    }

    public class SalesDashboard
    {

        public int Year { get; set; }
        public string Country { get; set; }
        public double Sales { get; set; }
        public double Projected { get; set; }
        public double BulletGBad { get; set; }
        public double BulletGTarget { get; set; }
        public double BulletGGood { get; set; }
        public double BulletGMax { get; set; }
        public double BulletGValue { get; set; }
        public IEnumerable<Last12MonthsSales> Last12MonthsSales { get; set; }
        public KPIs KPIsData { get; set; }
    }

    public class KPIs
    {
        public string Country { get; set; }
        public double Sales { get; set; }
        public double Expenses { get; set; }
        public double Profit { get; set; }
        public double Max { get; set; }
    }
    public class BudgetEvent
    {
        public string Country { get; set; }
        public string Title { get; set; }
    }
}