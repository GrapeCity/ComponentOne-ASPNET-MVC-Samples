using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookMyFlight.Models
{
    //Mybooking Model
    public class MyBookingsModel
    {
        public string[] MonthList = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        public string[] YearList;
        public int SelFromMonth { get; set; }
        public int SelFromYear { get; set; }
        public int SelToMonth { get; set; }
        public int SelToYear { get; set; }
        public DateTime TravelFromDate { get; set; }
        public DateTime TravelToDate { get; set; }

        //constructor
        public MyBookingsModel()
        {

        }
    }
}