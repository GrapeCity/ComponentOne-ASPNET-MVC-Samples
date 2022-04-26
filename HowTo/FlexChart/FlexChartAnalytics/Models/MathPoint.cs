using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexChartAnalytics.Models
{
    public class MathPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public static List<MathPoint> GetMathPointList(int count)
        {
            List<MathPoint> mPoints = new List<MathPoint>() { };

            var rand = new Random(0);
            for (int i = 0; i < count; i++)
            {
                mPoints.Add(new MathPoint { X = i + 1, Y = rand.Next(0, 100) });
            }

            return mPoints;
        }
    }
}