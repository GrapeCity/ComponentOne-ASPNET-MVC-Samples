using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DynamicDashboard.Models;

namespace DynamicDashboard.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Dashboard DashboardModel = new Dashboard();
            DataManager DMan = new DataManager();
            DashboardModel.Settings = DMan.CreateSettings();
            DashboardModel.CostBudgetingData = DMan.GetCostBudgetingData();
            DashboardModel.CostBudgetingChartType = "Column";
            DashboardModel.CostBudgetingStr = (DateTime.Today.Year).ToString();
            DashboardModel.CountrywiseSalesData = DMan.GetCountrywiseSalesData();
            DashboardModel.CountrywiseSalesChartType = "Bar";
            DashboardModel.CountrywiseSalesStr = DashboardModel.CountrywiseSalesData.FirstOrDefault().Year.ToString();
            DashboardModel.MonthlySalesData = DMan.GetMonthlySalesData();
            DashboardModel.MonthlySalesChartType = "Line";
            DashboardModel.MonthlySalesChartTrendLine = "AverageY";
            DashboardModel.MonthlySalesStr = DashboardModel.MonthlySalesData.LastOrDefault().MonthName.ToString() + " - " + DashboardModel.MonthlySalesData.FirstOrDefault().MonthName.ToString();
            DashboardModel.MonthlyProfitData = DMan.GetMonthlyProfitData();
            DashboardModel.MonthlyProfitChartType = "Column";
            DashboardModel.MonthlyProfitStr = DashboardModel.MonthlyProfitData.FirstOrDefault().MonthName.ToString() + " - " + DashboardModel.MonthlyProfitData.LastOrDefault().MonthName.ToString();
            DashboardModel.SalesDashboardData = DMan.GetSalesDashboardData();
            DashboardModel.SalesDashboardStr = DashboardModel.SalesDashboardData.FirstOrDefault().Year.ToString();
            DashboardModel.KPIsData = DMan.GetKPIsData();
            DashboardModel.KPIsStr = DateTime.Today.Year.ToString();

            return View(DashboardModel);
        }

    }
}
