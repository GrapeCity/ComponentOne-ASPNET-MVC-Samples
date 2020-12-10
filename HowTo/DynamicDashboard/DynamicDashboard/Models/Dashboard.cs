using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicDashboard.Models
{
    public class Dashboard
    {
        public IDictionary<string, object[]> Settings { get; set; }
        public string KPIsStr { get; set; }
        public IEnumerable<KPIs>  KPIsData { get; set; }
        public string CostBudgetingStr { get; set; }
        public string CostBudgetingChartType { get; set; }
        public IEnumerable<CostBudgeting> CostBudgetingData { get; set; }
        public string MonthlySalesStr { get; set; }
        public string MonthlySalesChartType { get; set; }
        public string MonthlySalesChartTrendLine { get; set; }
        public IEnumerable<MonthlySales> MonthlySalesData { get; set; }
        public string MonthlyProfitStr { get; set; }
        public string MonthlyProfitChartType { get; set; }
        public IEnumerable<MonthlyProfit> MonthlyProfitData { get; set; }
        public string CountrywiseSalesStr { get; set; }
        public string CountrywiseSalesChartType { get; set; }
        public IEnumerable<CountrywiseSales> CountrywiseSalesData { get; set; }
        public string SalesDashboardStr { get; set; }
        public IEnumerable<SalesDashboard> SalesDashboardData { get; set; }
        public Dashboard()
        {

        }
    }
}