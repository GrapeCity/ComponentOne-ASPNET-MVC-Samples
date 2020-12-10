using System;
using System.Collections.Generic;

namespace MvcExplorer.Models
{
    public class Dot
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Size { get; set; }

        public static List<Dot> GetData(double cx, double cy, int n, double maxr)
        {
            List<Dot> data = new List<Dot>();
            Random ran = new Random();
            double r0 = 0.1 + maxr;
            for (int i = 0; i < n; i++)
            {
                double a = 2 * Math.PI * ran.NextDouble();
                double r = r0 * Math.Sqrt(-2 * Math.Log(ran.NextDouble()));
                data.Add(new Dot()
                {
                    X = cx + r * Math.Cos(a),
                    Y = cy + r * Math.Sin(a)
                });
            }
            return data;
        }
    }
}