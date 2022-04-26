using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BookMyFlight.Models
{
    //MainModel Class
    public class MainModel
    {
        public string ErrMsg { get; set; }
        public IEnumerable<Common.RecentSearches> RecentSearchesList { get; set; }
        public string TripType { get; set; }
        public IEnumerable<GetDestListResult> TravelDestList { get; set; }
        public string FromDest { get; set; }
        public string ToDest { get; set; }
        public DateTime DepartDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public List<string> AdultCountList { get; set; }
        public List<string> ChildCountList { get; set; }
        public List<string> InfantCountList { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }
        public int Infant { get; set; }
        public List<string> ClassList { get; set; }
        public string Class { get; set; }
        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }
        public float PriceValue { get; set; }
        public double MinDepartTime { get; set; }
        public double MaxDepartTime { get; set; }
        public double DepartTimeValue { get; set; }
        public double MinReturnTime { get; set; }
        public double MaxReturnTime { get; set; }
        public double ReturnTimeValue { get; set; }
        public IEnumerable<GetAirlinesListResult> AirlinesList { get; set; }
        public List<string> TitleList { get; set; }
        public List<string> AdultTitleList { get; set; }
        public List<string> ChildTitleList { get; set; }
        public List<GetTravelersResult> TravelersList { get; set; }
        public List<Common.TravelerList> SelAdultList { get; set; }
        public List<Common.TravelerList> SelChildList { get; set; }
        public List<Common.TravelerList> SelInfantList { get; set; }
        public Common.TripInfoC SelTripInfo { get; set; }
        public List<Common.SearchFlight> DepartFlights { get; set; }
        public List<Common.SearchFlight> ReturnFlights { get; set; }
        public Common.SearchFlight SelDepartFlights { get; set; }
        public Common.SearchFlight SelReturnFlights { get; set; }

        //constructor
        public MainModel()
        {
            using (AirlinesDBMLDataContext Obj1 = new AirlinesDBMLDataContext())
            {
                TravelDestList = Common.GetDestList();
                AdultCountList = new List<string>();
                ChildCountList = new List<string>();
                InfantCountList = new List<string>();
                for (int i = 0; i <= 10; i++)
                {
                    if (i > 0)
                        AdultCountList.Add(i.ToString() + " Adult");
                    ChildCountList.Add(i.ToString() + " Child");
                    InfantCountList.Add(i.ToString() + " Infant");
                }
                ClassList = new List<string>();
                ClassList.Add("BUSINESS");
                ClassList.Add("ECONOMY");
                ClassList.Add("PREMIUM ECONOMY");
                ClassList.Add("FIRST CLASS");
                AirlinesList = Obj1.GetAirlinesList(0, null, true).ToList();
                GetAirlinesListResult T1 = new GetAirlinesListResult();
                T1.Airlines="All";
                T1.Airlines_Code="All";
                AirlinesList.ToList().Insert(0, T1);
                TitleList = Common.GetTitleList();
                AdultTitleList = Common.GetAdultTitleList();
                ChildTitleList = Common.GetChildTitleList();
                TravelersList = Common.GetAllTravelers();
            }
        }
    }
}