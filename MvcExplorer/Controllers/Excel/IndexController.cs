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

        public ActionResult Index()
        {
            return View();
        }

        private C1NWindEntities db = new C1NWindEntities();

        XLStyle _styTitle;
        XLStyle _styHeader;
        XLStyle _styMoney;
        XLStyle _styOrder;
        C1XLBook _c1xl = new C1.C1Excel.C1XLBook();

        protected static string TEMP_DIR;

        public ActionResult GenerateExcel()
        {
            TEMP_DIR = Server.MapPath("~") + "\\Temp";
            if (Directory.Exists(TEMP_DIR))
            {

            }
            else
            {
                Directory.CreateDirectory(TEMP_DIR);

            }

            string file = CreateExcelFile();

            try
            {
                Response.Clear();
                Response.Charset = "UTF-8";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                var filename = "Excel.xls";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                Response.ContentType = "application/ms-excel";
                Response.TransmitFile(file);
                Response.Flush();
                System.IO.File.Delete(file);
                Response.End();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            
            return View();
        }

        private string CreateExcelFile()
        {
            //clear Excel book, remove the single blank sheet
            _c1xl.Clear();
            _c1xl.Sheets.Clear();
            _c1xl.DefaultFont = new Font("Tahoma", 8);

            //create Excel styles
            _styTitle = new XLStyle(_c1xl);
            _styHeader = new XLStyle(_c1xl);
            _styMoney = new XLStyle(_c1xl);
            _styOrder = new XLStyle(_c1xl);

            //set up styles
            _styTitle.Font = new Font(_c1xl.DefaultFont.Name, 15, FontStyle.Bold);
            _styTitle.ForeColor = Color.Blue;
            _styHeader.Font = new Font(_c1xl.DefaultFont, FontStyle.Bold);
            _styHeader.ForeColor = Color.White;
            _styHeader.BackColor = Color.DarkGray;
            _styMoney.Format = XLStyle.FormatDotNetToXL("c");
            _styOrder.Font = _styHeader.Font;
            _styOrder.ForeColor = Color.Red;

            //create report with one sheet per category
            List<Category> categories = db.Categories.ToList<Category>();
            foreach (Category category in categories)
            {
                CreateSheet(category);
            }

            //save xls file
            string uid = System.Guid.NewGuid().ToString();
            string file = Server.MapPath("~") + "\\Temp\\testexcel" + uid + ".xls";
            _c1xl.Save(file);

            return file;

        }

        private void CreateSheet(Category category)
        {
            //get current category name
            string catName = category.CategoryName;

            //add a new worksheet to the workbook 
            //('/' is invalid in sheet names, so replace it with '+')
            string sheetName = catName.Replace("/", " + ");
            XLSheet sheet = _c1xl.Sheets.Add(sheetName);

            //add title to worksheet
            sheet[0, 0].Value = catName;
            sheet.Rows[0].Style = _styTitle;

            // set column widths (in twips)
            sheet.Columns[0].Width = 300;
            sheet.Columns[1].Width = 2200;
            sheet.Columns[2].Width = 1000;
            sheet.Columns[3].Width = 1600;
            sheet.Columns[4].Width = 1000;
            sheet.Columns[5].Width = 1000;
            sheet.Columns[6].Width = 1000;

            //add column headers
            int row = 2;
            sheet.Rows[row].Style = _styHeader;
            sheet[row, 1].Value = "Product Name";
            sheet[row, 2].Value = "Unit Price";
            sheet[row, 3].Value = "Qty/Unit";
            sheet[row, 4].Value = "Stock Units";
            sheet[row, 5].Value = "Stock Value";
            sheet[row, 6].Value = "Reorder";

            //loop through products in this category
            //DataRow[] products = category.GetChildRows("Categories_Products");
            List<Product> products = db.Products.Where(pro => pro.CategoryID == category.CategoryID).ToList<Product>();
            foreach (Product product in products)
            {
                //move on to next row
                row++;

                //add row with some data
                sheet[row, 1].Value = product.ProductName;
                sheet[row, 2].Value = product.UnitPrice;
                sheet[row, 3].Value = product.QuantityPerUnit;
                sheet[row, 4].Value = product.UnitsInStock;

                //calculate value in stock
                double valueInStock =
                                     Convert.ToDouble(product.UnitPrice) *
                                     Convert.ToInt32(product.UnitsInStock);
                sheet[row, 5].Value = valueInStock;

                //check reorder level
                if (Convert.ToInt32(product.UnitsInStock) <=
                      Convert.ToInt32(product.ReorderLevel))
                {
                    sheet[row, 6].Value = "<<<";
                    sheet[row, 6].Style = _styOrder;
                }

                //format money cells
                sheet[row, 2].Style = _styMoney;
                sheet[row, 5].Style = _styMoney;
            }
            if (products.Count == 0)
            {
                row++;
                sheet[row, 1].Value = "No products in this category";
            }
        }

    }
}
