using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicDashboard.Models
{
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
                    double budget = rand.Next(250, 3000) * rand.Next(500, 100000)*36500000;
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
        public KPIs KPIsData {get;set;}
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