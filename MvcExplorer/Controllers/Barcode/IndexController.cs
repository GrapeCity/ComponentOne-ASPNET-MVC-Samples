using C1.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class BarcodeController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            return View(getBarcodeControls());
        }

        private object[] getBarcodeControls()
        {
            return new object[]
            {
                new { Name = "Codabar" },
                new { Name = "Code39" },
                new { Name = "Code49" },
                new { Name = "Code93" },
                new { Name = "Code128" },
                new
                {
                    Name = "DataMatrix",
                    Items = new object[] {
                        new { Name = "DataMatrixEcc200" },
                        new { Name = "DataMatrixEcc000" }
                    }
                },
                new
                {
                    Name = "EAN",
                    Items = new object[] {
                        new { Name = "Ean8" },
                        new { Name = "Ean13" }
                    }
                },
                new{ Name = "Gs1_128" },
                new
                {
                    Name = "Gs1DataBar",
                    Items = new object[] {
                        new { Name = "Gs1DataBarExpanded" },
                        new { Name = "Gs1DataBarExpandedStacked" },
                        new { Name = "Gs1DataBarLimited" },
                        new { Name = "Gs1DataBarOmnidirectional" },
                        new { Name = "Gs1DataBarStacked" },
                        new { Name = "Gs1DataBarStackedOmnidirectional" },
                        new { Name = "Gs1DataBarTruncated" }
                    }
                },
                new { Name = "Interleaved2of5" },
                new { Name = "Itf14" },
                new { Name = "JapanesePostal" },
                new
                {
                    Name = "PDF",
                    Items = new object[] {
                        new { Name = "Pdf417" },
                        new { Name = "MicroPdf417" }
                    }
                },
                new { Name = "QRCode" },
                new
                {
                    Name = "UPC",
                    Items = new object[] {
                        new { Name = "UpcA" },
                        new { Name = "UpcE0" },
                        new { Name = "UpcE1" }
                    }
                }
            };
        }
    }
}