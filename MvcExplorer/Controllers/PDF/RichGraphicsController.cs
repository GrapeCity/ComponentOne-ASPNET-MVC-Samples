using System;
using System.Drawing;
using System.IO;
using System.Web.Mvc;

namespace MvcExplorer.Controllers.PDF
{
    public partial class PDFController : Controller
    {
        //
        // GET: /RichGraphics/

        public ActionResult RichGraphics()
        {
            return View();
        }

        public ActionResult CreateRichGraphicsPDF()
        {
            try
            {
                InitialRichGraphicsPdfDocument();
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

        public void InitialRichGraphicsPdfDocument()
        {
            _c1Pdf = new C1.C1Pdf.C1PdfDocument();
            //start document
            _c1Pdf.Clear();
            _tempDir = Server.MapPath("../Temp");
            if (Directory.Exists(_tempDir))
            {

            }
            else
            {
                Directory.CreateDirectory(_tempDir);

            }
            //prepare to draw with Gdi-like commands
            int penWidth = 0;
            int penRGB = 0;
            Rectangle rc = new Rectangle(50, 50, 300, 200);
            string text = "Hello world of .NET Graphics and PDF.\r\nNice to meet you.";
            Font font = new Font("Times New Roman", 16, FontStyle.Italic | FontStyle.Underline);

            //start, c1, c2, end1, c3, c4, end
            PointF[] bezierPoints = 
			{
				new PointF(110f, 200f), new PointF(120f, 110f), new PointF(135f, 150f),
				new PointF(150f, 200f), new PointF(160f, 250f), new PointF(165f, 200f),
				new PointF(150f, 100f)
			};

            //draw to pdf document
            C1.C1Pdf.C1PdfDocument g = _c1Pdf;
            g.FillPie(Brushes.Red, rc, 0, 20f);
            g.FillPie(Brushes.Green, rc, 20f, 30f);
            g.FillPie(Brushes.Blue, rc, 60f, 12f);
            g.FillPie(Brushes.Gold, rc, -80f, -20f);
            for (float sa = 0; sa < 360; sa += 40)
            {
                Color penColor = Color.FromArgb(penRGB, penRGB, penRGB);
                Pen pen = new Pen(penColor, penWidth++);
                penRGB = penRGB + 20;
                g.DrawArc(pen, rc, sa, 40f);
            }
            g.DrawRectangle(Pens.Red, rc);
            g.DrawBeziers(Pens.Blue, bezierPoints);
            g.DrawString(text, font, Brushes.Black, rc);
        }
    }
}
