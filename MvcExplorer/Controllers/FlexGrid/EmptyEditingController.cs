using C1.Web.Mvc;
using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C1.Web.Mvc.Serialization;
using System.Data;
using System.Collections;
using System.Globalization;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        
        private static IDictionary<string, object[]> _getClientSettings()
        {
            return new Dictionary<string, object[]>
            {
                {"CommitEmptyEdits", new object[]{true, false }}
            };
        }

        private static IDictionary<string, object> _getDefaultValues()
        {
            var defaultValues = new Dictionary<string, object>
            {
                {"CommitEmptyEdits", true}
            };

            return defaultValues;
        }

        private static List<Sale> _sourceSale = Sale.GetDataContainNull(20).ToList<Sale>();


        public ActionResult EmptyEditing()
        {
            ViewBag.DemoSettings = true;
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = _getClientSettings(),
                DefaultValues = _getDefaultValues()
            };

            return View(_sourceSale);
        }

        

    }
}
