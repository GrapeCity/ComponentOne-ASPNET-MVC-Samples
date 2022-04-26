using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMyFlight.Models;


namespace BookMyFlight.Controllers
{
    public class MyTravelersController : Controller
    {
        //MyTravelers View controller
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Home/Login?CallP=MyTravelers/Index");
            }
            TravelersModel ModelObj = new TravelersModel();
            return View(ModelObj);
        }

        //To Get Travelers Records by Ajax Call
        [HttpPost]
        public JsonResult GetTravelers(long? TravelerId, long? UserId, string Name, bool? Active)
        {
            try
            {
                List<GetTravelersResult> TravelersList = Common.GetAllTravelers();
                return Json(new { ResultType = "Success", ErrMsg = "", TravelersList = TravelersList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ResultType = "Fail", ErrMsg = "Error occurred: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //To Add New Traveler and update an existing Traveler by Ajax Call
        [HttpPost]
        public JsonResult AddUpdateTraveler(long TravelerId, string Title, string FName, string MName, string LName, string DOBStr, string Email, string ISD, string Phone)
        {
            try
            {
                if (TravelerId < 0)
                    return Json(new { ResultType = "Fail", ErrMsg = "Please! Select a Traveller to update." }, JsonRequestBehavior.AllowGet);
                long RetID = Common.AddUpdateTraveler(TravelerId, Title, FName, MName, LName, DOBStr, Email, ISD, Phone);
                if (RetID > 0)
                    return Json(new { ResultType = "Success", ErrMsg = "", TravelersList = Common.GetAllTravelers() }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { ResultType = "Fail", ErrMsg = "Unknown error occurred." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ResultType = "Fail", ErrMsg = "Error occurred: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //To delete a Traveler by Ajax Call
        [HttpPost]
        public JsonResult DeleteTraveler(long TravelerId)
        {
            try
            {
                if (TravelerId > 0)
                {
                    if(Common.GetAllBookingsDetails().Where(x=>x.Traveller_Id==TravelerId).Count()>0)
                        return Json(new { ResultType = "Fail", ErrMsg = "Requested Traveller can't be deleted because this has been already used in previous booking(s)." }, JsonRequestBehavior.AllowGet);
                    List<GetTravelersResult> AllTravelersList = Common.GetAllTravelers();                        
                    GetTravelersResult ItemToDel = AllTravelersList.Find(x => x.Id == TravelerId);
                    AllTravelersList.Remove(ItemToDel);
                    Session["AllTravelersList"] = AllTravelersList;
                    return Json(new { ResultType = "Success", ErrMsg = "", TravelersList = AllTravelersList }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { ResultType = "Fail", ErrMsg = "Please! Select a Traveller to delete." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ResultType = "Fail", ErrMsg = "Error occurred: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}