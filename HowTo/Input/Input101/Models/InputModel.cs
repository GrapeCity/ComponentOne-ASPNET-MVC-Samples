using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Input101.Models
{
    public class InputModel
    {
        public List<string> CountryList = new List<string>();
        public List<string> CitiesList = new List<string>();
        public InputModel()
        {
            CountryList = StaticModel.GetCountries();
            CitiesList = StaticModel.GetCities();
        }
    }
}