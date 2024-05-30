using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System;
using C1.Excel;

namespace MvcExplorer.Controllers
{
    public partial class ExcelController : Controller
    {
        C1XLBook _xlBook = new C1XLBook();
        public ActionResult LoadingWorksheets()
        {
            try
            {
                _xlBook.Load(Startup.Environment.WebRootPath + "\\Temp\\Houston.xlsx");
            }
            catch (Exception)
            {
                //Response.Write("Unable to load Excel file: Houston.xlsx");
                return View();
            }

            DrillDataPoints dps = GetChartData(_xlBook);

            return View(dps.DrillDataPointSeries);
        }

        DrillDataPoints GetChartData(C1XLBook book)
        {
            // Get first sheet
            var sheet = book.Sheets[0];

            // Get location, date, and cell count
            var location = sheet[1, 1].Value as string;
            var date = (DateTime)sheet[2, 1].Value;
            var count = sheet.Rows.Count - 5;
            // label.Text = string.Format("{0}, {1} points", location, count);

            // Get values into arrays for charting
            var drillData = new DrillDataPoints(count);
            for (int r = 0; r < count; r++)
            {
                drillData.Temperature[r] = (double)sheet[r + 5, 1].Value;
                drillData.Pressure[r] = (double)sheet[r + 5, 2].Value;
                drillData.Conductivity[r] = (double)sheet[r + 5, 3].Value;
                drillData.Ph[r] = (double)sheet[r + 5, 4].Value;
                drillData.Depth[r] = r;
            }
            drillData.ScaleValues();

            // Send data to chart
            return drillData;
        }
    }
}
