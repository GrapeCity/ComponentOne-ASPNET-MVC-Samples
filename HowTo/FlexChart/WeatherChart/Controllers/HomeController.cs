using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WeatherChart.Models;

namespace MvcExplorer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Weather> weatherDataList = GetWeatherData();

            return View(weatherDataList);
        }

        private List<Weather> GetWeatherData() 
        {
            List<Weather> weatherDataList = new List<Weather>() { };

            string sPath = Server.MapPath("./App_Data/WeatherData.txt");
            StreamReader sr = new StreamReader(sPath, Encoding.Default);
            string dataString = sr.ReadToEnd();
            string[] dataArray = Regex.Split(dataString, "\r\n");

            for (int i = 1; i < dataArray.Count(); i++)
            {
                if (!string.IsNullOrEmpty(dataArray[i]))
                {
                    weatherDataList.Add(new Weather(dataArray[i]));
                }
            }

            return weatherDataList;
        }
    }
}
