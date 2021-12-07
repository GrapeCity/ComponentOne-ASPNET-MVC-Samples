using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
#if !NETCORE31 && !NET50
using Microsoft.AspNetCore.Http.Internal;
#endif
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;

namespace TransposedGridExplorer.Models
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
        public double Total
        {
            get
            {
                return Stock + Bond + Other;
            }
        }
    }
}
