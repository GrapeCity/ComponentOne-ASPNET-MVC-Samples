using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C1.Web.Mvc.Chart;

namespace DynamicDashboard.Models
{
    public class DataManager
    {

        public static IEnumerable<ProjectedRevenue> ProjectedRevenue = BaseData.GenerateProjectedRevenue();
        public static IEnumerable<BudgetedCost> BudgetedCost = BaseData.GenerateBudgetedCost();
        public static IEnumerable<ProjectedSales> ProjectedSales = BaseData.GenerateProjectedSales();
        public static IEnumerable<Expenses> Expenses = BaseData.GenerateExpenses();
        public static IEnumerable<Sales> Sales = BaseData.GenerateSales();

        public DataManager()
        {

        }

        public IDictionary<string, object[]> CreateSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"ChartType", new object[]{"Column", "Bar", "Scatter", "Line", "LineSymbols", "Area", "Spline", "SplineSymbols", "SplineArea"}},
                {"TrendLine", new object[]{ "AverageX", "AverageY", "Exponential", "Fourier", "Linear", "Logarithmic", "MaxX", "MaxY", "MinX", "MinY", "Polynomial", "Power"}}
            };
            return settings;
        }

        /// <summary>
        /// To create the list of CostBudgeting class object for various countries
        /// </summary>
        /// <returns>return the list of CostBudgeting class objects</returns>
        public IEnumerable<CostBudgeting> GetCostBudgetingData()
        {
            List<CostBudgeting> ResultList = new List<CostBudgeting>();
            var GroupByExpenses = Expenses.GroupBy(t => new { Country = t.Country, Year = t.TxnDate.Year })
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
                ResultList.Add(new CostBudgeting
                {
                    Country = temp.ToList()[RCount].Country,
                    Year = temp.ToList()[RCount].Year,
                    Expenses = temp.ToList()[RCount].Expenses,
                    Budget = temp.ToList()[RCount].Budget
                });
            }
            return ResultList;
        }

        /// <summary>
        /// To create the list of MonthlySales class object for various countries
        /// </summary>
        /// <returns>return the list of MonthlySales class objects</returns>
        public IEnumerable<MonthlySales> GetMonthlySalesData()
        {
            List<MonthlySales> ResultList = new List<MonthlySales>();
            IEnumerable<Sales> Sales = DataManager.Sales;
            IEnumerable<Expenses> Expenses = DataManager.Expenses;
            DateTime MinDate = DateTime.Today.AddMonths(-4), MaxDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1);
            var GroupBySales = Sales.Where(x => x.TxnDate >= MinDate && x.TxnDate <= MaxDate)
                .GroupBy(t => new { TxnDate = t.TxnDate,Country=t.Country })
                .Select(t =>
                new
                {
                    Country=t.Key.Country,
                    TxnDate = t.Key.TxnDate,
                    Month = t.Key.TxnDate.Month,
                    Year = t.Key.TxnDate.Year,
                    TotalSales = t.Sum(tc => tc.Sale)
                }).OrderByDescending(x => x.TxnDate)
                .Select(x =>
                new
                {
                    Country=x.Country,
                    TxnDate = x.TxnDate,
                    MonthName = x.Month.ToString() + "/" + x.Year.ToString(),
                    Sales = x.TotalSales
                });

            var GroupByExpenses = Expenses.Where(x => x.TxnDate >= MinDate && x.TxnDate <= MaxDate)
                .GroupBy(t => new { TxnDate = t.TxnDate, Country = t.Country })
                .Select(t =>
                new
                {
                    Country=t.Key.Country,
                    TxnDate = t.Key.TxnDate,
                    Month = t.Key.TxnDate.Month,
                    Year = t.Key.TxnDate.Year,
                    TotalExpenses = t.Sum(tc => tc.Expense)
                }).OrderByDescending(x => x.TxnDate)
                .Select(x =>
                new
                {
                    Country=x.Country,
                    TxnDate = x.TxnDate,
                    MonthName = x.Month.ToString() + "/" + x.Year.ToString(),
                    Expenses = x.TotalExpenses
                });

            var FinalList = GroupBySales
                .Join(GroupByExpenses, o => o.TxnDate + o.Country, i => i.TxnDate + i.Country, (o, i) =>
                    new
                    {
                        Country = o.Country,
                        TxnDate = o.TxnDate,
                        MonthName = o.MonthName,
                        Sales = o.Sales,
                        Expenses = i.Expenses
                    });
            foreach(var item in FinalList)
            {
                MonthlySales temp = new MonthlySales();
                temp.Country = item.Country;
                temp.TxnDate = item.TxnDate;
                temp.MonthName = item.MonthName;
                temp.Sales = item.Sales;
                temp.Expenses = item.Expenses;
                ResultList.Add(temp);
            }

            var MonthNames = ResultList.GroupBy(p => p.TxnDate)
                .Select(g => g.First()).ToList();
            for (int Mcount = 0; Mcount < MonthNames.Count(); Mcount++)
            {
                ResultList.Add(new MonthlySales
                {
                    Country = "All",
                    TxnDate = MonthNames[Mcount].TxnDate,
                    MonthName = MonthNames[Mcount].MonthName,
                    Sales = ResultList.Where(x => x.TxnDate == MonthNames[Mcount].TxnDate).Sum(x => x.Sales),
                    Expenses = ResultList.Where(x => x.TxnDate == MonthNames[Mcount].TxnDate).Sum(x => x.Expenses)
                });
            }
            return ResultList;
        }

        /// <summary>
        /// To create the list of MonthlyProfit class object for various countries
        /// </summary>
        /// <returns>return the list of MonthlyProfit class objects</returns>
        public IEnumerable<MonthlyProfit> GetMonthlyProfitData()
        {
            List<MonthlyProfit> ResultList = new List<MonthlyProfit>();
            IEnumerable<Sales> Sales = DataManager.Sales;
            IEnumerable<Expenses> Expenses = DataManager.Expenses;
            var GroupBySales = Sales.GroupBy(t => new { Country = t.Country, Month = t.Month, Year = t.Year })
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

            var GroupByExpenses = Expenses.GroupBy(t => new { Country = t.Country, Month = t.Month, Year = t.Year })
                .Select(t =>
                new
                {
                    Country = t.Key.Country,
                    Month = t.Key.Month,
                    Year = t.Key.Year,
                    TotalExpenses = t.Sum(tc => tc.Expense)
                }).OrderByDescending(x => x.Year).ThenByDescending(x => x.Month)
                .Take(BaseData.countries.Count()*12).OrderBy(x => x.Year).ThenBy(x => x.Month)
                .Select(x =>
                new
                {
                    Country = x.Country,
                    MonthName = x.Month.ToString() + "/" + x.Year.ToString(),
                    Expenses = x.TotalExpenses
                });
            var FinalList = GroupBySales
                .Join(GroupByExpenses, o => (o.MonthName + o.Country), i => (i.MonthName + i.Country), (o, i) =>
                           new
                           {
                               Country = o.Country,
                               MonthName = o.MonthName,
                               Sales = o.Sales,
                               Expenses = i.Expenses,
                               Profit = o.Sales - i.Expenses
                           });
            for (int RCount = 0; RCount < FinalList.Count(); RCount++)
            {
                ResultList.Add(new MonthlyProfit
                {
                    Country = FinalList.ToList()[RCount].Country,
                    MonthName = FinalList.ToList()[RCount].MonthName,
                    Sales = FinalList.ToList()[RCount].Sales,
                    Expenses = FinalList.ToList()[RCount].Expenses,
                    Profit = FinalList.ToList()[RCount].Profit
                });
            }
            var MonthNames = ResultList.GroupBy(p => p.MonthName)
                .Select(g => g.First().MonthName).ToList();
            for (int Mcount = 0; Mcount < MonthNames.Count(); Mcount++)
            {
                ResultList.Add(new MonthlyProfit
                {
                    Country = "All",
                    MonthName = MonthNames[Mcount].ToString(),
                    Sales = ResultList.Where(x => x.MonthName == MonthNames[Mcount].ToString()).Sum(x => x.Sales),
                    Expenses = ResultList.Where(x => x.MonthName == MonthNames[Mcount].ToString()).Sum(x => x.Expenses),
                    Profit = ResultList.Where(x => x.MonthName == MonthNames[Mcount].ToString()).Sum(x => x.Profit)
                });
            }
            return ResultList;
        }

        /// <summary>
        /// To create the list of CountrywiseSales class object for various countries
        /// </summary>
        /// <returns>return the list of CountrywiseSales class objects</returns>
        public IEnumerable<CountrywiseSales> GetCountrywiseSalesData()
        {

            List<CountrywiseSales> ResultList = new List<CountrywiseSales>();
            IEnumerable<Sales> Sales = DataManager.Sales;
            IEnumerable<Expenses> Expenses = DataManager.Expenses;
            var GroupBySales = Sales.GroupBy(t => new { Year = t.Year, Country = t.Country })
                .Select(t =>
                new
                {
                    Year = t.Key.Year,
                    Country = t.Key.Country,
                    TotalSales = t.Sum(tc => tc.Sale)
                }).Where(x => x.Year == DateTime.Today.Year);

            var GroupByExpenses = Expenses.GroupBy(t => new { Year = t.Year, Country = t.Country })
            .Select(t =>
            new
            {
                Year = t.Key.Year,
                Country = t.Key.Country,
                TotalExpenses = t.Sum(tc => tc.Expense)
            }).Where(x => x.Year == DateTime.Today.Year);


            var FinalList = GroupBySales
                .Join(GroupByExpenses, o => o.Year + o.Country, i => i.Year + i.Country, (o, i) =>
                           new
                           {
                               Year = o.Year,
                               Country = o.Country,
                               Sales = o.TotalSales,
                               Expenses = i.TotalExpenses
                           });
            for (int RCount = 0; RCount < FinalList.Count(); RCount++)
            {
                ResultList.Add(new CountrywiseSales
                {
                    Year = FinalList.ToList()[RCount].Year,
                    Country = FinalList.ToList()[RCount].Country,
                    Sales = FinalList.ToList()[RCount].Sales,
                    Expenses = FinalList.ToList()[RCount].Expenses
                });
            }
            return ResultList;
        }

        /// <summary>
        /// To create the list of Last12MonthsSales class object for selected country
        /// </summary>
        /// <returns>return the list of Last12MonthsSales class objects</returns>
        public IEnumerable<Last12MonthsSales> GetLast12MonthsSalesData(string Country)
        {
            List<Last12MonthsSales> ResultList = new List<Last12MonthsSales>();
            IEnumerable<Sales> Sales = DataManager.Sales;
            if (Country == "All")
            {
                var GroupBySales = Sales.GroupBy(t => new { Month = t.Month, Year = t.Year })
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
                for (int RCount = 0; RCount < GroupBySales.Count(); RCount++)
                {
                    ResultList.Add(new Last12MonthsSales
                    {
                        Country = GroupBySales.ToList()[RCount].Country,
                        MonthName = GroupBySales.ToList()[RCount].MonthName,
                        Sales = GroupBySales.ToList()[RCount].Sales
                    });
                }
            }
            else
            {
                var GroupBySales = Sales.GroupBy(t => new { Country = t.Country, Month = t.Month, Year = t.Year })
                .Select(t =>
                new
                {
                    Country = t.Key.Country,
                    Month = t.Key.Month,
                    Year = t.Key.Year,
                    TotalSales = t.Sum(tc => tc.Sale)
                })
                .Where(x => x.Country == Country)
                .OrderByDescending(x => x.Year).ThenByDescending(x => x.Month)
                .Take(12).OrderBy(x => x.Year).ThenBy(x => x.Month)
                .Select(x =>
                new
                {
                    Country = x.Country,
                    MonthName = x.Month.ToString() + "/" + x.Year.ToString(),
                    Sales = x.TotalSales
                });
                for (int RCount = 0; RCount < GroupBySales.Count(); RCount++)
                {
                    ResultList.Add(new Last12MonthsSales
                    {
                        Country = GroupBySales.ToList()[RCount].Country,
                        MonthName = GroupBySales.ToList()[RCount].MonthName,
                        Sales = GroupBySales.ToList()[RCount].Sales
                    });
                }
            }
            return ResultList;
        }

        /// <summary>
        /// To create the list of SalesDashboard class object for various countries
        /// </summary>
        /// <returns>return the list of SalesDashboard class objects</returns>
        public IEnumerable<SalesDashboard> GetSalesDashboardData()
        {

            List<SalesDashboard> ResultList = new List<SalesDashboard>();
            IEnumerable<ProjectedSales> ProjectedSales = DataManager.ProjectedSales;
            IEnumerable<Sales> Sales = DataManager.Sales;
            IEnumerable<Expenses> Expenses = DataManager.Expenses;
            var KPIsDataList = GetKPIsData();
            var ActualSalesList = Sales.GroupBy(t => new { Year = t.Year, Country = t.Country })
                .Select(t =>
                new
                {
                    Year = t.Key.Year,
                    Country = t.Key.Country,
                    TotalSales = t.Sum(tc => tc.Sale)
                }).Where(x => x.Year == DateTime.Today.Year);

            var ProjectedSalesList = BaseData.GenerateProjectedSales().Where(x => x.Year == DateTime.Today.Year)
                .Select(x =>
                new
                {
                    Country = x.Country,
                    Year = x.Year,
                    ProjectedSales = x.Sales,
                    Max = ProjectedSales.Max(s => s.Sales)
                });

            var FinalList = ActualSalesList
                .Join(ProjectedSalesList, o => o.Year + o.Country, i => i.Year + i.Country, (o, i) =>
                new
                {
                    Year = o.Year,
                    Country = o.Country,
                    Sales = o.TotalSales,
                    Projected = i.ProjectedSales,
                    Max = i.Max
                }).ToList();
            for (int RCount = 0; RCount < FinalList.Count(); RCount++)
            {
                ResultList.Add(new SalesDashboard
                {
                    Year = FinalList.ToList()[RCount].Year,
                    Country = FinalList.ToList()[RCount].Country,
                    Sales = FinalList.ToList()[RCount].Sales,
                    Projected = FinalList.ToList()[RCount].Projected,
                    BulletGValue = FinalList.ToList()[RCount].Sales,
                    BulletGTarget = FinalList.ToList()[RCount].Projected,
                    BulletGMax = FinalList.ToList()[RCount].Max,
                    BulletGBad = FinalList.ToList()[RCount].Projected / 2,
                    BulletGGood = FinalList.ToList()[RCount].Projected * 80 / 100,
                    Last12MonthsSales = GetLast12MonthsSalesData(FinalList.ToList()[RCount].Country),
                    KPIsData= KPIsDataList.Where(x=>x.Country== FinalList.ToList()[RCount].Country).FirstOrDefault()
                });
            }
            ResultList.Insert(0, new SalesDashboard
            {
                Year = FinalList.ToList()[0].Year,
                Country = "All",
                Sales = FinalList.Sum(x => x.Sales),
                Projected = FinalList.Sum(x => x.Projected),
                BulletGValue = FinalList.Sum(x => x.Sales),
                BulletGTarget = FinalList.Sum(x => x.Projected),
                BulletGMax = FinalList.Sum(x => x.Max),
                BulletGBad = FinalList.Sum(x => x.Projected) / 2,
                BulletGGood = FinalList.Sum(x => x.Projected) * 80 / 100,
                Last12MonthsSales = GetLast12MonthsSalesData("All"),
                KPIsData = KPIsDataList.Where(x => x.Country == "All").FirstOrDefault()
            });
            return ResultList;
        }

        /// <summary>
        /// To create the list of KPIs class object for various countries
        /// </summary>
        /// <returns>return the list of KPIs class objects</returns>
        public IEnumerable<KPIs> GetKPIsData()
        {
            List<KPIs> Result = new List<KPIs>();
            IEnumerable<Sales> Sales = DataManager.Sales;
            IEnumerable<Expenses> Expenses = DataManager.Expenses;
            var SalesListByCountry = Sales.GroupBy(t => new { Year = t.Year, Country = t.Country })
                .Select(t =>
                new
                {
                    Year = t.Key.Year,
                    Country = t.Key.Country,
                    TotalSales = t.Sum(tc => tc.Sale)
                }).Where(x => x.Year == DateTime.Today.Year);

            var ExpensesListByCountry = Expenses.GroupBy(t => new { Year = t.Year, Country = t.Country })
                .Select(t =>
                new
                {
                    Year = t.Key.Year,
                    Country = t.Key.Country,
                    TotalExpenses = t.Sum(tc => tc.Expense)
                }).Where(x => x.Year == DateTime.Today.Year);

            var FinalList = SalesListByCountry
                .Join(ExpensesListByCountry, o => o.Year + o.Country, i => i.Year + i.Country, (o, i) =>
                new
                {
                    Year = o.Year,
                    Country = o.Country,
                    Sales = o.TotalSales,
                    Expenses = i.TotalExpenses,
                    Profit = o.TotalSales - i.TotalExpenses,
                    Max = o.TotalSales > i.TotalExpenses ? o.TotalSales : i.TotalExpenses
                }).ToList();

            for (int RCount = 0; RCount < FinalList.Count; RCount++)
            {
                Result.Add(new KPIs
                {
                    Country = FinalList[RCount].Country,
                    Expenses = FinalList[RCount].Expenses,
                    Max = FinalList[RCount].Max,
                    Profit = FinalList[RCount].Profit,
                    Sales = FinalList[RCount].Sales
                });
            }
            Result.Add(new KPIs
            {
                Country = "All",
                Expenses = FinalList.Sum(x => x.Expenses),
                Max = FinalList.Max(x => x.Max),
                Profit = FinalList.Sum(x => x.Profit),
                Sales = FinalList.Sum(x => x.Sales)
            });
            return Result;
        }
    }
}