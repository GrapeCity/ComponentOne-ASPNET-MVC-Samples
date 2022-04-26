using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gauge101.Models
{
    public class GaugeModel
    {
        public bool autoScale = true;
        public string format = "p0";
        public bool isReadOnly = false;        
        public double min = 0;
        public double max = 1;
        public bool showRanges = true;
        public double step = .25;
        public double startAngle = 0;
        public double sweepAngle = 180;
        public double value = .5;

        public double pointerThickness = .5;
        public double rangesTarget=.75;
        public double lowerRangemin = 0;
        public double lowerRangemax = .33;
        public System.Drawing.Color lowerRangecolor = System.Drawing.Color.Yellow;

        public double middleRangemin = .33;
        public double middleRangemax = .66;
        public System.Drawing.Color middleRangecolor = System.Drawing.Color.Green;

        public double upperRangemin = .66;
        public double upperRangemax = 1;
        public System.Drawing.Color upperRangecolor = System.Drawing.Color.Red;
        
        public GaugeModel()
        {

        }
    }
}