using C1.Web.Mvc;
using System;
using System.Linq;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

        public ActionResult EmptyEditing_Data()
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
