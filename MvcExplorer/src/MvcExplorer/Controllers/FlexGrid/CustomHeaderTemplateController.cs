using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
#if !NETCORE30 && !NET50
using Microsoft.AspNetCore.Http.Internal;
#endif
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;

namespace MvcExplorer.Controllers
{
    public class DataRepresentation
    {
        public DataRepresentation(params string[] args)
        {
            name = args[0];
            currency = args[1];
            ytd = args[2];
            m1 = args[3];
            m6 = args[4];
            m12 = args[5];
            stock = args[6];
            bond = args[7];
            cash = args[8];
            other = args[9];
        }

        public string name;
        public string currency;
        public string ytd;
        public string m1;
        public string m6;
        public string m12;
        public string stock;
        public string bond;
        public string cash;
        public string other;
    }

    public partial class FlexGridController : Controller
    {
        private static List<DataRepresentation> _data = new List<DataRepresentation>
        {
            new DataRepresentation("Constant Growth IXTR",  "USD",   "0.0523%", "0.0142%", "0.0443%", "0.0743%", "0.17%", "0.32%", "0.36%", "0.15%"),
            new DataRepresentation("Optimus Prime MMCT",    "EUR",  "3.43%",    "4.30%",    "2.44%",    "5.43%",    "61%",  "80%",  "90%",  "22%"),
            new DataRepresentation("Serenity Now ZTZZZ",    "YEN",  "5.22%",    "1.43%",    "4.58%",    "7.32%",    "66%",  "9%",   "19%",  "6%")
        };

        public ActionResult CustomHeaderTemplate_Bind([C1JsonRequest] CollectionViewRequest<DataRepresentation> DataRepresentation)
        {
            return this.C1Json(CollectionViewHelper.Read(DataRepresentation, _data));
        }

        public ActionResult CustomHeaderTemplate(IFormCollection collection)
        {
            return View();
        }
    }
}
