﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Input101.Models
{
    public static class StaticModel
    {
        public static List<string> GetCountries()
        {
            return new List<string> 
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
        }


        public static List<string> GetCities()
        {
            return new List<string> 
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
        }
    }
}