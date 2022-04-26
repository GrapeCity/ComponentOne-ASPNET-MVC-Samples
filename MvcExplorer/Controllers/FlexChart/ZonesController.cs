using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        public ActionResult Zones()
        {
            List<Dot> studentPoints = new List<Dot>();
            var nStudents = 200;
            var nMaxPoints = 1600;
            var rand = new Random(0);
            for (int i = 0; i < nStudents; i++)
            {
                studentPoints.Add(new Dot { X = i, Y = nMaxPoints * 0.5 * (1 + rand.Next(0, 10)) });
            }
            ViewBag.studentPoints = studentPoints;

            List<List<Dot>> lineSeriesDataList = new List<List<Dot>>() { };
            double mean = FindMean(studentPoints);
            double stdDev = FindStdDev(studentPoints, mean);
            for (int i = -2; i <= 2; i++)
            {
                double y = mean + i * stdDev;
                List<Dot> seriesData = new List<Dot>() { new Dot { X = 0, Y = y }, new Dot { X = nStudents - 1, Y = y } };
                lineSeriesDataList.Add(seriesData);
            }
            ViewBag.lineSeriesDataList = lineSeriesDataList;

            ViewBag.colors = new List<string>() {
                "rgba(255,192,192,0.2)",
                "rgba(55,328,228,0.5)",
                "rgba(255,228,128,0.5)",
                "rgba(128,255,128,0.5)",
                "rgba(128,128,225,0.5)"
            };

            List<double> yDatas = new List<double>() { };
            for (int i = 0; i < studentPoints.Count; i++)
            {
                yDatas.Add(studentPoints[i].Y);
            }
            yDatas.Sort();

            List<double> zones = new List<double>() {
                yDatas[GetBoundingIndex(yDatas, 0.05)],
                yDatas[GetBoundingIndex(yDatas, 0.25)],
                yDatas[GetBoundingIndex(yDatas, 0.75)],
                yDatas[GetBoundingIndex(yDatas, 0.95)]
            };
            ViewBag.zones = zones;

            return View();
        }

        private double FindMean(List<Dot> points)
        {
            double sum = 0;
            for (var i = 0; i < points.Count; i++)
            {
                sum += points[i].Y;
            }
            return sum / points.Count;
        }

        private double FindStdDev(List<Dot> points, double mean)
        {
            double sum = 0;
            for (var i = 0; i < points.Count; i++)
            {
                double d = points[i].Y - mean;
                sum += d * d;
            }
            return Math.Sqrt(sum / points.Count);
        }

        private int GetBoundingIndex(List<double> points, double frac)
        {
            double n = points.Count;
            int i = (int)Math.Ceiling(n * frac);
            while (i > points[0] && points[i] == points[i+1])
                i--;
            return i;
        }
    }
}
