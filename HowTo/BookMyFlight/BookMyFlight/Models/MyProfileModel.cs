using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BookMyFlight.Models
{
    //MyProfile Model
    public class MyProfileModel
    {
        public List<string> TitleList { get; set; }
        public GetUserDetailsResult UserProfile { get; set; }

        //constructor
        public MyProfileModel()
        {
            TitleList = Common.GetTitleList();
        }
    }
}