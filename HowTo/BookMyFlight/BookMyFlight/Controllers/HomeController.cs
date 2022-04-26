using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMyFlight.Models;


namespace BookMyFlight.Controllers
{
    public class HomeController : Controller
    {
        //Home/Index Page Controller
        public ActionResult Index()
        {
            Common Cmn = new Common();
            MainModel MainModelObj = new MainModel();
            MainModelObj.RecentSearchesList = Common.GetRecentSearches();
            MainModelObj.TripType = "SINGLE";
            MainModelObj.DepartDate = DateTime.Today;
            MainModelObj.ReturnDate = DateTime.Today.AddDays(1);
            MainModelObj.Adult = 1;
            MainModelObj.Child = 0;
            MainModelObj.Infant = 0;
            MainModelObj.Class = "ECONOMY";
            return View(MainModelObj);
        }

        //Refine Search Page Controller
        public ActionResult RefineSearch(string TripType, string FromDest, string ToDest, string DepartDateStr, string ReturnDateStr, int Adult, int Child, int Infant, string Class, float FFDepF=0, float FFRetF=0)
        {
            DateTime DepartDate = DateTime.Parse(DepartDateStr);
            DateTime ReturnDate = DateTime.Today;
            if (TripType == "ROUND" && ReturnDateStr != null)
                ReturnDate = DateTime.Parse(ReturnDateStr);
            MainModel MainModelObj = new MainModel();
            MainModelObj.TripType = TripType;
            MainModelObj.FromDest = FromDest;
            MainModelObj.ToDest = ToDest;
            MainModelObj.DepartDate = DepartDate;
            MainModelObj.ReturnDate = ReturnDate;
            MainModelObj.Adult = Adult;
            MainModelObj.Child = Child;
            MainModelObj.Infant = Infant;
            MainModelObj.Class = Class;
            List<string> SelAirlines = new List<string>();
            SelAirlines.Add("All");
            List<Common.SearchFlight> ReturnFlightList = new List<Common.SearchFlight>();
            List<Common.SearchFlight> DepartFlightList = GetFlightList(TripType, FromDest, ToDest, DepartDateStr, ReturnDateStr, Adult, Child, Infant, Class, 0, 0, 0, SelAirlines, ref ReturnFlightList, FFDepF, FFRetF);
            Session["DepartFlightList"] = DepartFlightList;
            Session["ReturnFlightList"] = ReturnFlightList;
            Common.TripInfoC TripInfo = new Common.TripInfoC(0, 0, TripType, FromDest, ToDest, DepartDate, ReturnDate, Class, Adult, Child, Infant, 0);
            Session["TripInfo"] = TripInfo;
            DateTime? RetDate = null;
            string DepartDateForShow = DepartDate.ToShortDateString();
            string ReturnDateForShow = "N/A";
            if (TripType == "ROUND")
            {
                RetDate = ReturnDate;
                ReturnDateForShow = ReturnDate.ToShortDateString();
            }
            List<Common.RecentSearches> RecSearches = Common.GetRecentSearches();
            if (RecSearches.Where(x => x.FromDest == FromDest && x.ToDest == ToDest && x.DepartDate.ToString("MM/dd/yyyy") == DepartDate.ToString("MM/dd/yyyy")).Count() == 0)
            {
                if (RecSearches.Count >= 5)
                    RecSearches.RemoveAt(0);
                RecSearches.Add(new Common.RecentSearches(TripType, FromDest, ToDest, DepartDate, RetDate, DepartDateForShow, ReturnDateForShow, Adult, Child, Infant, DepartFlightList.Min(x => x.Fare)));
                Session["RecentSearchesList"] = RecSearches;
            }
            List<Common.TravelerList> SelAdultList = new List<Common.TravelerList>();
            for (int i = 1; i <= Adult; i++)
            {
                SelAdultList.Add(new Common.TravelerList(i, null, null, null, null, null, null));
            }
            Session["SelAdultList"] = SelAdultList;
            List<Common.TravelerList> SelChildList = new List<Common.TravelerList>();
            for (int i = 1; i <= Child; i++)
            {
                SelChildList.Add(new Common.TravelerList(i, null, null, null, null, null, null));
            }
            Session["SelChildList"] = SelChildList;
            List<Common.TravelerList> SelInfantList = new List<Common.TravelerList>();
            for (int i = 1; i <= Infant; i++)
            {
                SelInfantList.Add(new Common.TravelerList(i, null, null, null, null, null, null));
            }
            Session["SelInfantList"] = SelInfantList;
            MainModelObj.DepartFlights = DepartFlightList;
            MainModelObj.ReturnFlights = ReturnFlightList;
            return View(MainModelObj);
        }

        //Function to generate Depart & Return Flight List
        public List<Common.SearchFlight> GetFlightList(string TripType, string FromDest, string ToDest, string DepartDate, string ReturnDate, int Adult, int Child, int Infant, string Class, double MaxPrice, double MaxDepartTime, double MaxReturnTime, List<string> SelAirlinesList, ref List<Common.SearchFlight> ReturnFlights, float FFDepFare, float FFRetFare)
        {
            List<Common.SearchFlight> DepartFlights = new List<Common.SearchFlight>();
            ReturnFlights = new List<Common.SearchFlight>();
            using (AirlinesDBMLDataContext AObj = new AirlinesDBMLDataContext())
            {
                List<GetAirlinesListResult> TempAirlines = AObj.GetAirlinesList(0, null, true).ToList();
                List<GetAirlinesListResult> Airlines = new List<GetAirlinesListResult>();
                if (SelAirlinesList.Count > 0)
                {
                    if (SelAirlinesList.Where(x => x == "All").FirstOrDefault().Count() > 0)
                    {
                        Airlines = TempAirlines;
                    }
                    else
                    {
                        for (int i = 0; i < SelAirlinesList.Count; i++)
                        {
                            if (SelAirlinesList[i].ToString() != "All")
                            {
                                Airlines.Add(TempAirlines.Where(x => x.Airlines_Code == SelAirlinesList[i]).FirstOrDefault());
                            }
                        }
                    }
                    Random rnd = new Random();
                    TimeSpan DurationVar = new TimeSpan(rnd.Next(1, 30), rnd.Next(1, 59), 0);
                    for (int i = 0; i < Airlines.Count; i++)
                    {
                        string Flight_No = Airlines[i].Airlines_Code + rnd.Next(100, 999);
                        DateTime Depart_DateTime = DateTime.Parse(DepartDate);
                        double Fare = 0;
                        if (i == 0 && FFDepFare > 0)
                        {
                            Fare = FFDepFare;
                            Depart_DateTime = DateTime.Parse(DepartDate + " " + rnd.Next(0, 6) + ":" + rnd.Next(0, 59));
                        }
                        else
                        {
                            Fare = Math.Round(rnd.Next(10, 500) * rnd.NextDouble() * rnd.Next(100, 999), 0);
                            Depart_DateTime = DateTime.Parse(DepartDate + " " + rnd.Next(0, 24) + ":" + rnd.Next(0, 59));
                        }                        
                        TimeSpan Duration = DurationVar.Add(new TimeSpan(0, rnd.Next(-10, 120), 0));
                        DateTime Arrive_DateTime = Depart_DateTime.Add(Duration);
                        double TotalFare = Math.Round(Adult * Fare, 0) + Math.Round(Child * Fare * 80 / 100, 0) + Math.Round(Infant * Fare * 40 / 100, 0);
                        DepartFlights.Add(new Common.SearchFlight { Id = i + 1, Airlines = Airlines[i].Airlines, Airlines_Code = Airlines[i].Airlines_Code, Airlines_Img = Airlines[i].Img_URL, ArriveTime = Arrive_DateTime, ArriveTimeStr = Arrive_DateTime.ToString("HH:MM"), DepartTime = Depart_DateTime, DepartTimeStr = Depart_DateTime.ToString("HH:MM"), Duration = Duration, DurationStr = ((Duration.Hours > 9 ? Duration.Hours.ToString() : "0" + Duration.Hours.ToString()) + "h:" + (Duration.Minutes > 9 ? Duration.Minutes.ToString() : "0" + Duration.Minutes.ToString()) + "m"), DurationMin = Duration.Days * 24 * 60 + Duration.Hours * 60 + Duration.Minutes, Fare = Fare, Flight_No = Flight_No, TotalFare = TotalFare });
                        if (TripType == "ROUND")
                        {
                            Flight_No = Airlines[i].Airlines_Code + rnd.Next(100, 999);
                            DateTime Return_Depart_DateTime = DateTime.Parse(ReturnDate);
                            if (i == 0 && FFRetFare > 0)
                            {
                                Fare = FFRetFare;
                                Return_Depart_DateTime = DateTime.Parse(ReturnDate + " " + rnd.Next(12, 23) + ":" + rnd.Next(1, 59));
                            }
                            else
                            {
                                Fare = Math.Round(Fare * rnd.NextDouble() / rnd.NextDouble());
                                Return_Depart_DateTime = DateTime.Parse(ReturnDate + " " + Depart_DateTime.ToShortTimeString());
                                Return_Depart_DateTime = Return_Depart_DateTime.AddMinutes(rnd.Next(-10, 30));
                            }                            
                            Duration = Duration.Add(new TimeSpan(0, rnd.Next(-10, 20), 0));
                            DateTime Return_Arrive_DateTime = Return_Depart_DateTime.Add(Duration);
                            TotalFare = Math.Round(Adult * Fare, 0) + Math.Round(Child * Fare * 80 / 100, 0) + Math.Round(Infant * Fare * 40 / 100, 0);
                            ReturnFlights.Add(new Common.SearchFlight { Id = i + 1, Airlines = Airlines[i].Airlines, Airlines_Code = Airlines[i].Airlines_Code, Airlines_Img = Airlines[i].Img_URL, ArriveTime = Return_Arrive_DateTime, ArriveTimeStr = Return_Arrive_DateTime.ToString("HH:MM"), DepartTime = Return_Depart_DateTime, DepartTimeStr = Return_Depart_DateTime.ToString("HH:MM"), Duration = Duration, DurationStr = ((Duration.Hours > 9 ? Duration.Hours.ToString() : "0" + Duration.Hours.ToString()) + "h:" + (Duration.Minutes > 9 ? Duration.Minutes.ToString() : "0" + Duration.Minutes.ToString()) + "m"), DurationMin = Duration.Days * 24 * 60 + Duration.Hours * 60 + Duration.Minutes, Fare = Fare, Flight_No = Flight_No, TotalFare = TotalFare });
                        }
                    }
                }
            }
            return DepartFlights;
        }        

        //BookFlight View controller
        public ActionResult BookFlight(int Dep, int? Ret)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Home/Login?CallP=Home/BookFlight?Dep=" + Dep.ToString() + "&Ret=" + Ret.ToString());
            }
            else if (Session["TripInfo"] == null || Session["DepartFlightList"] == null || Common.DepartFlightList().Count == 0)
            {
                Response.Redirect("~/Home/Index");
            }
            Common.TripInfoC TripInfo = Common.GetSelTripInfo();
            TripInfo.DepId = Dep;
            MainModel ModelObj = new MainModel();
            if (Ret > 0 && TripInfo.TripType == "ROUND")
            {
                ModelObj.SelReturnFlights = Common.ReturnFlightList().Where(x => x.Id == Ret).FirstOrDefault();
                Session["ReturnFlight"] = Common.ReturnFlightList().Where(x => x.Id == Ret).FirstOrDefault();
                TripInfo.RetId = Ret;
            }
            Session["TripInfo"] = TripInfo;
            ModelObj.SelTripInfo = TripInfo;
            ModelObj.SelDepartFlights = Common.DepartFlightList().Where(x => x.Id == Dep).FirstOrDefault();
            Session["DepartFlight"] = Common.DepartFlightList().Where(x => x.Id == Dep).FirstOrDefault();
            ModelObj.TravelersList = Common.GetAllTravelers();
            ModelObj.SelAdultList = Common.SelAdultList();
            ModelObj.SelChildList = Common.SelChildList();
            ModelObj.SelInfantList = Common.SelInfantList();
            return View(ModelObj);
        }

        //Get data for initialization of BookFlight View by Ajax Call
        [HttpGet]
        public JsonResult BookFlight_Load()
        {
            try
            {
                return Json(new { ResultType = "Success", ErrMsg = "", SelAdultList = (List<Common.TravelerList>)Session["SelAdultList"], SelChildList = (List<Common.TravelerList>)Session["SelChildList"], SelInfantList = (List<Common.TravelerList>)Session["SelInfantList"] }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ResultType = "Fail", ErrMsg = "Error occurred: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //Save booking data by Ajax Call
        [HttpPost]
        public JsonResult SaveFlight(List<Common.TravelerListGet> SelAdultList, List<Common.TravelerListGet> SelChildList, List<Common.TravelerListGet> SelInfantList)//StaticModel.TravelerList[] selAdultList)//, List<StaticModel.TravelerList> SelChildList, List<StaticModel.TravelerList> SelInfantList)
        {
            try
            {
                int BookID = Common.GetAllBookingsDetails().Max(x => x.Id) + 1;
                Common.SearchFlight SelDepartFlight = Common.DepartFlightList().Where(x => x.Id == Common.GetSelTripInfo().DepId).FirstOrDefault();
                Common.SearchFlight SelReturnFlight;
                List<GetBookingsResult> AllBookings = Common.GetAllBookings();
                AllBookings.Add(new GetBookingsResult { Id = BookID, User_ID = 1, Book_Date = DateTime.Now, Book_DateStr = DateTime.Now.ToString("MMM dd, yyyy"), Departure_DateTime = SelDepartFlight.DepartTime, Departure_DateTimeStr = SelDepartFlight.DepartTime.ToString("MMM dd, yyyy"), Departure_ArrivalDateTime = SelDepartFlight.ArriveTime, Departure_ArrivalDateTimeStr = SelDepartFlight.ArriveTime.ToString("MMM dd, yyyy"), From_DestID = Common.GetDestList().Where(x => x.Dest == Common.GetSelTripInfo().FromDest).FirstOrDefault().id, To_DestID = Common.GetDestList().Where(x => x.Dest == Common.GetSelTripInfo().ToDest).FirstOrDefault().id, Class = Common.GetSelTripInfo().Class, Depart_AdultFare = SelDepartFlight.Fare, Departure_Fare = SelDepartFlight.TotalFare, Total_Fare = SelDepartFlight.TotalFare, Adult = Common.GetSelTripInfo().Adult, Child = Common.GetSelTripInfo().Child, Infant = Common.GetSelTripInfo().Infant, Depart_Airlines = SelDepartFlight.Airlines, Depart_Airlines_Code = SelDepartFlight.Airlines_Code, Depart_FlightNo = SelDepartFlight.Flight_No, Depart_Airlines_Img_URL = SelDepartFlight.Airlines_Img, From_Dest = Common.GetSelTripInfo().FromDest, From_Dest_Name = Common.GetDestList().Where(x => x.Dest == Common.GetSelTripInfo().FromDest).FirstOrDefault().Dest_Name, From_City = Common.GetDestList().Where(x => x.Dest == Common.GetSelTripInfo().FromDest).FirstOrDefault().City, From_Country = Common.GetDestList().Where(x => x.Dest == Common.GetSelTripInfo().FromDest).FirstOrDefault().Country, Active = true });
                if (Common.GetSelTripInfo().TripType == "ROUND" && Common.GetSelTripInfo().RetId > 0)
                {
                    SelReturnFlight = Common.ReturnFlightList().Where(x => x.Id == Common.GetSelTripInfo().RetId).FirstOrDefault();
                    AllBookings.Where(x => x.Id == BookID).ToList().ForEach(x =>
                    {
                        x.Return_DateTime = SelReturnFlight.DepartTime;
                        x.Return_DateTimeStr = SelReturnFlight.DepartTime.ToString("MMM dd, yyyy");
                        x.Return_ArrivalDateTime = SelReturnFlight.ArriveTime;
                        x.Return_ArrivalDateTimeStr = SelReturnFlight.ArriveTime.ToString("MMM dd,yyyy");
                        x.Return_AdultFare = SelReturnFlight.Fare;
                        x.Return_Fare = SelReturnFlight.TotalFare;
                        x.Total_Fare = x.Total_Fare + SelReturnFlight.TotalFare;
                        x.Return_Airlines = SelReturnFlight.Airlines;
                        x.Return_Airlines_Code = SelReturnFlight.Airlines_Code;
                        x.Return_Airlines_Img_URL = SelReturnFlight.Airlines_Img;
                        x.Return_FlightNo = SelReturnFlight.Flight_No;
                        x.To_Dest = Common.GetSelTripInfo().ToDest;
                        x.To_Dest_Name = Common.GetDestList().Where(y => y.Dest == Common.GetSelTripInfo().ToDest).FirstOrDefault().Dest_Name;
                        x.To_City = Common.GetDestList().Where(y => y.Dest == Common.GetSelTripInfo().ToDest).FirstOrDefault().City;
                        x.To_Country = Common.GetDestList().Where(y => y.Dest == Common.GetSelTripInfo().ToDest).FirstOrDefault().Country;
                    });
                }
                List<GetBookingsDetailsResult> AllBookingsDetails = Common.GetAllBookingsDetails();
                if (SelAdultList != null && SelAdultList.Count > 0)
                {
                    for (int ACount = 0; ACount < SelAdultList.Count; ACount++)
                    {
                        long TravelerId = 0;
                        if (SelAdultList[ACount].TravelerId != null)
                            TravelerId = long.Parse(SelAdultList[ACount].TravelerId.ToString());
                        if (TravelerId <= 0)
                            TravelerId = Common.AddUpdateTraveler(0, SelAdultList[ACount].Title, SelAdultList[ACount].First_Name, SelAdultList[ACount].Middle_Name, SelAdultList[ACount].Last_Name, null, null, null, null);
                        AllBookingsDetails.Add(new GetBookingsDetailsResult { Active = true, Add_Date = DateTime.Now, Book_Id = BookID, Id = Common.GetAllBookingsDetails().Max(x => x.Id) + 1, Traveller_Id = (int)TravelerId, Type = "Adult" });
                    }
                }
                if (SelChildList != null && SelChildList.Count > 0)
                {
                    for (int CCount = 0; CCount < SelChildList.Count; CCount++)
                    {
                        long TravelerId = 0;
                        if (SelChildList[CCount].TravelerId != null)
                            TravelerId = long.Parse(SelChildList[CCount].TravelerId.ToString());
                        if (TravelerId <= 0)
                            TravelerId = Common.AddUpdateTraveler(0, SelChildList[CCount].Title, SelChildList[CCount].First_Name, SelChildList[CCount].Middle_Name, SelChildList[CCount].Last_Name, null, null, null, null);
                        AllBookingsDetails.Add(new GetBookingsDetailsResult { Active = true, Add_Date = DateTime.Now, Book_Id = BookID, Id = Common.GetAllBookingsDetails().Max(x => x.Id) + 1, Traveller_Id = (int)TravelerId, Type = "Child" });
                    }
                }
                if (SelInfantList != null && SelInfantList.Count > 0)
                {
                    for (int ICount = 0; ICount < SelInfantList.Count; ICount++)
                    {
                        long TravelerId = 0;
                        if (SelInfantList[ICount].TravelerId != null)
                            TravelerId = long.Parse(SelInfantList[ICount].TravelerId.ToString());
                        if (TravelerId <= 0)
                            TravelerId = Common.AddUpdateTraveler(0, SelInfantList[ICount].Title, SelInfantList[ICount].First_Name, SelInfantList[ICount].Middle_Name, SelInfantList[ICount].Last_Name, null, null, null, null);
                        AllBookingsDetails.Add(new GetBookingsDetailsResult { Active = true, Add_Date = DateTime.Now, Book_Id = BookID, Id = Common.GetAllBookingsDetails().Max(x => x.Id) + 1, Traveller_Id = (int)TravelerId, Type = "Infant" });
                    }
                }
                Session["AllBookingsList"] = AllBookings;
                Session["AllBookingsDetailsList"] = AllBookingsDetails;
                return Json(new { ResultType = "Success", ErrMsg = "", BookID = BookID }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ResultType = "Fail", ErrMsg = "Error occurred: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //Login view Controller
        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        //TryLogin by Ajax Call
        public JsonResult TryLogin(string Email, string Password)
        {
            try
            {
                GetUserDetailsResult UserList = Common.GetUserProfile();//ADBML.GetUserDetails(null, Email, null, null).ToList()[0];
                if (UserList == null)
                    return Json(new { ResultType = "Fail", ErrMsg = "Invalid User Name/Password." }, JsonRequestBehavior.AllowGet);
                else
                {
                    if (UserList.Pwd == Password)
                    {
                        Session["UserID"] = UserList.User_ID;
                        Session["UserName"] = UserList.First_Name + " " + UserList.Last_Name;
                        return Json(new { ResultType = "Success", ErrMsg = "", UserID = UserList.User_ID, UserName = UserList.First_Name + " " + UserList.Last_Name }, JsonRequestBehavior.AllowGet);
                    }
                    else
                        return Json(new { ResultType = "Fail", ErrMsg = "Invalid Password." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { ResultType = "Fail", ErrMsg = "Error occurred: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}