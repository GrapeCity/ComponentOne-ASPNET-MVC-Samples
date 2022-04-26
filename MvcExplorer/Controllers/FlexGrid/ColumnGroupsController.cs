using System.Collections;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using C1.Web.Mvc;
using MvcExplorer.Models;
using System.Collections.Generic;
using System;

namespace MvcExplorer.Controllers
{
    public class Portfolio
    {
        public Portfolio(string name, string currency, params double[] args)
        {
            Name = name;
            Currency = currency;
            YTD = args[0];
            M1 = args[1];
            M6 = args[2];
            M12 = args[3];
            Stock = args[4];
            Bond = args[5];
            Cash = args[6];
            Other = args[7];
            Amount = args[8];
        }

        public string Name;
        public string Currency;
        public double YTD;
        public double M1;
        public double M6;
        public double M12;
        public double Stock;
        public double Bond;
        public double Cash;
        public double Other;
        public double Amount;
    }

    public partial class FlexGridController : Controller
    {
        private static List<Portfolio> _portfolios = new List<Portfolio>
        {
            new Portfolio("Constant Growth", "USD",   0.0523, 0.0142, 0.0443, 0.0743, 0.17, 0.50, 0.18, 0.15, 40500),
            new Portfolio("Optimus Prime",	"EUR",	0.343,	0.430,	0.244,	0.543,	0.33,	0.17,	0.28,	0.22, 100000),
            new Portfolio("Serenity Now",	"YEN",	0.522,	0.143,	0.458,	0.732,	0.15,	0.11,	0.35,	0.39, 255800),
            new Portfolio("Vina Capital",   "VND",  0.418,  0.231,  0.356,  0.648,  0.25,   0.31,   0.19,   0.25, 116900),
            new Portfolio("Dragon Capital",   "BTC",  0.116,  0.528,  0.451,  0.324,  0.15,   0.14,   0.71,   0, 278300)
        };

        public ActionResult ColumnGroups(FormCollection collection)
        {
            return View(_portfolios);
        }
    }
}