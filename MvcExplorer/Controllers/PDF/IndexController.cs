using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using C1.C1Pdf;
using System.Drawing;
using MvcExplorer.Models;
using System.IO;

namespace MvcExplorer.Controllers.PDF
{
    public partial class PDFController : Controller
    {
        //
        // GET: /Index/

        public ActionResult Index()
        {
            return View();
        }

        private readonly C1NWindEntities _dataBase = new C1NWindEntities();
        private C1PdfDocument _c1Pdf;

        public ActionResult CreatePdf()
        {
            try
            {
                InitialPdfDocument();
                var stream = new MemoryStream();
                _c1Pdf.Save(stream);
                stream.Position = 0;
                return File(stream, "application/pdf");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        private readonly Font _fontBody = new Font("Tahoma", 10);
        private readonly Font _fontTitle = new Font("Tahoma", 15, FontStyle.Bold);
        private readonly Font _fontHeader = new Font("Tahoma", 11, FontStyle.Bold);
        private StringFormat _sfRight;
        private StringFormat _sfRightCenter;
        
        private static string _tempDir;

        public void InitialPdfDocument()
        {
            _c1Pdf = new C1PdfDocument();
            //create StringFormat for right-aligned fields
            _sfRight = new StringFormat();
            _sfRight.Alignment = StringAlignment.Far;
            _sfRightCenter = new StringFormat();
            _sfRightCenter.Alignment = StringAlignment.Far;
            _sfRightCenter.LineAlignment = StringAlignment.Center;

            //initialize pdf generator
            _c1Pdf.Clear();

            //get page rectangle, discount margins
            RectangleF rcPage = _c1Pdf.PageRectangle;
            rcPage.Inflate(-72, -92);

            //loop through selected categories
            int page = 0;
            //DataTable dt = GetCategories();
            List<Category> categories = _dataBase.Categories.ToList();

            foreach (Category category in categories)
            {
                //add page break, update page counter
                if (page > 0) _c1Pdf.NewPage();
                page++;

                //get current category name
                string catName = category.CategoryName;

                //add title to page
                _c1Pdf.DrawString(catName, _fontTitle, Brushes.Blue, rcPage);

                //add outline entry
                _c1Pdf.AddBookmark(catName, 0, 0);

                //build row template
                RectangleF[] rcRows = new RectangleF[6];
                for (int i = 0; i < rcRows.Length; i++)
                {
                    rcRows[i] = RectangleF.Empty;
                    rcRows[i].Location = new PointF(rcPage.X, rcPage.Y + _fontHeader.SizeInPoints + 10);
                    rcRows[i].Size = new SizeF(0, _fontBody.SizeInPoints + 3);
                }
                rcRows[0].Width = 110;		// Product Name
                rcRows[1].Width = 60;		// Unit Price
                rcRows[2].Width = 80;		// Qty/Unit
                rcRows[3].Width = 60;		// Stock Units
                rcRows[4].Width = 60;		// Stock Value
                rcRows[5].Width = 60;		// Reorder
                for (int i = 1; i < rcRows.Length; i++)
                    rcRows[i].X = rcRows[i - 1].X + rcRows[i - 1].Width + 8;

                //add column headers
                _c1Pdf.FillRectangle(Brushes.DarkGray, RectangleF.Union(rcRows[0], rcRows[5]));
                _c1Pdf.DrawString("Product Name", _fontHeader, Brushes.White, rcRows[0]);
                _c1Pdf.DrawString("Unit Price", _fontHeader, Brushes.White, rcRows[1], _sfRight);
                _c1Pdf.DrawString("Qty/Unit", _fontHeader, Brushes.White, rcRows[2]);
                _c1Pdf.DrawString("Stock Units", _fontHeader, Brushes.White, rcRows[3], _sfRight);
                _c1Pdf.DrawString("Stock Value", _fontHeader, Brushes.White, rcRows[4], _sfRight);
                _c1Pdf.DrawString("Reorder", _fontHeader, Brushes.White, rcRows[5]);

                //loop through products in this category
                List<Product> products = _dataBase.Products.Where(pro => pro.CategoryID == category.CategoryID).ToList();

                foreach (Product product in products)
                {
                    //move on to next row
                    for (int i = 0; i < rcRows.Length; i++)
                        rcRows[i].Y += rcRows[i].Height;

                    //add row with some data
                    try
                    {
                        _c1Pdf.DrawString(product.ProductName, _fontBody, Brushes.Black, rcRows[0]);
                        _c1Pdf.DrawString(string.Format("{0:c}", product.UnitPrice), _fontBody, Brushes.Black, rcRows[1], _sfRight);
                        _c1Pdf.DrawString(string.Format("{0}", product.QuantityPerUnit), _fontBody, Brushes.Black, rcRows[2]);
                        _c1Pdf.DrawString(string.Format("{0}", product.UnitsInStock), _fontBody, Brushes.Black, rcRows[3], _sfRight);
                        _c1Pdf.DrawString(string.Format("{0:c}", product.UnitPrice * product.UnitsInStock), _fontBody, Brushes.Black, rcRows[4], _sfRight);
                        if (product.UnitsInStock <= product.ReorderLevel)
                            _c1Pdf.DrawString("<<<", _fontBody, Brushes.Red, rcRows[5]);
                    }
                    catch
                    {
                        // Debug.Assert(false);
                    }
                }
                if (products.Count == 0)
                {
                    rcRows[0].Y += rcRows[0].Height;
                    _c1Pdf.DrawString("No products in this category.", _fontBody, Brushes.Black,
                                      RectangleF.Union(rcRows[0], rcRows[5]));
                }
            }

            //add page headers
            AddPageHeaders(rcPage);
        }

        private void AddPageHeaders(RectangleF rcPage)
        {
            RectangleF rcHdr = rcPage;
            rcHdr.Y = 10;
            rcHdr.Height = rcPage.Top - 10;
            for (int page = 0; page < _c1Pdf.Pages.Count; page++)
            {
                //reopen each page
                _c1Pdf.CurrentPage = page;

                //draw letterhead
                string s = string.Format("Page {0} of {1}", page + 1, _c1Pdf.Pages.Count);
                _c1Pdf.DrawString(s, _fontBody, Brushes.LightGray, rcHdr, _sfRightCenter);
                rcHdr.Inflate(0, -30);
                _c1Pdf.DrawRectangle(Pens.LightGray, rcHdr);
                rcHdr.Inflate(0, +30);
            }
        }

    }
}
