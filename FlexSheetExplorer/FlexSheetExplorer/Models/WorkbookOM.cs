using C1.C1Excel;
using C1.Web.Mvc.Sheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexSheetExplorer.Models
{
    public static class WorkbookOM
    {
        public static Workbook GetWorkbook()
        {
            C1XLBook xlBook = new C1XLBook();
            xlBook.Load(AppDomain.CurrentDomain.BaseDirectory + "Content\\xlsxFile\\example.xlsx");
            return xlBook.ToWorkbook();
        }
    }
}