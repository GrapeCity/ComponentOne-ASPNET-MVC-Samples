using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMyFlight.Models;


namespace BookMyFlight.Controllers
{
    public class MyProfileController : Controller
    {
        //MyProfile View controller
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Home/Login?CallP=MyProfile/Index");
            }
            long? UserID = null;
            if (Session["UserID"] != null)
                UserID = long.Parse(Session["UserID"].ToString());
            MyProfileModel ProfileObj = new MyProfileModel();
            ProfileObj.UserProfile = Common.GetUserProfile();
            return View(ProfileObj);
        }

        //To Update User Profile by Ajax Call
        [HttpPost]
        public JsonResult UpdateUserProfile(string Title, string FName, string LName, string Email, string ISD, string Phone, string Address, string Country, string State, string City, string PIN)
        {
            try
            {
                GetUserDetailsResult MyProfile = Common.GetUserProfile();
                MyProfile.Title = Title;
                MyProfile.First_Name = FName;
                MyProfile.Last_Name = LName;
                MyProfile.Email_ID = Email;
                MyProfile.ISD = ISD;
                MyProfile.Contact_No = Phone;
                MyProfile.Address1 = Address;
                MyProfile.Country = Country;
                MyProfile.State = State;
                MyProfile.City = City;
                MyProfile.Zip_Code = PIN;
                Session["MyProfile"] = MyProfile;
                return Json(new { ResultType = "Success", ErrMsg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ResultType = "Fail", ErrMsg = "Error occurred: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}