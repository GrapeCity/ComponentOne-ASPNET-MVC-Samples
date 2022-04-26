using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TransposedGridExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
#if !NETCORE31 && !NET50
using Microsoft.AspNetCore.Http.Internal;
#endif
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;

namespace TransposedGridExplorer.Controllers
{
    public partial class TransposedGridController : Controller
    {
        private static List<Portfolio> _portfolios = new List<Portfolio>
        {
            new Portfolio("Constant Growth", "USD",   0.0523, 0.0142, 0.0443, 0.0743, 0.17, 0.50, 0.18, 0.15, 40500),
            new Portfolio("Optimus Prime",  "EUR",  0.343,  0.430,  0.244,  0.543,  0.33,   0.17,   0.28,   0.22, 100000),
            new Portfolio("Serenity Now",   "YEN",  0.522,  0.143,  0.458,  0.732,  0.15,   0.11,   0.35,   0.39, 255800),
            new Portfolio("Vina Capital",   "VND",  0.418,  0.231,  0.356,  0.648,  0.25,   0.31,   0.19,   0.25, 116900),
            new Portfolio("Dragon Capital",   "BTC",  0.116,  0.528,  0.451,  0.324,  0.15,   0.14,   0.71,   0, 278300)
        };

        public IActionResult RowGrouping()
        {
            return View(_portfolios);
        }

    }
}
