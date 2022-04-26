using C1.C1Excel;
using MvcExplorer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcExplorer.Controllers.Excel
{
    public partial class ExcelController : Controller
    {
        //
        // GET: /Excel/
        C1XLBook _xlBook = new C1XLBook();
        public ActionResult LoadingWorkSheets()
        {
            try
            {
                _xlBook.Load(AppDomain.CurrentDomain.BaseDirectory + "Models\\Houston.xlsx");
            }
            catch (Exception)
            {
                Response.Write("Unable to load Excel file: Houston.xlsx");
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
