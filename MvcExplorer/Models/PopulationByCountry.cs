using System.Collections.Generic;

namespace MvcExplorer.Models
{
    public class PopulationByCountry
    {
        public string Country { get; set; }
        public int Population { get; set; }
        public static List<PopulationByCountry> GetData()
        {
            string[] countries = "China,India,USA,Indonesia,Brazil".Split(new char[] { ',' });
            int[] population = new int[] { 1380, 1310, 325, 260, 206 };

            var data = new List<PopulationByCountry>();
            for (var i = 0; i < countries.Length; i++)
            {
                data.Add(new PopulationByCountry()
                {
                    Country = countries[i],
                    Population = population[i]
                });
            }
            return data;
        }
    }
}