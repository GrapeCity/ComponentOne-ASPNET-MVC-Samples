using System.Collections.Generic;
using System;

namespace MvcExplorer.Models
{
    public class DrillDataPoints
    {
        //-----------------------------------------------------------------
        #region ** ctor

        public DrillDataPoints(int count)
        {
            Temperature = new double[count];
            Pressure = new double[count];
            Conductivity = new double[count];
            Ph = new double[count];
            Depth = new double[count];
        }

        #endregion

        //-----------------------------------------------------------------
        #region ** object model

        public double[] Temperature { get; set; }
        public double[] Pressure { get; set; }
        public double[] Conductivity { get; set; }
        public double[] Ph { get; set; }
        public double[] Depth { get; set; }

        public List<DrillDataPoint> DrillDataPointSeries { get; set; }

        #endregion

        //-----------------------------------------------------------------
        #region ** scale values so they can be seen when plotted together

        public void ScaleValues()
        {
            double max, min;
            GetRange(Temperature, out max, out min);
            ScaleValues(Pressure, max, min);
            ScaleValues(Conductivity, max, min);
            ScaleValues(Ph, max, min);

            DrillDataPointSeries = new List<DrillDataPoint>() { };
            for (int i = 0; i < Temperature.Length; i++)
            {
                DrillDataPointSeries.Add(new DrillDataPoint { Temperature = Temperature[i], Pressure = Pressure[i], Conductivity = Conductivity[i], Ph = Ph[i], Depth = Depth[i] });
            }
        }
        void GetRange(double[] values, out double max, out double min)
        {
            max = values[0];
            min = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                max = Math.Max(max, values[i]);
                min = Math.Min(min, values[i]);
            }
        }
        void ScaleValues(double[] values, double max, double min)
        {
            double rmax, rmin;
            GetRange(values, out rmax, out rmin);
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = (values[i] - rmin) / (rmax - rmin) * (max - min) + min;
            }
        }

        #endregion
    }

    public class DrillDataPoint
    {
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double Conductivity { get; set; }
        public double Ph { get; set; }
        public double Depth { get; set; }
    }
}
