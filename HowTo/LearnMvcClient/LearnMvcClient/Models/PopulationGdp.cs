using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnMvcClient.Models
{
    public class PopulationGdp
    {
        public string Country { get; set; }
        public int Pop { get; set; }
        public int Gdp { get; set; }
        public double Pci { get; set; }

        private static IEnumerable<PopulationGdp> _data;
        public static IEnumerable<PopulationGdp> GetData()
        {
            return _data ?? (_data = GetPopData());
        }

        private static IEnumerable<PopulationGdp> GetPopData()
        {
            // population and gross domestic product for about 200 countries
            // Sources:
            //  https://simple.wikipedia.org/wiki/List_of_countries_by_population
            //  https://en.wikipedia.org/wiki/List_of_countries_by_GDP_(nominal)

            var list = new List<PopulationGdp>();
            list.Add(new PopulationGdp { Country = "United States", Pop = 320723000, Gdp = 17348075 });
            list.Add(new PopulationGdp { Country = "China", Pop = 1357000000, Gdp = 10356508 });
            list.Add(new PopulationGdp { Country = "Japan", Pop = 127300000, Gdp = 4602367 });
            list.Add(new PopulationGdp { Country = "Germany", Pop = 80620000, Gdp = 3874437 });
            list.Add(new PopulationGdp { Country = "United Kingdom", Pop = 64596800, Gdp = 2950039 });
            list.Add(new PopulationGdp { Country = "France", Pop = 65821885, Gdp = 2833687 });
            list.Add(new PopulationGdp { Country = "Brazil", Pop = 200400000, Gdp = 2346583 });
            list.Add(new PopulationGdp { Country = "Italy", Pop = 60705991, Gdp = 2147744 });
            list.Add(new PopulationGdp { Country = "India", Pop = 1252000000, Gdp = 2051228 });
            list.Add(new PopulationGdp { Country = "Russia", Pop = 143500000, Gdp = 1860598 });
            list.Add(new PopulationGdp { Country = "Canada", Pop = 34482779, Gdp = 1785387 });
            list.Add(new PopulationGdp { Country = "Australia", Pop = 24170869, Gdp = 1442722 });
            list.Add(new PopulationGdp { Country = "South Korea", Pop = 48219000, Gdp = 1410383 });
            list.Add(new PopulationGdp { Country = "Spain", Pop = 46162024, Gdp = 1406538 });
            list.Add(new PopulationGdp { Country = "Mexico", Pop = 122300000, Gdp = 1291062 });
            list.Add(new PopulationGdp { Country = "Indonesia", Pop = 249900000, Gdp = 888648 });
            list.Add(new PopulationGdp { Country = "Netherlands", Pop = 16715489, Gdp = 880716 });
            list.Add(new PopulationGdp { Country = "Turkey", Pop = 76667864, Gdp = 798332 });
            list.Add(new PopulationGdp { Country = "Saudi Arabia", Pop = 27136977, Gdp = 746248 });
            list.Add(new PopulationGdp { Country = "Switzerland", Pop = 8039060, Gdp = 703852 });
            list.Add(new PopulationGdp { Country = "Nigeria", Pop = 173600000, Gdp = 573999 });
            list.Add(new PopulationGdp { Country = "Sweden", Pop = 9471174, Gdp = 570591 });
            list.Add(new PopulationGdp { Country = "Poland", Pop = 38092000, Gdp = 547894 });
            list.Add(new PopulationGdp { Country = "Argentina", Pop = 40117096, Gdp = 543061 });
            list.Add(new PopulationGdp { Country = "Belgium", Pop = 10839905, Gdp = 534230 });
            list.Add(new PopulationGdp { Country = "Taiwan", Pop = 23197947, Gdp = 529597 });
            list.Add(new PopulationGdp { Country = "Norway", Pop = 5316800, Gdp = 499817 });
            list.Add(new PopulationGdp { Country = "Austria", Pop = 8419776, Gdp = 437582 });
            list.Add(new PopulationGdp { Country = "Iran", Pop = 80117000, Gdp = 416490 });
            list.Add(new PopulationGdp { Country = "Thailand", Pop = 69519000, Gdp = 404824 });
            list.Add(new PopulationGdp { Country = "United Arab Emirates", Pop = 8264070, Gdp = 399451 });
            list.Add(new PopulationGdp { Country = "Colombia", Pop = 48389000, Gdp = 377867 });
            list.Add(new PopulationGdp { Country = "South Africa", Pop = 50586757, Gdp = 350082 });
            list.Add(new PopulationGdp { Country = "Denmark", Pop = 5579204, Gdp = 342362 });
            list.Add(new PopulationGdp { Country = "Malaysia", Pop = 28334135, Gdp = 338108 });
            list.Add(new PopulationGdp { Country = "Singapore", Pop = 5183700, Gdp = 307872 });
            list.Add(new PopulationGdp { Country = "Israel", Pop = 7798600, Gdp = 305673 });
            list.Add(new PopulationGdp { Country = "Hong Kong", Pop = 7108100, Gdp = 290896 });
            list.Add(new PopulationGdp { Country = "Egypt", Pop = 88094000, Gdp = 286435 });
            list.Add(new PopulationGdp { Country = "Philippines", Pop = 98390000, Gdp = 284618 });
            list.Add(new PopulationGdp { Country = "Finland", Pop = 5394389, Gdp = 272649 });
            list.Add(new PopulationGdp { Country = "Chile", Pop = 17248450, Gdp = 258017 });
            list.Add(new PopulationGdp { Country = "Ireland", Pop = 4581269, Gdp = 250814 });
            list.Add(new PopulationGdp { Country = "Pakistan", Pop = 191282000, Gdp = 246849 });
            list.Add(new PopulationGdp { Country = "Greece", Pop = 10787690, Gdp = 237970 });
            list.Add(new PopulationGdp { Country = "Portugal", Pop = 10555853, Gdp = 229948 });
            list.Add(new PopulationGdp { Country = "Iraq", Pop = 32105000, Gdp = 223508 });
            list.Add(new PopulationGdp { Country = "Kazakhstan", Pop = 16615000, Gdp = 216036 });
            list.Add(new PopulationGdp { Country = "Algeria", Pop = 36300000, Gdp = 213518 });
            list.Add(new PopulationGdp { Country = "Qatar", Pop = 1699435, Gdp = 210109 });
            list.Add(new PopulationGdp { Country = "Venezuela", Pop = 31220000, Gdp = 206252 });
            list.Add(new PopulationGdp { Country = "Czech Republic", Pop = 10542080, Gdp = 205270 });
            list.Add(new PopulationGdp { Country = "Peru", Pop = 29797694, Gdp = 202642 });
            list.Add(new PopulationGdp { Country = "Romania", Pop = 21436000, Gdp = 199093 });
            list.Add(new PopulationGdp { Country = "New Zealand", Pop = 4577100, Gdp = 197502 });
            list.Add(new PopulationGdp { Country = "Vietnam", Pop = 89700000, Gdp = 185897 });
            list.Add(new PopulationGdp { Country = "Bangladesh", Pop = 156600000, Gdp = 183824 });
            list.Add(new PopulationGdp { Country = "Kuwait", Pop = 2818000, Gdp = 172608 });
            list.Add(new PopulationGdp { Country = "Hungary", Pop = 9985722, Gdp = 136989 });
            list.Add(new PopulationGdp { Country = "Ukraine", Pop = 45668028, Gdp = 130660 });
            list.Add(new PopulationGdp { Country = "Angola", Pop = 19618000, Gdp = 129326 });
            list.Add(new PopulationGdp { Country = "Morocco", Pop = 33803400, Gdp = 110009 });
            list.Add(new PopulationGdp { Country = "Ecuador", Pop = 14483499, Gdp = 100543 });
            list.Add(new PopulationGdp { Country = "Slovakia", Pop = 5437126, Gdp = 99869 });
            list.Add(new PopulationGdp { Country = "Oman", Pop = 2773479, Gdp = 77779 });
            list.Add(new PopulationGdp { Country = "Syria", Pop = 23362000, Gdp = 77460 });
            list.Add(new PopulationGdp { Country = "Belarus", Pop = 9468000, Gdp = 76139 });
            list.Add(new PopulationGdp { Country = "Sri Lanka", Pop = 20653000, Gdp = 74924 });
            list.Add(new PopulationGdp { Country = "Sudan", Pop = 30894000, Gdp = 74766 });
            list.Add(new PopulationGdp { Country = "Azerbaijan", Pop = 9111100, Gdp = 74145 });
            list.Add(new PopulationGdp { Country = "Luxembourg", Pop = 511840, Gdp = 65683 });
            list.Add(new PopulationGdp { Country = "Dominican Republic", Pop = 9378818, Gdp = 64058 });
            list.Add(new PopulationGdp { Country = "Myanmar", Pop = 48337000, Gdp = 63135 });
            list.Add(new PopulationGdp { Country = "Uzbekistan", Pop = 28000000, Gdp = 62613 });
            list.Add(new PopulationGdp { Country = "Kenya", Pop = 38610097, Gdp = 60937 });
            list.Add(new PopulationGdp { Country = "Guatemala", Pop = 14713763, Gdp = 58728 });
            list.Add(new PopulationGdp { Country = "Uruguay", Pop = 3368595, Gdp = 57471 });
            list.Add(new PopulationGdp { Country = "Croatia", Pop = 4290612, Gdp = 57073 });
            list.Add(new PopulationGdp { Country = "Bulgaria", Pop = 7364570, Gdp = 55824 });
            list.Add(new PopulationGdp { Country = "Ethiopia", Pop = 94100000, Gdp = 54809 });
            list.Add(new PopulationGdp { Country = "Lebanon", Pop = 4259000, Gdp = 50028 });
            list.Add(new PopulationGdp { Country = "Slovenia", Pop = 2081660, Gdp = 49570 });
            list.Add(new PopulationGdp { Country = "Costa Rica", Pop = 4563539, Gdp = 49553 });
            list.Add(new PopulationGdp { Country = "Tunisia", Pop = 10673800, Gdp = 48633 });
            list.Add(new PopulationGdp { Country = "Lithuania", Pop = 3207100, Gdp = 48288 });
            list.Add(new PopulationGdp { Country = "Tanzania", Pop = 43188000, Gdp = 48089 });
            list.Add(new PopulationGdp { Country = "Turkmenistan", Pop = 5105000, Gdp = 47932 });
            list.Add(new PopulationGdp { Country = "Serbia", Pop = 7120666, Gdp = 43866 });
            list.Add(new PopulationGdp { Country = "Panama", Pop = 3405813, Gdp = 43777 });
            list.Add(new PopulationGdp { Country = "Yemen", Pop = 23833000, Gdp = 43229 });
            list.Add(new PopulationGdp { Country = "Libya", Pop = 6423000, Gdp = 41148 });
            list.Add(new PopulationGdp { Country = "Ghana", Pop = 24233431, Gdp = 38616 });
            list.Add(new PopulationGdp { Country = "Democratic Republic of the Congo", Pop = 67758000, Gdp = 35918 });
            list.Add(new PopulationGdp { Country = "Jordan", Pop = 6783300, Gdp = 35878 });
            list.Add(new PopulationGdp { Country = "Bahrain", Pop = 1234571, Gdp = 33862 });
            list.Add(new PopulationGdp { Country = "Côte d\'Ivoire", Pop = 21395000, Gdp = 33741 });
            list.Add(new PopulationGdp { Country = "Bolivia", Pop = 10426154, Gdp = 33237 });
            list.Add(new PopulationGdp { Country = "Latvia", Pop = 2209000, Gdp = 31972 });
            list.Add(new PopulationGdp { Country = "Cameroon", Pop = 19406100, Gdp = 31777 });
            list.Add(new PopulationGdp { Country = "Paraguay", Pop = 6337127, Gdp = 30220 });
            list.Add(new PopulationGdp { Country = "Trinidad and Tobago", Pop = 1317714, Gdp = 28874 });
            list.Add(new PopulationGdp { Country = "Uganda", Pop = 32939800, Gdp = 27616 });
            list.Add(new PopulationGdp { Country = "Zambia", Pop = 13046508, Gdp = 26611 });
            list.Add(new PopulationGdp { Country = "Estonia", Pop = 1340194, Gdp = 26506 });
            list.Add(new PopulationGdp { Country = "El Salvador", Pop = 6227000, Gdp = 25164 });
            list.Add(new PopulationGdp { Country = "Cyprus", Pop = 803200, Gdp = 23263 });
            list.Add(new PopulationGdp { Country = "Afghanistan", Pop = 32358000, Gdp = 20444 });
            list.Add(new PopulationGdp { Country = "Nepal", Pop = 26620809, Gdp = 19761 });
            list.Add(new PopulationGdp { Country = "Honduras", Pop = 8215313, Gdp = 19511 });
            list.Add(new PopulationGdp { Country = "Gabon", Pop = 1534000, Gdp = 18209 });
            list.Add(new PopulationGdp { Country = "Bosnia and Herzegovina", Pop = 3843126, Gdp = 18165 });
            list.Add(new PopulationGdp { Country = "Brunei", Pop = 422700, Gdp = 17104 });
            list.Add(new PopulationGdp { Country = "Iceland", Pop = 318452, Gdp = 17036 });
            list.Add(new PopulationGdp { Country = "Papua New Guinea", Pop = 7014000, Gdp = 16809 });
            list.Add(new PopulationGdp { Country = "Mozambique", Pop = 23049621, Gdp = 16684 });
            list.Add(new PopulationGdp { Country = "Cambodia", Pop = 13395682, Gdp = 16551 });
            list.Add(new PopulationGdp { Country = "Georgia", Pop = 4469200, Gdp = 16536 });
            list.Add(new PopulationGdp { Country = "Senegal", Pop = 12855153, Gdp = 15683 });
            list.Add(new PopulationGdp { Country = "Equatorial Guinea", Pop = 720000, Gdp = 15530 });
            list.Add(new PopulationGdp { Country = "Botswana", Pop = 2031000, Gdp = 15217 });
            list.Add(new PopulationGdp { Country = "South Sudan", Pop = 8260490, Gdp = 14304 });
            list.Add(new PopulationGdp { Country = "Chad", Pop = 10329208, Gdp = 13945 });
            list.Add(new PopulationGdp { Country = "Zimbabwe", Pop = 12754000, Gdp = 13833 });
            list.Add(new PopulationGdp { Country = "Jamaica", Pop = 2705800, Gdp = 13709 });
            list.Add(new PopulationGdp { Country = "Namibia", Pop = 2324000, Gdp = 13632 });
            list.Add(new PopulationGdp { Country = "Republic of the Congo", Pop = 4140000, Gdp = 13552 });
            list.Add(new PopulationGdp { Country = "Albania", Pop = 3194972, Gdp = 13276 });
            list.Add(new PopulationGdp { Country = "Mauritius", Pop = 1280924, Gdp = 12588 });
            list.Add(new PopulationGdp { Country = "Burkina Faso", Pop = 15730977, Gdp = 12503 });
            list.Add(new PopulationGdp { Country = "Mali", Pop = 14517176, Gdp = 12094 });
            list.Add(new PopulationGdp { Country = "Mongolia", Pop = 2736800, Gdp = 12037 });
            list.Add(new PopulationGdp { Country = "Nicaragua", Pop = 5815524, Gdp = 11806 });
            list.Add(new PopulationGdp { Country = "Laos", Pop = 6348800, Gdp = 11681 });
            list.Add(new PopulationGdp { Country = "Armenia", Pop = 3266300, Gdp = 11644 });
            list.Add(new PopulationGdp { Country = "Macedonia", Pop = 2057284, Gdp = 11342 });
            list.Add(new PopulationGdp { Country = "Madagascar", Pop = 18866000, Gdp = 10674 });
            list.Add(new PopulationGdp { Country = "Malta", Pop = 417617, Gdp = 10514 });
            list.Add(new PopulationGdp { Country = "Tajikistan", Pop = 7616000, Gdp = 9242 });
            list.Add(new PopulationGdp { Country = "Haiti", Pop = 10085214, Gdp = 8711 });
            list.Add(new PopulationGdp { Country = "Benin", Pop = 9100000, Gdp = 8685 });
            list.Add(new PopulationGdp { Country = "Bahamas", Pop = 353658, Gdp = 8511 });
            list.Add(new PopulationGdp { Country = "Niger", Pop = 15730754, Gdp = 8024 });
            list.Add(new PopulationGdp { Country = "Moldova", Pop = 3560400, Gdp = 7962 });
            list.Add(new PopulationGdp { Country = "Rwanda", Pop = 10412826, Gdp = 7897 });
            list.Add(new PopulationGdp { Country = "Kyrgyzstan", Pop = 5477600, Gdp = 7402 });
            list.Add(new PopulationGdp { Country = "Kosovo", Pop = 1870981, Gdp = 7319 });
            list.Add(new PopulationGdp { Country = "Guinea", Pop = 10217591, Gdp = 6707 });
            list.Add(new PopulationGdp { Country = "Malawi", Pop = 13077160, Gdp = 6055 });
            list.Add(new PopulationGdp { Country = "Suriname", Pop = 529000, Gdp = 5210 });
            list.Add(new PopulationGdp { Country = "Mauritania", Pop = 3340627, Gdp = 5081 });
            list.Add(new PopulationGdp { Country = "Timor-Leste", Pop = 1066409, Gdp = 4970 });
            list.Add(new PopulationGdp { Country = "Sierra Leone", Pop = 5997000, Gdp = 4815 });
            list.Add(new PopulationGdp { Country = "Togo", Pop = 5753324, Gdp = 4594 });
            list.Add(new PopulationGdp { Country = "Montenegro", Pop = 620029, Gdp = 4551 });
            list.Add(new PopulationGdp { Country = "Swaziland", Pop = 1203000, Gdp = 4416 });
            list.Add(new PopulationGdp { Country = "Barbados", Pop = 276302, Gdp = 4354 });
            list.Add(new PopulationGdp { Country = "Fiji", Pop = 868000, Gdp = 4289 });
            list.Add(new PopulationGdp { Country = "Eritrea", Pop = 5415000, Gdp = 3858 });
            list.Add(new PopulationGdp { Country = "Burundi", Pop = 8575000, Gdp = 3094 });
            list.Add(new PopulationGdp { Country = "Guyana", Pop = 784894, Gdp = 3059 });
            list.Add(new PopulationGdp { Country = "Maldives", Pop = 317280, Gdp = 2885 });
            list.Add(new PopulationGdp { Country = "Lesotho", Pop = 2194000, Gdp = 2220 });
            list.Add(new PopulationGdp { Country = "Liberia", Pop = 3476608, Gdp = 2013 });
            list.Add(new PopulationGdp { Country = "Bhutan", Pop = 708265, Gdp = 1983 });
            list.Add(new PopulationGdp { Country = "Cape Verde", Pop = 491575, Gdp = 1858 });
            list.Add(new PopulationGdp { Country = "San Marino", Pop = 32093, Gdp = 1786 });
            list.Add(new PopulationGdp { Country = "Central African Republic", Pop = 4487000, Gdp = 1726 });
            list.Add(new PopulationGdp { Country = "Belize", Pop = 312698, Gdp = 1699 });
            list.Add(new PopulationGdp { Country = "Djibouti", Pop = 818159, Gdp = 1589 });
            list.Add(new PopulationGdp { Country = "Seychelles", Pop = 90945, Gdp = 1423 });
            list.Add(new PopulationGdp { Country = "Saint Lucia", Pop = 166526, Gdp = 1404 });
            list.Add(new PopulationGdp { Country = "Antigua and Barbuda", Pop = 89138, Gdp = 1248 });
            list.Add(new PopulationGdp { Country = "Solomon Islands", Pop = 542287, Gdp = 1155 });
            list.Add(new PopulationGdp { Country = "Guinea-Bissau", Pop = 1520830, Gdp = 1111 });
            list.Add(new PopulationGdp { Country = "Grenada", Pop = 110821, Gdp = 912 });
            list.Add(new PopulationGdp { Country = "Saint Kitts and Nevis", Pop = 51970, Gdp = 852 });
            list.Add(new PopulationGdp { Country = "Samoa", Pop = 184032, Gdp = 827 });
            list.Add(new PopulationGdp { Country = "The Gambia", Pop = 1776000, Gdp = 824 });
            list.Add(new PopulationGdp { Country = "Vanuatu", Pop = 234023, Gdp = 822 });
            list.Add(new PopulationGdp { Country = "Saint Vincent and the Grenadines", Pop = 100892, Gdp = 729 });
            list.Add(new PopulationGdp { Country = "Comoros", Pop = 754000, Gdp = 697 });
            list.Add(new PopulationGdp { Country = "Dominica", Pop = 71685, Gdp = 524 });
            list.Add(new PopulationGdp { Country = "Tonga", Pop = 105000, Gdp = 438 });
            list.Add(new PopulationGdp { Country = "São Tomé and Príncipe", Pop = 169000, Gdp = 338 });
            list.Add(new PopulationGdp { Country = "Micronesia", Pop = 102624, Gdp = 308 });
            list.Add(new PopulationGdp { Country = "Palau", Pop = 21000, Gdp = 249 });
            list.Add(new PopulationGdp { Country = "Marshall Islands", Pop = 54305, Gdp = 193 });
            list.Add(new PopulationGdp { Country = "Kiribati", Pop = 101000, Gdp = 181 });
            list.Add(new PopulationGdp { Country = "Tuvalu", Pop = 10000, Gdp = 38 });

            // calculate the per-capita income and add that to each data point
            foreach (var item in list)
            {
                item.Pci = (double)item.Gdp / item.Pop * 1e6; // US$/year/capita
            }

            return list;
        }
    }
}