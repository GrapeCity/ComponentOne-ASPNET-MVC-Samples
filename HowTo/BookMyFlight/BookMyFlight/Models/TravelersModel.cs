using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BookMyFlight.Models
{
    //MyTravelers Model
    public class TravelersModel
    {
        public List<GetTravelersResult> TravelersList { get; set; }
        public List<string> TitleList { get; set; }

        //constructor
        public TravelersModel()
        {
            TitleList = Common.GetTitleList();
        }
    }
}