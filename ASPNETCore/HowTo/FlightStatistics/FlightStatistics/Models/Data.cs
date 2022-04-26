using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FlightStatistics.Models
{
    public class Data
    {
        //private static AirportEntities _db;

        //public Data(AirportEntities db)
        //{
        //    _db = db;
        //}

        public static string[] MonthList = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        public string[] YearList;
        public static int[] years = { DateTime.Today.Year - 4, DateTime.Today.Year - 3, DateTime.Today.Year - 2, DateTime.Today.Year - 1, DateTime.Today.Year };
        public static List<string> Regions = new List<string> { "Asia-Pacific", "Europe", "Latin America", "Middle East & Africa", "North America" };
        public static string[] AirportSize = new string[] { "" };
        public static List<string> Countries = new List<string>
            {
                "Afghanistan", "Albania", "Algeria", "American Samoa", "Andorra", "Angola", "Anguilla", "Antigua", "Argentina", "Armenia",
        "Aruba", "Australia", "Austria", "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize",
        "Benin", "Bermuda", "Bhutan", "Bolivia", "Bonaire", "Bosnia", "Botswana", "Brazil", "Brunei", "Bulgaria", "Burkina Faso", "Burundi",
        "Cambodia", "Cameroon", "Canada", "Canary Islands", "Cape Verde", "Cayman Islands", "Central African Republic", "Chad", "Channel Islands",
        "Chile", "China", "Christmas Island", "Cocos Island", "Colombia", "Comoros", "Congo", "Cook Islands", "Costa Rica", "Cote D'Ivoire",
        "Croatia", "Cuba", "Curacao", "Cyprus", "Czech Republic", "Denmark", "Djibouti", "Dominica", "Dominican Republic", "East Timor", "Ecuador",
        "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia", "Ethiopia", "Falkland Islands", "Faroe Islands", "Fiji", "Finland",
        "France", "French Guiana", "French Polynesia", "French Southern Ter", "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Gibraltar",
        "Great Britain", "Greece", "Greenland", "Grenada", "Guadeloupe", "Guam", "Guatemala", "Guinea", "Guyana", "Haiti", "Honduras",
        "Hong Kong", "Hungary", "Iceland", "India", "Indonesia", "Iran", "Iraq", "Ireland", "Isle of Man", "Israel", "Italy", "Jamaica", "Japan",
        "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Korea North", "Korea South", "Kuwait", "Kyrgyzstan", "Laos", "Latvia", "Lebanon", "Lesotho",
        "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Macau", "Macedonia", "Madagascar", "Malaysia", "Malawi", "Maldives",
        "Mali", "Malta", "Marshall Islands", "Martinique", "Mauritania", "Mauritius", "Mayotte", "Mexico", "Midway Islands", "Moldova", "Monaco",
        "Mongolia", "Montserrat", "Morocco", "Mozambique", "Myanmar", "Nambia", "Nauru", "Nepal", "Netherland Antilles", "Netherlands", "Nevis",
        "New Caledonia", "New Zealand", "Nicaragua", "Niger", "Nigeria", "Niue", "Norfolk Island", "Norway", "Oman", "Pakistan", "Palau Island",
        "Palestine", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Pitcairn Island", "Poland", "Portugal", "Puerto Rico",
        "Qatar", "Republic of Montenegro", "Republic of Serbia", "Reunion", "Romania", "Russia", "Rwanda", "St Barthelemy", "St Eustatius",
        "St Helena", "St Kitts-Nevis", "St Lucia", "St Maarten", "Saipan", "Samoa", "Samoa American", "San Marino", "Saudi Arabia", "Scotland",
        "Senegal", "Serbia", "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands", "Somalia", "South Africa",
        "Spain", "Sri Lanka", "Sudan", "Suriname", "Swaziland", "Sweden", "Switzerland", "Syria", "Tahiti", "Taiwan", "Tajikistan", "Tanzania",
        "Thailand", "Togo", "Tokelau", "Tonga", "Trinidad Tobago", "Tunisia", "Turkey", "Turkmenistan", "Turks & Caicos Is", "Tuvalu", "Uganda",
        "Ukraine", "United Arab Emirates", "United Kingdom", "United States of America", "Uruguay", "Uzbekistan", "Vanuatu", "Vatican City State",
        "Venezuela", "Vietnam", "Virgin Islands (British)", "Virgin Islands (USA)", "Wake Island", "Yemen", "Zaire", "Zambia", "Zimbabwe"
            };

        public static List<string> Cities = new List<string>
            {
                "Abidjan", "Accra", "Ahmedabad", "Alexandria", "Ankara", "Atlanta", "Baghdad", "Bandung", "Bangkok", "Barcelona", "Beijing", "Belo Horizonte",
        "Bengaluru", "Bogota", "Boston", "Buenos Aires", "Cairo", "Calcutta", "Chengdu", "Chennai", "Chicago", "Chongqung", "Dalian", "Dallas", "Delhi",
        "Detroit", "Dhaka", "Dongguan", "Essen", "Fuzhou", "Guadalajara", "Guangzhou", "Hangzhou", "Harbin", "Ho Chi Minh City", "Hong Kong", "Houston",
        "Hyderabad", "Istanbul", "Jakarta", "Johannesburg", "Karachi", "Khartoum", "Kinshasa", "Kuala Lumpur", "Lagos", "Lahore", "Lima", "London",
        "Los Angeles", "Luanda", "Madrid", "Manila", "Medellin", "Mexico City", "Miami", "Milan", "Monterrey", "Moscow", "Mumbai", "Nagoya", "Nanjing",
        "Naples", "New York", "Osaka", "Paris", "Pheonix", "Philadelphia", "Porto Alegre", "Pune", "Qingdao", "Quanzhou", "Recife", "Rio de Janeiro",
        "Riyadh", "Rome", "Saint Petersburg", "Salvador", "San Francisco", "Santiago", "Sao Paulo", "Seoul", "Shanghair", "Shenyang", "Shenzhen",
        "Singapore", "Surabaya", "Surat", "Suzhou", "Sydney", "Taipei", "Tehran", "Tianjin", "Toronto", "Washington", "Wuhan", "Xi'an-Xianyang", "Yangoon",
        "Zhengzhou", "Tokyo"
            };

        public static List<string> GetCountries()
        {
            return Countries;
        }
        public static List<string> GetCities()
        {
            return Cities;
        }
        public static IEnumerable<Airport> GetAirports(IEnumerable<Airport> airports, IEnumerable<AirportMonthlyData> airportMonthlyData)
        {
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

            var rand = new Random(0);

            List<Airport> List = new List<Airport>();

            foreach (Airport airport in airports)
            {
                var ap = airportMonthlyData.Where(x => x.AirportCode.Trim() == airport.AirportCode.Trim()); // && x.RecordedDate.Year == 2018
                List.Add(new Airport
                {
                    AirportCode = airport.AirportCode.Trim(),
                    AirportCity = myTI.ToTitleCase(airport.AirportCity.Trim().ToLower()),
                    CountryCode = airport.CountryCode.Trim(),
                    CountryName = airport.CountryName.Trim(),
                    Delayed = airport.Delayed,
                    AverageDelay = airport.AverageDelay,
                    Region = airport.Region,
                    Flights = airport.Flights,
                    UserRating = airport.UserRating,
                    CompletionFactor = airport.CompletionFactor,
                    MonthlyData = ap, //GetAirportMonthlyData(airportMonthlyData).Where(x => x.AirportCity == city),
                    OnTimeRanking = airport.OnTimeRanking
                });
            }

            //var cities = GetCities();
            //var countries = GetCountries();

            //List<int> rankings = new List<int>();

            //do
            //{
            //    var r = rand.Next(1, 101);
            //    if (!rankings.Contains(r))
            //        rankings.Add(r);
            //} while (rankings.Count < 100);

            //for (int i = 0; i < 100; i++)
            //{
            //    double d = rand.NextDouble();
            //    double c;
            //    while (d > 0.2)
            //    {
            //        d = rand.NextDouble();
            //    }

            //    c = 0.8 + d;
            //    var city = cities[rand.Next(cities.Count - 1)];

            //    List.Add(new Airport
            //    {
            //        AirportCode = "Code" + rand.Next(0, 100).ToString("D3"),
            //        AirportCity = city,
            //        CountryName = countries[rand.Next(countries.Count - 1)],
            //        Delayed = d,
            //        AverageDelay = rand.Next(15, 60) + rand.NextDouble(),
            //        Region = Regions[rand.Next(Regions.Count)],
            //        Flights = rand.Next(50, 15000),
            //        UserRating = rand.Next(3, 6),
            //        CompletionFactor = c,
            //        MonthlyData = GetAirportMonthlyData().Where(x => x.AirportCity == city),
            //        OnTimeRanking = rankings[i]
            //    });
            //}
            return List;
        }

        public static IEnumerable<AirportMonthlyData> GetAirportMonthlyData(IEnumerable<AirportMonthlyData> monthlyData)
        {
            Random rand = new Random(0);
            //var data = Data.GetAirports();

            List<AirportMonthlyData> airportMonthlyData = monthlyData.Select(x => new AirportMonthlyData
            {
                Region = x.Region,
                AirportCode = x.AirportCode,
                Delay = x.Delay,
                RecordedDate = DateTime.Now.AddDays(-rand.Next(1, 30))
            }).ToList();

            //List<AirportMonthlyData> airportMonthlyData = new List<AirportMonthlyData>();

            //foreach (string c in Cities)
            //{
            //    for (int i = 1; i <= 30; i++)
            //    {
            //        var month = rand.Next(1, 13);
            //        var maxDate = 0;
            //        if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            //            maxDate = 32;
            //        else
            //            maxDate = 29;

            //        airportMonthlyData.Add(new AirportMonthlyData()
            //        {
            //            AirportCity = c,
            //            Region = Regions[rand.Next(Regions.Count)],
            //            Delay = rand.Next(15, 60) + rand.NextDouble(),
            //            RecordedDate = new DateTime(2000 + rand.Next(14, 19), month, rand.Next(1, maxDate))
            //        });
            //    }
            //}
            return airportMonthlyData;
        }

        public static List<AirportsGroupedData> GetGroupedData(IEnumerable<Airport> airportsData)
        {
            Random rnd = new Random(0);
            List<AirportsGroupedData> data = airportsData.GroupBy(x => new { x.Region })
                .Select(group => new AirportsGroupedData
                {
                    Region = group.Key.Region,
                    AverageDelayAvg = group.Average(a => a.AverageDelay),
                    OnTimeAvg = group.Average(b => b.OnTime),
                    TotalFlights = group.Sum(c => c.Flights),
                    RecordedDate = DateTime.Now.AddDays(rnd.Next(1, 100))
                }).ToList();

            return data;
        }

        //public static List<Airport> GetTreeMapData()
        //{
        //    //InitAllRegions();
        //    var result = new List<Airport>();
        //    var rand = new Random(0);
        //    var rCount = Regions.Count;
        //    var cities = GetCities();

        //    for (int i = 0; i < 50; i++)
        //    {
        //        result.Add(new Airport()
        //        {
        //            Region = Regions[rand.Next(0, rCount - 1)],
        //            AirportCity = cities[rand.Next(0, cities.Count - 1)],
        //            AverageDelay = rand.NextDouble() * i
        //        });
        //    }
        //    return result;
        //}

        public static List<object> GetYears()
        {
            List<object> yrs = new List<object>();
            foreach (int i in years)
            {
                yrs.Add(i);
            }
            return yrs;
        }
    }
}
