using System.Globalization;
using C1.C1Pdf;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace MvcExplorer.Controllers.PDF
{
    public partial class PDFController : Controller
    {
        //
        // GET: /TableOfContents/

        public ActionResult TableOfContents()
        {
            return View();
        }

        private readonly Random _rnd = new Random();

        public ActionResult CreateTableOfContentsPdf()
        {
            try
            {
                InitialTableOfContentsPdfDocument();
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

        public void InitialTableOfContentsPdfDocument()
        {
            _c1Pdf = new C1PdfDocument();
            // initialize pdf generator
            _c1Pdf.Clear();
            _c1Pdf.DocumentInfo.Title = "Pdf Document With Table of Contents";
            _tempDir = Server.MapPath("../Temp");
            if (Directory.Exists(_tempDir))
            {

            }
            else
            {
                Directory.CreateDirectory(_tempDir);

            }
            // add title
            Font titleFont = new Font("Tahoma", 24, FontStyle.Bold);
            RectangleF rcPage = GetPageRect();
            RectangleF rc = RenderParagraph(_c1Pdf.DocumentInfo.Title, titleFont, rcPage, rcPage);
            rc.Y += 12;

            // create nonsense document
            ArrayList bkmk = new ArrayList();
            Font headerFont = new Font("Tahoma", 16, FontStyle.Bold);
            Font bodyFont = new Font("Tahoma", 10);
            for (int i = 0; i < 30; i++)
            {
                // create ith header (as a link target and outline entry)
                string header = string.Format("{0}. {1}", i + 1, BuildRandomTitle());
                rc = RenderParagraph(header, headerFont, rcPage, rc, true, true);

                // save bookmark to build TOC later
                int pageNumber = _c1Pdf.CurrentPage + 1;
                bkmk.Add(new [] { pageNumber.ToString(CultureInfo.InvariantCulture), header });

                // create some text
                rc.X += 36;
                rc.Width -= 36;
                for (int j = 0; j < 3 + _rnd.Next(10); j++)
                {
                    string text = BuildRandomParagraph();
                    rc = RenderParagraph(text, bodyFont, rcPage, rc);
                    rc.Y += 6;
                }
                rc.X -= 36;
                rc.Width += 36;
                rc.Y += 20;
            }

            // number pages (before adding TOC)
            AddFooters();

            // start Table of Contents
            _c1Pdf.NewPage();					// start TOC on a new page
            int tocPage = _c1Pdf.CurrentPage;	// save page index (to move TOC later)
            rc = RenderParagraph("Table of Contents", titleFont, rcPage, rcPage, true);
            rc.Y += 12;
            rc.X += 30;
            rc.Width -= 40;

            // render Table of Contents
            Pen dottedPen = new Pen(Brushes.Gray, 1.5f);
            dottedPen.DashStyle = DashStyle.Dot;
            StringFormat sfRight = new StringFormat();
            sfRight.Alignment = StringAlignment.Far;
            rc.Height = bodyFont.Height;
            foreach (string[] entry in bkmk)
            {
                // get bookmark info
                string page = entry[0];
                string header = entry[1];

                // render header name and page number
                _c1Pdf.DrawString(header, bodyFont, Brushes.Black, rc);
                _c1Pdf.DrawString(page, bodyFont, Brushes.Black, rc, sfRight);

                // connect the two with some dots (looks better than a dotted line)
                string dots = ". ";
                float wid = _c1Pdf.MeasureString(dots, bodyFont).Width;
                float x1 = rc.X + _c1Pdf.MeasureString(header, bodyFont).Width + 8;
                float x2 = rc.Right - _c1Pdf.MeasureString(page, bodyFont).Width - 8;
                float x = rc.X;
                for (rc.X = x1; rc.X < x2; rc.X += wid)
                    _c1Pdf.DrawString(dots, bodyFont, Brushes.Gray, rc);
                rc.X = x;

                // add local hyperlink to entry
                _c1Pdf.AddLink("#" + header, rc);

                // move on to next entry
                rc.Offset(0, rc.Height);
                if (rc.Bottom > rcPage.Bottom)
                {
                    _c1Pdf.NewPage();
                    rc.Y = rcPage.Y;
                }
            }

            // move table of contents to start of document
            PdfPage[] arr = new PdfPage[_c1Pdf.Pages.Count - tocPage];
            _c1Pdf.Pages.CopyTo(tocPage, arr, 0, arr.Length);
            _c1Pdf.Pages.RemoveRange(tocPage, arr.Length);
            _c1Pdf.Pages.InsertRange(0, arr);
        }

        private string BuildRandomTitle()
        {
            string[] a1 = "Learning|Explaining|Mastering|Forgetting|Examining|Understanding|Applying|Using|Destroying".Split('|');
            string[] a2 = "Music|Tennis|Golf|Zen|Diving|Modern Art|Gardening|Architecture|Mathematics|Investments|.NET|Java".Split('|');
            string[] a3 = "Quickly|Painlessly|The Hard Way|Slowly|Painfully|With Panache".Split('|');
            return string.Format("{0} {1} {2}", a1[_rnd.Next(a1.Length - 1)], a2[_rnd.Next(a2.Length - 1)], a3[_rnd.Next(a3.Length - 1)]);
        }

        private string BuildRandomParagraph()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 5 + _rnd.Next(10); i++)
                sb.AppendFormat(BuildRandomSentence());
            return sb.ToString();

        }

        private string BuildRandomSentence()
        {
            string[] a1 = "Artists|Movie stars|Musicians|Politicians|Computer programmers|Modern thinkers|Gardeners|Experts|Some people|Hockey players".Split('|');
            string[] a2 = "know|seem to think about|care about|often discuss|dream about|hate|love|despise|respect|long for|pay attention to|embrace".Split('|');
            string[] a3 = "the movies|chicken soup|tea|many things|sushi|my car|deep thoughts|tasteless jokes|vaporware|cell phones|hot dogs|ballgames".Split('|');
            string[] a4 = "incessantly|too much|easily|without reason|rapidly|sadly|randomly|vigorously|more than usual|with enthusiasm|shamelessly|on Tuesdays".Split('|');
            return string.Format("{0} {1} {2} {3}. ", a1[_rnd.Next(a1.Length - 1)], a2[_rnd.Next(a2.Length - 1)], a3[_rnd.Next(a3.Length - 1)], a4[_rnd.Next(a4.Length - 1)]);
        }

        // get the current page rectangle (depends on paper size)
        // and apply a 1" margin all around it.
        private RectangleF GetPageRect()
        {
            RectangleF rcPage = _c1Pdf.PageRectangle;
            rcPage.Inflate(-72, -72);
            return rcPage;
        }

        private RectangleF RenderParagraph(string text, Font font, RectangleF rcPage, RectangleF rc, bool outline = false, bool linkTarget = false)
        {
            // if it won't fit this page, do a page break
            rc.Height = _c1Pdf.MeasureString(text, font, rc.Width).Height;
            if (rc.Bottom > rcPage.Bottom)
            {
                _c1Pdf.NewPage();
                rc.Y = rcPage.Top;
            }

            // draw the string
            _c1Pdf.DrawString(text, font, Brushes.Black, rc);

            // add headings to outline
            if (outline)
            {
                _c1Pdf.DrawLine(Pens.Black, rc.X, rc.Y, rc.Right, rc.Y);
                _c1Pdf.AddBookmark(text, 0, rc.Y);
            }

            // add link target
            if (linkTarget)
            {
                _c1Pdf.AddTarget(text, rc);
            }

            // update rectangle for next time
            rc.Offset(0, rc.Height);
            return rc;
        }


        //================================================================================
        // add page footers to a document
        // 
        // this method is called by all samples in this project. it scans the document
        // and adds a 'page n of m' footer to each page. the footers are rendered as 
        // vertical text along the right edge of the document.
        // 
        // adding content to an existing page is easy: just set the CurrentPage property
        // to point to an existing page and write into it as usual.
        // 
        private void AddFooters()
        {
            Font fontHorz = new Font("Tahoma", 7, FontStyle.Bold);
            Font fontVert = new Font("Viner Hand ITC", 14, FontStyle.Bold);

            StringFormat sfRight = new StringFormat();
            sfRight.Alignment = StringAlignment.Far;

            StringFormat sfVert = new StringFormat();
            sfVert.FormatFlags |= StringFormatFlags.DirectionVertical;
            sfVert.Alignment = StringAlignment.Center;

            for (int page = 0; page < _c1Pdf.Pages.Count; page++)
            {
                // select page we want (could change PageSize)
                _c1Pdf.CurrentPage = page;

                // build rectangles for rendering text
                RectangleF rcPage = GetPageRect();
                RectangleF rcFooter = rcPage;
                rcFooter.Y = rcFooter.Bottom + 6;
                rcFooter.Height = 12;
                RectangleF rcVert = rcPage;
                rcVert.X = rcPage.Right + 6;

                // add left-aligned footer
                string text = _c1Pdf.DocumentInfo.Title;
                _c1Pdf.DrawString(text, fontHorz, Brushes.Gray, rcFooter);

                // add right-aligned footer
                text = string.Format("Page {0} of {1}", page + 1, _c1Pdf.Pages.Count);
                _c1Pdf.DrawString(text, fontHorz, Brushes.Gray, rcFooter, sfRight);

                // add vertical text
                text = _c1Pdf.DocumentInfo.Title + " (document created using the C1Pdf .NET component)";
                _c1Pdf.DrawString(text, fontVert, Brushes.LightGray, rcVert, sfVert);

                // draw lines on bottom and right of the page
                _c1Pdf.DrawLine(Pens.Gray, rcPage.Left, rcPage.Bottom, rcPage.Right, rcPage.Bottom);
                _c1Pdf.DrawLine(Pens.Gray, rcPage.Right, rcPage.Top, rcPage.Right, rcPage.Bottom);
            }
        }
    }
}
