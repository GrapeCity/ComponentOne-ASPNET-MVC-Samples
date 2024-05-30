using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMyFlight.Models;
using C1.C1Pdf;
using System.Reflection;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Web.Hosting;

namespace BookMyFlight.Controllers
{
    public class MyBookingsController : Controller
    {
        private C1.C1Pdf.C1PdfDocument C1PDF;
        
        //MyBookings View Controller
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Home/Login?CallP=MyBookings/Index");
            }
            MyBookingsModel ModelObj = new MyBookingsModel();
            ModelObj.SelToMonth = DateTime.Today.Month;
            ModelObj.SelToYear = DateTime.Today.Year;
            DateTime TempDate = new DateTime(ModelObj.SelToYear, ModelObj.SelToMonth, 1);
            TempDate = TempDate.AddMonths(-5);
            ModelObj.SelFromMonth = TempDate.Month;
            ModelObj.SelFromYear = TempDate.Year;
            ModelObj.YearList = new string[5];
            for (int i = 0; i < 5; i++)
                ModelObj.YearList[i] = (DateTime.Today.Year - 4 + i).ToString();
            ModelObj.TravelFromDate = DateTime.Today.Subtract(new TimeSpan(DateTime.Today.Day - 1, 0, 0, 0)).AddMonths(-3);
            ModelObj.TravelToDate = DateTime.Today.Subtract(new TimeSpan(DateTime.Today.Day - 1, 0, 0, 0));
            return View(ModelObj);
        }

        //Controller to Open Booking Invoice PDF
        public ActionResult OpenInvoice(long BookID)
        {
            MemoryStream InvoiceStream = new MemoryStream();
            try
            {
                if (BookID > 0)
                {
                    C1PdfDocument InvoicePDF = GenerateInvoice(BookID);
                    InvoicePDF.Save(InvoiceStream);
                    byte[] byteInfo = InvoiceStream.ToArray();
                    InvoiceStream.Write(byteInfo, 0, byteInfo.Length);
                    InvoiceStream.Position = 0;
                    ViewBag.PageTitle = "Booking Invoice-" + BookID.ToString();
                }
            }
            catch
            {
            }
            return new FileStreamResult(InvoiceStream, "application/pdf");
        }

        //To Get Booking Records by Ajax Call
        [HttpPost]
        public JsonResult GetBookings(long? BookID, string Book_FDateStr, string Book_TDateStr, string Travel_FDateStr, string Travel_TDateStr, long? User_ID, long? From_DestID, long? To_DestID, bool? Active)
        {
            try
            {
                DateTime? Book_FDate = null, Book_TDate = null, Travel_FDate = null, Travel_TDate = null;
                if (Book_FDateStr != null && Book_FDateStr != "")
                    Book_FDate = DateTime.Parse(Book_FDateStr);
                if (Book_TDateStr != null && Book_TDateStr != "")
                    Book_TDate = DateTime.Parse(Book_TDateStr);
                if (Travel_FDateStr != null && Travel_FDateStr != "")
                    Travel_FDate = DateTime.Parse(Travel_FDateStr);
                if (Travel_TDateStr != null && Travel_TDateStr != "")
                    Travel_TDate = DateTime.Parse(Travel_TDateStr);
                List<GetBookingsResult> BookingsList = Common.GetAllBookings().Where(x => x.Id == BookID || BookID == null || BookID == 0).ToList();
                BookingsList = BookingsList.Where(x => x.Book_Date >= Book_FDate || Book_FDate == null).ToList();
                BookingsList = BookingsList.Where(x => x.Book_Date <= Book_TDate || Book_TDate == null).ToList();
                BookingsList = BookingsList.Where(x => x.Departure_ArrivalDateTime >= Travel_FDate || Travel_FDate == null).ToList();
                BookingsList = BookingsList.Where(x => x.Departure_ArrivalDateTime >= Travel_TDate || Travel_TDate == null).ToList();
                BookingsList = BookingsList.Where(x => x.User_ID == User_ID || User_ID == null || User_ID == 0).ToList();
                BookingsList = BookingsList.Where(x => x.From_DestID == From_DestID || From_DestID == null || From_DestID == 0).ToList();
                BookingsList = BookingsList.Where(x => x.To_DestID == To_DestID || To_DestID == null || To_DestID == 0).ToList();
                BookingsList = BookingsList.Where(x => x.Active == Active || Active == null).ToList();
                return Json(new { ResultType = "Success", ErrMsg = "", BookingsList = BookingsList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ResultType = "Fail", ErrMsg = "Error occurred: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //Function to generate Invoice pdf and return
        private C1PdfDocument GenerateInvoice(long InvoiceID)
        {
            MemoryStream InvoiceStream = new MemoryStream();
            // create pdf document
            C1PDF = new C1PdfDocument();
            StringFormat sf = new StringFormat();
            C1PDF.Clear();
            C1PDF.DocumentInfo.Title = "INVOICE-" + InvoiceID;
            GetBookingsResult InvoiceData = Common.GetAllBookings().Where(x => x.Id == InvoiceID).FirstOrDefault();//ADBML.GetBookings(InvoiceID, null, null, null, null, null, null, null, null).ToList()[0];
            List<GetBookingsDetailsResult> InvoicePSG = Common.GetAllBookingsDetails().Where(x => x.Book_Id == InvoiceID).ToList(); //ADBML.GetBookingsDetails(InvoiceID, null).ToList();
            RectangleF rcPage = GetPageRect();
            Pen pen = new Pen(Color.Black, 0.1f);
            PointF Pagecenter = new PointF(rcPage.X + rcPage.Width / 2, rcPage.Y + rcPage.Height / 2);
            //draw Page border
            C1PDF.DrawRectangle(pen, rcPage);
            // add title            
            string Title = "INVOICE";
            Font titleFont = new Font("Tahoma", 15, FontStyle.Italic);
            Font bodyFont = new Font("Tahoma", 9);
            Font bodyFontBold = new Font("Tahoma", 9, FontStyle.Bold);
            C1PDF.DrawString(Title, titleFont, Brushes.Black, new PointF(rcPage.Left + Pagecenter.X - 2 * titleFont.Size * Title.Length / 2, rcPage.Top + 10));

            //Add logo of BookMyFlight
            PointF logoPoint = new PointF(rcPage.Left + 10, rcPage.Top + 40);
            string AppPath = AppDomain.CurrentDomain.BaseDirectory;

            string FullPath = AppPath + "images/logo.PNG";
            RectangleF LogoRect = new RectangleF(logoPoint.X, logoPoint.Y, 150, 25);
            Image LogoImg = Image.FromFile(FullPath);
            C1PDF.DrawImage(LogoImg, LogoRect);

            //draw right panel            
            string PnlRight1Str1 = "BookMyFlight\n211 North Whitfield, Seventh Floor\nPittsburgh, PA 15206\nTel 1.800.858.2739 | 412.681.4343\nFax 412.681.4384\nE-mail us.sales@mescius.com\n";
            RectangleF PnlRight1 = new RectangleF(Pagecenter.X, rcPage.Top + 40, rcPage.Width / 2 - 20, 100);
            sf.Alignment = StringAlignment.Far;
            sf.LineAlignment = StringAlignment.Near;
            C1PDF.DrawString(PnlRight1Str1, bodyFont, Brushes.Purple, PnlRight1, sf);

            //Add Client Info
            C1PDF.DrawString("Client Info-", bodyFontBold, Brushes.Purple, new PointF(rcPage.Left + 10, PnlRight1.Y + PnlRight1.Height + 20));
            string PnlLeft2Str1 = "";
            PnlLeft2Str1 += "\nName";
            PnlLeft2Str1 += "\nAddress";
            PnlLeft2Str1 += "\nCity";
            PnlLeft2Str1 += "\nCountry";
            PnlLeft2Str1 += "\nPhone";
            PnlLeft2Str1 += "\nE-mail";
            RectangleF PnlLeft2 = new RectangleF(rcPage.Left + 10, PnlRight1.Y + PnlRight1.Height + 20, 70, 80);
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Near;
            C1PDF.DrawString(PnlLeft2Str1, bodyFont, Brushes.Black, PnlLeft2, sf);
            string PnlLeft3Str = "";
            PnlLeft3Str += "\n: " + Common.GetUserProfile().First_Name + " " + Common.GetUserProfile().Last_Name;
            PnlLeft3Str += "\n: " + Common.GetUserProfile().Address1;
            PnlLeft3Str += "\n: " + Common.GetUserProfile().City;
            PnlLeft3Str += "\n: " + Common.GetUserProfile().Country;
            PnlLeft3Str += "\n: " + Common.GetUserProfile().Contact_No;
            PnlLeft3Str += "\n: " + Common.GetUserProfile().Email_ID;
            RectangleF PnlLeft3 = new RectangleF(rcPage.Left + 10 + 70, PnlRight1.Y + PnlRight1.Height + 20, 150, 80);
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Near;
            C1PDF.DrawString(PnlLeft3Str, bodyFont, Brushes.Black, PnlLeft3, sf);

            //Add Booking Info
            C1PDF.DrawString("Booking Info-", bodyFontBold, Brushes.Purple, new PointF(PnlRight1.X + 30, PnlRight1.Y + PnlRight1.Height + 20));
            string PnlRight2Str = "";
            PnlRight2Str += "\nBooking Reference";
            PnlRight2Str += "\nInvoice No";
            PnlRight2Str += "\nInvoice Date";
            PnlRight2Str += "\nBooking Date";
            RectangleF PnlRight2 = new RectangleF(PnlRight1.X + 30, PnlRight1.Y + PnlRight1.Height + 20, 90, 80);
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Near;
            C1PDF.DrawString(PnlRight2Str, bodyFont, Brushes.Black, PnlRight2, sf);
            string PnlRight3Str = "";
            PnlRight3Str += "\n: " + InvoiceData.Id;
            PnlRight3Str += "\n: BMF/" + InvoiceData.Book_Date.Year.ToString() + "/" + InvoiceData.Book_Date.Month.ToString() + InvoiceData.Id;
            PnlRight3Str += "\n: " + InvoiceData.Book_Date.ToShortDateString();
            PnlRight3Str += "\n: " + InvoiceData.Book_Date.ToShortDateString() + " " + InvoiceData.Book_Date.ToShortTimeString();
            RectangleF PnlRight3 = new RectangleF(PnlRight1.X + 30 + 90, PnlRight1.Y + PnlRight1.Height + 20, 150, 80);
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Near;
            C1PDF.DrawString(PnlRight3Str, bodyFont, Brushes.Black, PnlRight3, sf);

            //Add Travelers Info
            C1PDF.DrawLine(pen, new PointF(rcPage.X, PnlLeft2.Y + PnlLeft2.Height + 10), new PointF(rcPage.X + rcPage.Width, PnlLeft2.Y + PnlLeft2.Height + 10));
            C1PDF.DrawString("Traveler", bodyFontBold, Brushes.Black, new PointF(rcPage.X + 5, PnlLeft2.Y + PnlLeft2.Height + 10 + 10));
            C1PDF.DrawString("From-To", bodyFontBold, Brushes.Black, new PointF(rcPage.X + 130, PnlLeft2.Y + PnlLeft2.Height + 10 + 10));
            C1PDF.DrawString("Class", bodyFontBold, Brushes.Black, new PointF(rcPage.X + 5 + 130 + 60, PnlLeft2.Y + PnlLeft2.Height + 10 + 10));
            C1PDF.DrawString("Airline", bodyFontBold, Brushes.Black, new PointF(rcPage.X + 5 + 130 + 60 + 60, PnlLeft2.Y + PnlLeft2.Height + 10 + 10));
            C1PDF.DrawString("Travel Date", bodyFontBold, Brushes.Black, new PointF(rcPage.X + 5 + 130 + 50 + 50 + 130, PnlLeft2.Y + PnlLeft2.Height + 10 + 10));
            sf.Alignment = StringAlignment.Far;
            sf.LineAlignment = StringAlignment.Near;
            C1PDF.DrawString("Fare", bodyFontBold, Brushes.Black, new PointF(rcPage.X + rcPage.Width - 23, PnlLeft2.Y + PnlLeft2.Height + 10 + 10));
            C1PDF.DrawLine(pen, new PointF(rcPage.X, PnlLeft2.Y + PnlLeft2.Height + 10 + 30), new PointF(rcPage.X + rcPage.Width, PnlLeft2.Y + PnlLeft2.Height + 10 + 30));
            float YValue = PnlLeft2.Y + PnlLeft2.Height + 10 + 20;
            for (int PsgCount = 0; PsgCount < InvoicePSG.Count; PsgCount++)
            {
                YValue += 15;
                C1PDF.DrawString(Common.GetAllTravelers().Where(x => x.Id == InvoicePSG[PsgCount].Traveller_Id).FirstOrDefault().User_Name, bodyFont, Brushes.Black, new PointF(rcPage.X + 5, YValue));
                C1PDF.DrawString(InvoiceData.From_Dest + "-" + InvoiceData.To_Dest, bodyFont, Brushes.Black, new PointF(rcPage.X + 130, YValue));
                C1PDF.DrawString(InvoiceData.Class, bodyFont, Brushes.Black, new PointF(rcPage.X + 5 + 130 + 60, YValue));
                C1PDF.DrawString(InvoiceData.Depart_Airlines + "-" + InvoiceData.Depart_FlightNo, bodyFont, Brushes.Black, new PointF(rcPage.X + 5 + 130 + 60 + 60, YValue));
                C1PDF.DrawString(InvoiceData.Departure_DateTime.ToString("MMM dd,yyyy"), bodyFont, Brushes.Black, new PointF(rcPage.X + 5 + 130 + 50 + 50 + 130, YValue));
                double Fare = 0;
                float FarePer = 1;
                if (InvoicePSG[PsgCount].Type == "Child")
                    FarePer = .8F;
                else if (InvoicePSG[PsgCount].Type == "Infant")
                    FarePer = .4F;
                Fare += (double)InvoiceData.Depart_AdultFare * FarePer;
                if (InvoiceData.Return_DateTime != null)
                    Fare += (double)InvoiceData.Return_AdultFare * FarePer;
                C1PDF.DrawString(Math.Round(Fare, 0).ToString() + ".00", bodyFont, Brushes.Black, new RectangleF(new PointF(rcPage.X + rcPage.Width - 50, YValue), new SizeF(45, 20)), sf);
                if (InvoiceData.Return_DateTime != null)
                {
                    YValue += 12;
                    C1PDF.DrawString(InvoiceData.To_Dest + "-" + InvoiceData.From_Dest, bodyFont, Brushes.Black, new PointF(rcPage.X + 130, YValue));
                    C1PDF.DrawString(InvoiceData.Class, bodyFont, Brushes.Black, new PointF(rcPage.X + 5 + 130 + 60, YValue));
                    C1PDF.DrawString(InvoiceData.Return_Airlines + "-" + InvoiceData.Return_FlightNo, bodyFont, Brushes.Black, new PointF(rcPage.X + 5 + 130 + 60 + 60, YValue));
                    C1PDF.DrawString(DateTime.Parse(InvoiceData.Return_DateTime.ToString()).ToString("MMM dd,yyyy"), bodyFont, Brushes.Black, new PointF(rcPage.X + 5 + 130 + 50 + 50 + 130, YValue));
                }
            }
            YValue += 20;
            C1PDF.DrawLine(pen, new PointF(rcPage.X, YValue), new PointF(rcPage.X + rcPage.Width, YValue));
            YValue += 5;
            //Add Total Fare
            C1PDF.DrawString("Total Fare:", bodyFont, Brushes.Green, new PointF(rcPage.X + 5 + 130 + 50 + 50 + 80, YValue));
            C1PDF.DrawString("$" + Math.Round(InvoiceData.Total_Fare, 0).ToString() + ".00", bodyFontBold, Brushes.Green, new RectangleF(new PointF(rcPage.X + rcPage.Width - 45 - 55, YValue), new SizeF(100, 20)), sf);
            YValue += 15;
            C1PDF.DrawLine(pen, new PointF(rcPage.X, YValue), new PointF(rcPage.X + rcPage.Width, YValue));
            //Add Footer
            C1PDF.DrawLine(pen, new PointF(rcPage.X, rcPage.Y + rcPage.Height - 30), new PointF(rcPage.X + rcPage.Width, rcPage.Y + rcPage.Height - 30));
            C1PDF.DrawString("Regd. Office: BookMyFlight, BookMyFlight, 211 North Whitfield, Seventh Floor, Pittsburgh, PA 15206\nTel 1.800.858.2739 | 412.681.4343, Fax 412.681.4384, E-mail us.sales@mescius.com", bodyFont, Brushes.Blue, new PointF(rcPage.X + 50, rcPage.Y + rcPage.Height - 25));
            return C1PDF;
        }

        //Function to get main rectangle of page
        internal RectangleF GetPageRect()
        {
            RectangleF rcPage = C1PDF.PageRectangle;
            rcPage.Inflate(-72, -72);
            return rcPage;
        }


    }
}
