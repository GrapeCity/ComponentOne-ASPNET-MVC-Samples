using System;
using System.Collections.Specialized;
using C1.Web.Api.Storage;
using System.IO;
using C1.C1Pdf;
using System.Drawing;

namespace CustomPdfProvider.Models
{
    public class CustomPdfStorageProvider : IStorageProvider
    {
        private string _root;

        public CustomPdfStorageProvider()
        {
            _root = GetFullRoot();
        }

        public IDirectoryStorage GetDirectoryStorage(string name, NameValueCollection args = null)
        {
            throw new NotImplementedException();
        }

        public IFileStorage GetFileStorage(string name, NameValueCollection args = null)
        {
            return new StreamFileStorage(CreatePdf(name));
        }

        private static string GetFullRoot()
        {
            var applicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var fullRoot = Path.GetFullPath(applicationBase);
            if (!fullRoot.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
            {
                // When we do matches in GetFullPath, we want to only match full directory names.
                fullRoot += Path.DirectorySeparatorChar;
            }
            return fullRoot;
        }

        private string GetFullPath(string name)
        {
            return Path.Combine(_root, name);
        }

        private Stream CreatePdf(string name)
        {
            switch (name)
            {
                case "TextFlow.pdf":
                    return CreateTextFlowPdf();
                case "TextPosition.pdf":
                    return CreateTextPositionPdf();
                case "DefaultDocument.pdf":
                    var stream = new FileStream(GetFullPath("Content/DefaultDocument.pdf"), FileMode.Open, FileAccess.Read);
                    return stream;
            }

            return null;
        }

        private RectangleF GetPageRect(C1PdfDocument c1pdf)
        {
            RectangleF rcPage = c1pdf.PageRectangle;
            rcPage.Inflate(-72, -72);
            return rcPage;
        }

        private RectangleF RenderParagraph(C1PdfDocument c1pdf, string text, Font font, RectangleF rcPage, RectangleF rc, bool outline, bool linkTarget)
        {
            // if it won't fit this page, do a page break
            rc.Height = c1pdf.MeasureString(text, font, rc.Width).Height;
            if (rc.Bottom > rcPage.Bottom)
            {
                c1pdf.NewPage();
                rc.Y = rcPage.Top;
            }

            // draw the string
            c1pdf.DrawString(text, font, Brushes.Black, rc);

            // show bounds (mainly to check word wrapping)
            //c1pdf.DrawRectangle(Pens.Sienna, rc);

            // add headings to outline
            if (outline)
            {
                c1pdf.DrawLine(Pens.Black, rc.X, rc.Y, rc.Right, rc.Y);
                c1pdf.AddBookmark(text, 0, rc.Y);
            }

            // add link target
            if (linkTarget)
            {
                c1pdf.AddTarget(text, rc);
            }

            // update rectangle for next time
            rc.Offset(0, rc.Height);
            return rc;
        }

        private Stream CreateTextFlowPdf()
        {
            // load long string from resource file
            string text = "Resource not found...";

            string path = GetFullPath("Content/flow.txt");
            StreamReader sr = new StreamReader(path);
            text = sr.ReadToEnd();

            text = text.Replace("\t", "   ");
            text = string.Format("{0}\r\n\r\n---oOoOoOo---\r\n\r\n{0}", text);

            // create pdf document
            var c1pdf = new C1PdfDocument();
            c1pdf.DocumentInfo.Title = "Text Flow";

            // add title
            Font titleFont = new Font("Tahoma", 24, FontStyle.Bold);
            Font bodyFont = new Font("Tahoma", 9);
            RectangleF rcPage = GetPageRect(c1pdf);
            RectangleF rc = RenderParagraph(c1pdf, c1pdf.DocumentInfo.Title, titleFont, rcPage, rcPage, false, false);
            rc.Y += titleFont.Size + 6;
            rc.Height = rcPage.Height - rc.Y;

            // create two columns for the text
            RectangleF rcLeft = rc;
            rcLeft.Width = rcPage.Width / 2 - 12;
            rcLeft.Height = 300;
            rcLeft.Y = (rcPage.Y + rcPage.Height - rcLeft.Height) / 2;
            RectangleF rcRight = rcLeft;
            rcRight.X = rcPage.Right - rcRight.Width;

            // start with left column
            rc = rcLeft;

            // render string spanning columns and pages
            for (;;)
            {
                // render as much as will fit into the rectangle
                rc.Inflate(-3, -3);
                int nextChar = c1pdf.DrawString(text, bodyFont, Brushes.Black, rc);
                rc.Inflate(+3, +3);
                c1pdf.DrawRectangle(Pens.Silver, rc);

                // break when done
                if (nextChar >= text.Length)
                    break;

                // get rid of the part that was rendered
                text = text.Substring(nextChar);

                // switch to right-side rectangle
                if (rc.Left == rcLeft.Left)
                {
                    rc = rcRight;
                }
                else // switch to left-side rectangle on the next page
                {
                    c1pdf.NewPage();
                    rc = rcLeft;
                }
            }

            // save and show pdf document
            var stream = new MemoryStream();
            c1pdf.Save(stream);
            stream.Position = 0;
            return stream;
        }

        private Stream CreateTextPositionPdf()
        {
            // create pdf document
            var c1pdf = new C1PdfDocument();
            c1pdf.DocumentInfo.Title = "Text Alignment";

            // create objects
            StringFormat sf = new StringFormat();
            Font titleFont = new Font("Tahoma", 24, FontStyle.Bold);
            Font font = new Font("Tahoma", 10);
            RectangleF rcPage = GetPageRect(c1pdf);

            // render title
            RenderParagraph(c1pdf, c1pdf.DocumentInfo.Title, titleFont, rcPage, rcPage, false, false);

            // draw cross-hair
            Pen pen = new Pen(Color.Black, 0.1f);
            PointF center = new PointF(rcPage.X + rcPage.Width / 2, rcPage.Y + rcPage.Height / 2);
            c1pdf.DrawLine(pen, center.X, rcPage.Y, center.X, rcPage.Bottom);
            c1pdf.DrawLine(pen, rcPage.X, center.Y, rcPage.Right, center.Y);

            // draw some text
            string text = "This is some sample text that will be rendered in rectangles on the page.";

            sf.Alignment = StringAlignment.Far;
            sf.LineAlignment = StringAlignment.Far;
            RectangleF rc = new RectangleF(center.X - 100, center.Y - 100, 100, 100);
            c1pdf.FillRectangle(Brushes.LightGoldenrodYellow, rc);
            c1pdf.DrawString("TOP LEFT: " + text, font, Brushes.Black, rc, sf);
            c1pdf.DrawRectangle(Pens.Black, rc);

            sf.LineAlignment = StringAlignment.Near;
            rc.Offset(0, rc.Height);
            c1pdf.FillRectangle(Brushes.LightSalmon, rc);
            c1pdf.DrawString("BOTTOM LEFT: " + text, font, Brushes.Black, rc, sf);
            c1pdf.DrawRectangle(Pens.Black, rc);

            sf.Alignment = StringAlignment.Near;
            rc.Offset(rc.Width, 0);
            c1pdf.FillRectangle(Brushes.LightSeaGreen, rc);
            c1pdf.DrawString("BOTTOM RIGHT: " + text, font, Brushes.Black, rc, sf);
            c1pdf.DrawRectangle(Pens.Black, rc);

            sf.LineAlignment = StringAlignment.Far;
            rc.Offset(0, -rc.Height);
            c1pdf.FillRectangle(Brushes.LightSteelBlue, rc);
            c1pdf.DrawString("TOP RIGHT: " + text, font, Brushes.Black, rc, sf);
            c1pdf.DrawRectangle(Pens.Black, rc);

            // render some rtf as well
            string qbf = "The quick brown fox jumped over the lazy dog. ";
            qbf = qbf + qbf + qbf + qbf;
            text = @"This is some {\b RTF} text. It will render into the rectangle as usual.\par\par" +
                    @"\qr And {\i this paragraph} will be {\b\i right-aligned}. {\fs12 " + qbf + @"\par\par}" +
                    @"\qc And {\i this paragraph} will be {\b\i center-aligned}. {\fs12 " + qbf + @"\par\par}" +
                    @"\qj And {\i this paragraph} will be {\b\i justified}. {\fs12 " + qbf + @"\par\par Done.}";
            rc.Location = new PointF(rcPage.X + 12, rcPage.Y + 50);
            rc.Size = c1pdf.MeasureStringRtf(text, font, rc.Width * 2);
            c1pdf.FillRectangle(Brushes.DarkBlue, rc);
            c1pdf.DrawStringRtf(text, font, Brushes.White, rc);
            c1pdf.DrawRectangle(Pens.Black, rc);

            // and some rtf with bullets
            text = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fswiss\fcharset0 Arial;}{\f1\fnil\fcharset2 Symbol;}}" +
                   @"\viewkind4\uc1\pard\f0\fs20 Here's a bullet list:\par" +
                   @"\par" +
                   @"\pard{\pntext\f1\'B7\tab}{\*\pn\pnlvlblt\pnf1\pnindent0{\pntxtb\'B7}}\fi-360\li360 Item 1\par" +
                   @"{\pntext\f1\'B7\tab}Item 2\par" +
                   @"{\pntext\f1\'B7\tab}Item 3\par" +
                   @"{\pntext\f1\'B7\tab}Item 4\par" +
                   @"\pard\par" +
                   @"}";
            rc.Location = new PointF(rcPage.X + 250, rcPage.Y + 50);
            rc.Size = c1pdf.MeasureStringRtf(text, font, rc.Width);
            c1pdf.DrawStringRtf(text, font, Brushes.White, rc);
            c1pdf.DrawRectangle(Pens.Black, rc);

            // box the whole thing
            pen = new Pen(Brushes.Black, 1);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            c1pdf.DrawRectangle(pen, rcPage);

            // save and show pdf document
            var stream = new MemoryStream();
            c1pdf.Save(stream);
            stream.Position = 0;
            return stream;
        }
    }
}