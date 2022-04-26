using System.Collections.Generic;
using System.Linq;

namespace MvcExplorer.Models
{
    public class Population
    {
        public string Country { get; set; }
        public int POP { get; set; }
        public int GDP { get; set; }
        public double PCI { get; set; }

        private static List<string> COUNTRIES = new List<string>
        {
            "United States","China","Japan","Germany","United Kingdom","France","Brazil","Italy",
            "India","Russia","Canada","Australia","South Korea","Spain","Mexico","Indonesia","Netherlands",
            "Turkey","Saudi Arabia","Switzerland","Nigeria","Sweden","Poland","Argentina","Belgium","Taiwan",
            "Norway","Austria","Iran","Thailand","United Arab Emirates","Colombia","South Africa","Denmark",
            "Malaysia","Singapore","Israel","Hong Kong","Egypt","Philippines","Finland","Chile","Ireland",
            "Pakistan","Greece","Portugal","Iraq","Kazakhstan","Algeria","Qatar","Venezuela","Czech Republic",
            "Peru","Romania","New Zealand","Vietnam","Bangladesh","Kuwait","Hungary","Ukraine","Angola","Morocco",
            "Ecuador","Slovakia","Oman","Syria","Belarus","Sri Lanka","Sudan","Azerbaijan","Luxembourg",
            "Dominican Republic","Myanmar","Uzbekistan","Kenya","Guatemala","Uruguay","Croatia","Bulgaria",
            "Ethiopia","Lebanon","Slovenia","Costa Rica","Tunisia","Lithuania","Tanzania","Turkmenistan","Serbia",
            "Panama","Yemen","Libya","Ghana","Democratic Republic of the Congo","Jordan","Bahrain","Côte d\"Ivoire",
            "Bolivia","Latvia","Cameroon","Paraguay","Trinidad and Tobago","Uganda","Zambia","Estonia","El Salvador",
            "Cyprus","Afghanistan","Nepal","Honduras","Gabon","Bosnia and Herzegovina","Brunei","Iceland",
            "Papua New Guinea","Mozambique","Cambodia","Georgia","Senegal","Equatorial Guinea","Botswana",
            "South Sudan","Chad","Zimbabwe","Jamaica","Namibia","Republic of the Congo","Albania","Mauritius",
            "Burkina Faso","Mali","Mongolia","Nicaragua","Laos","Armenia","Macedonia","Madagascar","Malta",
            "Tajikistan","Haiti","Benin","Bahamas","Niger","Moldova","Rwanda","Kyrgyzstan","Kosovo","Guinea",
            "Malawi","Suriname","Mauritania","Timor-Leste","Sierra Leone","Togo","Montenegro","Swaziland","Barbados",
            "Fiji","Eritrea","Burundi","Guyana","Maldives","Lesotho","Liberia","Bhutan","Cape Verde","San Marino",
            "Central African Republic","Belize","Djibouti","Seychelles","Saint Lucia","Antigua and Barbuda",
            "Solomon Islands","Guinea-Bissau","Grenada","Saint Kitts and Nevis","Samoa","The Gambia","Vanuatu",
            "Saint Vincent and the Grenadines","Comoros","Dominica","Tonga","São Tomé and Príncipe","Micronesia",
            "Palau","Marshall Islands","Kiribati","Tuvalu"
        };

        private static List<int> GDPS = new List<int>
        {
            17348075,10356508,4602367,3874437,2950039,2833687,2346583,2147744,2051228,1860598,1785387,1442722,1410383,
            1406538,1291062,888648,880716,798332,746248,703852,573999,570591,547894,543061,534230,529597,499817,437582,
            416490,404824,399451,377867,350082,342362,338108,307872,305673,290896,286435,284618,272649,258017,250814,
            246849,237970,229948,223508,216036,213518,210109,206252,205270,202642,199093,197502,185897,183824,172608,
            136989,130660,129326,110009,100543,99869,77779,77460,76139,74924,74766,74145,65683,64058,63135,62613,60937,
            58728,57471,57073,55824,54809,50028,49570,49553,48633,48288,48089,47932,43866,43777,43229,41148,38616,35918,
            35878,33862,33741,33237,31972,31777,30220,28874,27616,26611,26506,25164,23263,20444,19761,19511,18209,18165,
            17104,17036,16809,16684,16551,16536,15683,15530,15217,14304,13945,13833,13709,13632,13552,13276,12588,12503,
            12094,12037,11806,11681,11644,11342,10674,10514,9242,8711,8685,8511,8024,7962,7897,7402,7319,6707,6055,5210,
            5081,4970,4815,4594,4551,4416,4354,4289,3858,3094,3059,2885,2220,2013,1983,1858,1786,1726,1699,1589,1423,1404,
            1248,1155,1111,912,852,827,824,822,729,697,524,438,338,308,249,193,181,38

        };

        private static List<int> POPS = new List<int>
        {
            320723000,1357000000,127300000,80620000,64596800,65821885,200400000,60705991,1252000000,143500000,34482779,
            24170869,48219000,46162024,122300000,249900000,16715489,76667864,27136977,8039060,173600000,9471174,38092000,
            40117096,10839905,23197947,5316800,8419776,80117000,69519000,8264070,48389000,50586757,5579204,28334135,
            5183700,7798600,7108100,88094000,98390000,5394389,17248450,4581269,191282000,10787690,10555853,32105000,
            16615000,36300000,1699435,31220000,10542080,29797694,21436000,4577100,89700000,156600000,2818000,9985722,
            45668028,19618000,33803400,14483499,5437126,2773479,23362000,9468000,20653000,30894000,9111100,511840,9378818,
            48337000,28000000,38610097,14713763,3368595,4290612,7364570,94100000,4259000,2081660,4563539,10673800,3207100,
            43188000,5105000,7120666,3405813,23833000,6423000,24233431,67758000,6783300,1234571,21395000,10426154,2209000,
            19406100,6337127,1317714,32939800,13046508,1340194,6227000,803200,32358000,26620809,8215313,1534000,3843126,
            422700,318452,7014000,23049621,13395682,4469200,12855153,720000,2031000,8260490,10329208,12754000,2705800,
            2324000,4140000,3194972,1280924,15730977,14517176,2736800,5815524,6348800,3266300,2057284,18866000,417617,
            7616000,10085214,9100000,353658,15730754,3560400,10412826,5477600,1870981,10217591,13077160,529000,3340627,
            1066409,5997000,5753324,620029,1203000,276302,868000,5415000,8575000,784894,317280,2194000,3476608,708265,
            491575,32093,4487000,312698,818159,90945,166526,89138,542287,1520830,110821,51970,184032,1776000,234023,
            100892,754000,71685,105000,169000,102624,21000,54305,101000,10000
        };

        public static IEnumerable<Population> GetData()
        {
            return Enumerable.Range(0, COUNTRIES.Count).Select(i =>
            {
                return new Population
                {
                    Country = COUNTRIES[i],
                    POP = POPS[i],
                    GDP = GDPS[i],
                    PCI = GDPS[i] * 1.0d / POPS[i] * 1e6
                };
            });
        }

    }
}