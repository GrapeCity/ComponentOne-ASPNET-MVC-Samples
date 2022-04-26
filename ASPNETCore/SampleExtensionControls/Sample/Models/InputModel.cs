using System.Collections.Generic;

namespace Sample.Models
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
