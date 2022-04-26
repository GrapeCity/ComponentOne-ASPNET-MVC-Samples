using C1.JsonNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BookMyFlight.Models
{
    public class TimeSpanConverter : JsonConverter
    {
        #region Methods
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        protected override object ReadJson(JsonReader reader, Type objectType, object existingValue)
        {
            return TimeSpan.Parse(reader.Current.ToString());
        }

        protected override void WriteJson(JsonWriter writer, object value)
        {
            writer.WriteValue(value.ToString());
        }
        #endregion Methods
    }

    public class Common
    {
        //TravelersList Class
        public class TravelerList
        {
            public int Id { get; set; }
            public int? TravelerId { get; set; }
            public string Title { get; set; }
            public string First_Name { get; set; }
            public string Middle_Name { get; set; }
            public string Last_Name { get; set; }
            public DateTime? DOB { get; set; }
            //constructor
            public TravelerList()
            {
            }
            //constructor-2
            public TravelerList(int P_Id, int? P_TravelerId, string P_Title, string P_First_Name, string P_Middle_Name, string P_Last_Name, DateTime? P_DOB)
            {
                Id = P_Id;
                TravelerId = P_TravelerId;
                Title = P_Title;
                First_Name = P_First_Name;
                Middle_Name = P_Middle_Name;
                Last_Name = P_Last_Name;
                DOB = P_DOB;
            }
        }

        //TravelersList Class-to get travelers at booking flight
        public class TravelerListGet
        {
            public int Id { get; set; }
            public int? TravelerId { get; set; }
            public string Title { get; set; }
            public string First_Name { get; set; }
            public string Middle_Name { get; set; }
            public string Last_Name { get; set; }
            public string DOB { get; set; }
            //constructor
            public TravelerListGet()
            {
            }
        }

        //TripInfo Class
        public class TripInfoC
        {
            public int? DepId { get; set; }
            public int? RetId { get; set; }
            public string TripType { get; set; }
            public string FromDest { get; set; }
            public string ToDest { get; set; }
            public DateTime DepartDate { get; set; }
            public DateTime? ReturnDate { get; set; }
            public string Class { get; set; }
            public int Adult { get; set; }
            public int Child { get; set; }
            public int Infant { get; set; }
            public double AdultFare { get; set; }
            public double ChildFare { get; set; }
            public double InfantFare { get; set; }
            public double Fare { get; set; }

            //constructor
            public TripInfoC()
            {

            }
            //constructor-2
            public TripInfoC(int? P_DepId, int P_RetId, string P_TripType, string P_FromDest, string P_ToDest, DateTime P_DepartDate, DateTime? P_ReturnDate, string P_Class, int P_Adult, int P_Child, int P_Infant, double P_Fare)
            {
                DepId = P_DepId;
                RetId = P_RetId;
                TripType = P_TripType;
                FromDest = P_FromDest;
                ToDest = P_ToDest;
                DepartDate = P_DepartDate;
                ReturnDate = P_ReturnDate;
                Class = P_Class;
                Adult = P_Adult;
                Child = P_Child;
                Infant = P_Infant;
                Fare = P_Fare;
            }
        }

        //RecentSearches Class
        public class RecentSearches
        {
            public int Id { get; set; }
            public string TripType { get; set; }
            public string FromDest { get; set; }
            public string ToDest { get; set; }
            public DateTime DepartDate { get; set; }
            public DateTime? ReturnDate { get; set; }
            public string DepartDateStr { get; set; }
            public string ReturnDateStr { get; set; }
            public int Adult { get; set; }
            public int Child { get; set; }
            public int Infant { get; set; }
            public double Fare { get; set; }
            //constructor
            public RecentSearches()
            {

            }
            //constructor-2
            public RecentSearches(string P_TripType, string P_FromDest, string P_ToDest, DateTime P_DepartDate, DateTime? P_ReturnDate, string P_DepartDateStr, string P_ReturnDateStr, int P_Adult, int P_Child, int P_Infant, double P_Fare)
            {
                if (System.Web.HttpContext.Current.Session["RecentSearchesList"] == null)
                    Id = 1;
                else
                {
                    List<RecentSearches> RecSearches = (List<RecentSearches>)System.Web.HttpContext.Current.Session["RecentSearchesList"];
                    Id = RecSearches.Max(x => x.Id) + 1;
                }
                TripType = P_TripType;
                FromDest = P_FromDest;
                ToDest = P_ToDest;
                DepartDate = P_DepartDate;
                ReturnDate = P_ReturnDate;
                DepartDateStr = P_DepartDateStr;
                ReturnDateStr = P_ReturnDateStr;
                Adult = P_Adult;
                Child = P_Child;
                Infant = P_Infant;
                Fare = P_Fare;
            }
        }

        //SearchFlight Result Class
        public class SearchFlight
        {
            public int Id { get; set; }
            public string Airlines { get; set; }
            public string Airlines_Code { get; set; }
            public string Airlines_Img { get; set; }
            public string Flight_No { get; set; }
            public DateTime DepartTime { get; set; }
            public string DepartTimeStr { get; set; }
            public DateTime ArriveTime { get; set; }
            public string ArriveTimeStr { get; set; }
            [JsonConverter(typeof(TimeSpanConverter))]
            public TimeSpan Duration { get; set; }
            public string DurationStr { get; set; }
            public int DurationMin { get; set; }
            public double Fare { get; set; }
            public double TotalFare { get; set; }
            //constructor
            public SearchFlight()
            {

            }
        }

        //function to Initialize & return Recent Searches List
        public static List<RecentSearches> GetRecentSearches()
        {
            List<RecentSearches> RValue = new List<RecentSearches>();
            if (System.Web.HttpContext.Current.Session["RecentSearchesList"] != null)
            {
                RValue = (List<RecentSearches>)System.Web.HttpContext.Current.Session["RecentSearchesList"];
            }
            return RValue;
        }


        //function to Initialize & return AllTravelersList
        public static List<GetTravelersResult> GetAllTravelers()
        {
            if (System.Web.HttpContext.Current.Session["AllTravelersList"] == null)
            {
                using (AirlinesDBMLDataContext AObj = new AirlinesDBMLDataContext())
                {
                    System.Web.HttpContext.Current.Session["AllTravelersList"] = AObj.GetTravelers(null, null, null, true).ToList();
                }
            }
            List<GetTravelersResult> RValue = (List<GetTravelersResult>)System.Web.HttpContext.Current.Session["AllTravelersList"];

            return RValue;
        }

        //function to Initialize & return AllBookingsList
        public static List<GetBookingsResult> GetAllBookings()
        {
            if (System.Web.HttpContext.Current.Session["AllBookingsList"] == null)
            {
                using (AirlinesDBMLDataContext AObj = new AirlinesDBMLDataContext())
                {
                    System.Web.HttpContext.Current.Session["AllBookingsList"] = AObj.GetBookings(null, null, null, null, null, null, null, null, true).ToList();
                }
            }
            List<GetBookingsResult> RValue = (List<GetBookingsResult>)System.Web.HttpContext.Current.Session["AllBookingsList"];
            return RValue;
        }

        //function to Initialize & return AllBookingsDetailsList
        public static List<GetBookingsDetailsResult> GetAllBookingsDetails()
        {
            if (System.Web.HttpContext.Current.Session["AllBookingsDetailsList"] == null)
            {
                using (AirlinesDBMLDataContext AObj = new AirlinesDBMLDataContext())
                {
                    System.Web.HttpContext.Current.Session["AllBookingsDetailsList"] = AObj.GetBookingsDetails(null, null).ToList();
                }
            }
            List<GetBookingsDetailsResult> RValue = (List<GetBookingsDetailsResult>)System.Web.HttpContext.Current.Session["AllBookingsDetailsList"];
            return RValue;
        }

        //function to Initialize & return UserProfile
        public static GetUserDetailsResult GetUserProfile()
        {
            if (System.Web.HttpContext.Current.Session["MyProfile"] == null)
            {
                using (AirlinesDBMLDataContext AObj = new AirlinesDBMLDataContext())
                {
                    System.Web.HttpContext.Current.Session["MyProfile"] = AObj.GetUserDetails(null, null, null, null).ToList()[0];
                }
            }
            GetUserDetailsResult RValue = (GetUserDetailsResult)System.Web.HttpContext.Current.Session["MyProfile"];
            return RValue;
        }

        //function to Initialize & return AirportList
        public static List<GetDestListResult> GetDestList()
        {
            if (System.Web.HttpContext.Current.Session["DestList"] == null)
            {
                using (AirlinesDBMLDataContext AObj = new AirlinesDBMLDataContext())
                {
                    System.Web.HttpContext.Current.Session["DestList"] = AObj.GetDestList(null, null, null, null).ToList();
                }
            }
            List<GetDestListResult> RValue = (List<GetDestListResult>)System.Web.HttpContext.Current.Session["DestList"];
            return RValue;
        }

        //function to return DepartFlightList
        public static List<SearchFlight> DepartFlightList()
        {
            List<SearchFlight> RValue = new List<SearchFlight>();
            if (System.Web.HttpContext.Current.Session["DepartFlightList"] != null)
                RValue = (List<SearchFlight>)System.Web.HttpContext.Current.Session["DepartFlightList"];
            return RValue;
        }

        //function to return ReturnFlightList
        public static List<SearchFlight> ReturnFlightList()
        {
            List<SearchFlight> RValue = new List<SearchFlight>();
            if (System.Web.HttpContext.Current.Session["ReturnFlightList"] != null)
                RValue = (List<SearchFlight>)System.Web.HttpContext.Current.Session["ReturnFlightList"];
            return RValue;
        }

        //function to return SelectedTripInfo
        public static TripInfoC GetSelTripInfo()
        {
            TripInfoC RValue = new TripInfoC();
            if (System.Web.HttpContext.Current.Session["TripInfo"] != null)
                RValue = (TripInfoC)System.Web.HttpContext.Current.Session["TripInfo"];
            return RValue;
        }

        //function to return Selected DepartFlight
        public static SearchFlight DepartFlight()
        {
            SearchFlight RValue = new SearchFlight();
            if (System.Web.HttpContext.Current.Session["DepartFlight"] != null)
                RValue = (SearchFlight)System.Web.HttpContext.Current.Session["DepartFlight"];
            return RValue;
        }

        //function to return Selected ReturnFlight
        public static SearchFlight ReturnFlight()
        {
            SearchFlight RValue = new SearchFlight();
            if (System.Web.HttpContext.Current.Session["ReturnFlight"] != null)
                RValue = (SearchFlight)System.Web.HttpContext.Current.Session["ReturnFlight"];
            return RValue;
        }

        //function to return Selected AdultTravelersList
        public static List<TravelerList> SelAdultList()
        {
            List<TravelerList> RValue = new List<TravelerList>();
            if (System.Web.HttpContext.Current.Session["SelAdultList"] != null)
                RValue = (List<TravelerList>)System.Web.HttpContext.Current.Session["SelAdultList"];
            return RValue;
        }

        //function to return Selected ChildTravelersList
        public static List<TravelerList> SelChildList()
        {
            List<TravelerList> RValue = new List<TravelerList>();
            if (System.Web.HttpContext.Current.Session["SelChildList"] != null)
                RValue = (List<TravelerList>)System.Web.HttpContext.Current.Session["SelChildList"];
            return RValue;
        }

        //function to return Selected InfantTravelersList
        public static List<TravelerList> SelInfantList()
        {
            List<TravelerList> RValue = new List<TravelerList>();
            if (System.Web.HttpContext.Current.Session["SelInfantList"] != null)
                RValue = (List<TravelerList>)System.Web.HttpContext.Current.Session["SelInfantList"];
            return RValue;
        }

        //function to return TitlesList
        public static List<string> GetTitleList()
        {
            List<string> TitleList = new List<string>();
            TitleList.Add("Mr.");
            TitleList.Add("Mrs.");
            TitleList.Add("Ms.");
            TitleList.Add("Master");
            TitleList.Add("Miss.");
            return TitleList;
        }

        //function to return TitlesList for Adult
        public static List<string> GetAdultTitleList()
        {
            List<string> TitleList = new List<string>();
            TitleList.Add("Mr.");
            TitleList.Add("Mrs.");
            TitleList.Add("Ms.");
            return TitleList;
        }

        //function to return TitlesList for Child/Infant
        public static List<string> GetChildTitleList()
        {
            List<string> TitleList = new List<string>();
            TitleList.Add("Master");
            TitleList.Add("Miss.");
            return TitleList;
        }

        //function to Add/Update Traveler
        public static long AddUpdateTraveler(long TravelerId, string Title, string FName, string MName, string LName, string DOBStr, string Email, string ISD, string Phone)
        {
            try
            {
                if (TravelerId == 0)
                {
                    DateTime? DOB = null;
                    if (DOBStr != null)
                        DOB = DateTime.Parse(DOBStr);
                    List<GetTravelersResult> AllTravelersList = GetAllTravelers();
                    long NewID = AllTravelersList.Max(x => x.Id) + 1;
                    AllTravelersList.Add(new GetTravelersResult { Id = NewID, Reg_Date = DateTime.Now, Title = Title, First_Name = FName, Middle_Name = MName, Last_Name = LName, User_Name = FName + " " + MName + " " + LName, DOB = DOB, Email_ID = Email, Mobile = Phone });
                    System.Web.HttpContext.Current.Session["AllTravelersList"] = AllTravelersList;
                    return NewID;
                }
                else if (TravelerId > 0)
                {
                    DateTime? DOB = null;
                    if (DOBStr != null)
                        DOB = DateTime.Parse(DOBStr);
                    List<GetTravelersResult> AllTravelersList = GetAllTravelers();
                    AllTravelersList.Where(x => x.Id == TravelerId).ToList().ForEach(x =>
                    {
                        x.Title = Title;
                        x.First_Name = FName;
                        x.Middle_Name = MName;
                        x.Last_Name = LName;
                        x.User_Name = FName + " " + MName + " " + LName;
                        x.DOB = DOB;
                        x.Email_ID = Email;
                        x.ISD = ISD;
                        x.Mobile = Phone;
                    });
                    System.Web.HttpContext.Current.Session["AllTravelersList"] = AllTravelersList;
                    return TravelerId;
                }
                else
                    return -1;
            }
            catch
            {
                return -1;
            }
        }

        //constructor
        public Common()
        {
            using (AirlinesDBMLDataContext AObj = new AirlinesDBMLDataContext())
            {
                string ErrMsg = "";
                AObj.SetForDemo(ref ErrMsg);
            }
            GetDestList();
            GetUserProfile();
            GetAllTravelers();
            GetAllBookings();
            GetAllBookingsDetails();
        }

    }


}