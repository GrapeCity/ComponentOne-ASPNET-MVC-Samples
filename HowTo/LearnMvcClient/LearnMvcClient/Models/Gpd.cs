using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnMvcClient.Models
{
    public class Gdp
    {
        public int Id { get; set; }
        public String Country { get; set; }
        public string Continent { get; set; }
        public int Gdpm { get; set; }
        public int Popk { get; set; }
        public int Gdpcap { get; set; }

        private static IEnumerable<Gdp> _data;
        public static IEnumerable<Gdp> GetData()
        {
            return _data ?? (_data = GetGdpData());
        }
        private static IEnumerable<Gdp> GetGdpData()
        {
            // list of country GDP and populations
            // https://en.wikipedia.org/wiki/List_of_IMF_ranked_countries_by_GDP

            var list = new List<Gdp>();
            list.Add(new Gdp { Id = 1, Country = "Luxembourg", Continent = "Europe", Gdpm = 57825, Popk = 563, Gdpcap = 102708 });
            list.Add(new Gdp { Id = 2, Country = "Switzerland", Continent = "Europe", Gdpm = 664005, Popk = 8238, Gdpcap = 80602 });
            list.Add(new Gdp { Id = 3, Country = "Norway", Continent = "Europe", Gdpm = 388315, Popk = 5205, Gdpcap = 74604 });
            list.Add(new Gdp { Id = 4, Country = "Macao", Continent = "Asia", Gdpm = 46178, Popk = 647, Gdpcap = 71372 });
            list.Add(new Gdp { Id = 5, Country = "Qatar", Continent = "Africa", Gdpm = 166908, Popk = 2421, Gdpcap = 68941 });
            list.Add(new Gdp { Id = 6, Country = "Ireland", Continent = "Europe", Gdpm = 283716, Popk = 4635, Gdpcap = 61211 });
            list.Add(new Gdp { Id = 7, Country = "United States", Continent = "America", Gdpm = 18036650, Popk = 321601, Gdpcap = 56083 });
            list.Add(new Gdp { Id = 8, Country = "Singapore", Continent = "Asia", Gdpm = 292734, Popk = 5535, Gdpcap = 52887 });
            list.Add(new Gdp { Id = 9, Country = "Denmark", Continent = "Europe", Gdpm = 295091, Popk = 5660, Gdpcap = 52136 });
            list.Add(new Gdp { Id = 10, Country = "Australia", Continent = "Oceania", Gdpm = 1225286, Popk = 23940, Gdpcap = 51181 });
            list.Add(new Gdp { Id = 11, Country = "Iceland", Continent = "Europe", Gdpm = 16718, Popk = 333, Gdpcap = 50204 });
            list.Add(new Gdp { Id = 12, Country = "Sweden", Continent = "Europe", Gdpm = 493042, Popk = 9851, Gdpcap = 50049 });
            list.Add(new Gdp { Id = 13, Country = "San Marino", Continent = "Europe", Gdpm = 1558, Popk = 31, Gdpcap = 50258 });
            list.Add(new Gdp { Id = 14, Country = "Netherlands", Continent = "Europe", Gdpm = 750696, Popk = 16937, Gdpcap = 44322 });
            list.Add(new Gdp { Id = 15, Country = "United Kingdom", Gdpm = 2858482, Popk = 65110, Gdpcap = 43902 });
            list.Add(new Gdp { Id = 16, Country = "Austria", Continent = "Europe", Gdpm = 374261, Popk = 8621, Gdpcap = 43412 });
            list.Add(new Gdp { Id = 17, Country = "Canada", Continent = "America", Gdpm = 1550537, Popk = 35825, Gdpcap = 43280 });
            list.Add(new Gdp { Id = 18, Country = "Finland", Continent = "Europe", Gdpm = 232077, Popk = 5472, Gdpcap = 42411 });
            list.Add(new Gdp { Id = 19, Country = "Germany", Continent = "Europe", Gdpm = 3365293, Popk = 82176, Gdpcap = 40952 });
            list.Add(new Gdp { Id = 20, Country = "Belgium", Continent = "Europe", Gdpm = 454288, Popk = 11209, Gdpcap = 40528 });
            list.Add(new Gdp { Id = 21, Country = "United Arab Emirates", Continent = "Africa", Gdpm = 370296, Popk = 9581, Gdpcap = 38648 });
            list.Add(new Gdp { Id = 22, Country = "France", Continent = "Europe", Gdpm = 2420163, Popk = 64275, Gdpcap = 37653 });
            list.Add(new Gdp { Id = 23, Country = "New Zealand", Continent = "Oceania", Gdpm = 172257, Popk = 4647, Gdpcap = 37068 });
            list.Add(new Gdp { Id = 24, Country = "Israel", Continent = "Africa", Gdpm = 299413, Popk = 8377, Gdpcap = 35742 });
            list.Add(new Gdp { Id = 25, Country = "Japan", Continent = "Asia", Gdpm = 4124211, Popk = 126981, Gdpcap = 32478 });
            list.Add(new Gdp { Id = 26, Country = "Brunei Darussalam", Continent = "Africa", Gdpm = 12930, Popk = 417, Gdpcap = 31007 });
            list.Add(new Gdp { Id = 27, Country = "Italy", Continent = "Europe", Gdpm = 1815759, Popk = 60796, Gdpcap = 29866 });
            list.Add(new Gdp { Id = 28, Country = "Puerto Rico", Continent = "America", Gdpm = 102906, Popk = 3474, Gdpcap = 29621 });
            list.Add(new Gdp { Id = 29, Country = "Kuwait", Continent = "Africa", Gdpm = 114079, Popk = 4110, Gdpcap = 27756 });
            list.Add(new Gdp { Id = 30, Country = "South Korea", Continent = "Asia", Gdpm = 1377873, Popk = 50617, Gdpcap = 27221 });
            list.Add(new Gdp { Id = 31, Country = "Spain", Continent = "Europe", Gdpm = 1199715, Popk = 46423, Gdpcap = 25843 });
            list.Add(new Gdp { Id = 32, Country = "The Bahamas", Continent = "America", Gdpm = 8854, Popk = 364, Gdpcap = 24324 });
            list.Add(new Gdp { Id = 33, Country = "Bahrain", Continent = "Africa", Gdpm = 31119, Popk = 1294, Gdpcap = 24048 });
            list.Add(new Gdp { Id = 34, Country = "Cyprus", Continent = "Europe", Gdpm = 19330, Popk = 847, Gdpcap = 22821 });
            list.Add(new Gdp { Id = 35, Country = "Malta", Continent = "Europe", Gdpm = 9752, Popk = 429, Gdpcap = 22731 });
            list.Add(new Gdp { Id = 36, Country = "Taiwan", Continent = "Asia", Gdpm = 523006, Popk = 23492, Gdpcap = 22263 });
            list.Add(new Gdp { Id = 37, Country = "Slovenia", Continent = "Europe", Gdpm = 42798, Popk = 2063, Gdpcap = 20745 });
            list.Add(new Gdp { Id = 38, Country = "Saudi Arabia", Continent = "Africa", Gdpm = 646002, Popk = 31386, Gdpcap = 20582 });
            list.Add(new Gdp { Id = 39, Country = "Portugal", Continent = "Europe", Gdpm = 199032, Popk = 10411, Gdpcap = 19117 });
            list.Add(new Gdp { Id = 40, Country = "Trinidad and Tobago", Continent = "America", Gdpm = 24631, Popk = 1358, Gdpcap = 18137 });
            list.Add(new Gdp { Id = 41, Country = "Greece", Continent = "Europe", Gdpm = 195320, Popk = 10858, Gdpcap = 17988 });
            list.Add(new Gdp { Id = 42, Country = "Czech Republic", Continent = "Europe", Gdpm = 185156, Popk = 10538, Gdpcap = 17570 });
            list.Add(new Gdp { Id = 43, Country = "Estonia", Continent = "Europe", Gdpm = 22704, Popk = 1313, Gdpcap = 17291 });
            list.Add(new Gdp { Id = 44, Country = "Equatorial Guinea", Continent = "Africa", Gdpm = 13819, Popk = 799, Gdpcap = 17295 });
            list.Add(new Gdp { Id = 45, Country = "Oman", Continent = "Europe", Gdpm = 64121, Popk = 3840, Gdpcap = 16698 });
            list.Add(new Gdp { Id = 46, Country = "St. Kitts and Nevis", Continent = "America", Gdpm = 915, Popk = 56, Gdpcap = 16339 });
            list.Add(new Gdp { Id = 47, Country = "Palau", Continent = "Asia", Gdpm = 287, Popk = 18, Gdpcap = 15944 });
            list.Add(new Gdp { Id = 48, Country = "Slovakia", Continent = "Europe", Gdpm = 86629, Popk = 5421, Gdpcap = 15980 });
            list.Add(new Gdp { Id = 49, Country = "Barbados", Continent = "America", Gdpm = 4385, Popk = 280, Gdpcap = 15660 });
            list.Add(new Gdp { Id = 50, Country = "Uruguay", Continent = "America", Gdpm = 53107, Popk = 3416, Gdpcap = 15546 });
            return list;
        }
    }
}