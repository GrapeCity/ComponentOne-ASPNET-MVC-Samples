using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using C1.Web.Mvc.Chart;

namespace MvcExplorer.Models
{
    public class ChartOptions
    {
        public static List<string> ChartTypes = new List<string>() { "Column", "Bar", "Scatter", "Line", "LineSymbols", "Area", "Spline", "SplineSymbols", "SplineArea" };
        public static List<string> Stackings = new List<string>() { "None", "Stacked", "Stacked 100%" };
        public static List<string> SelectionModes = new List<string>() { "None", "Series", "Point" };
        public static List<string> LabelPositions = new List<string>() { "None", "Top", "Bottom", "Left", "Right", "Center" };
        public static List<string> LabelBorders = new List<string>() { "False", "True" };

        public string type { get; set; }
        public string stack { get; set; }
        public string selectionmode { get; set; }

        public ChartType CType
        {
            get
            {
                ChartType rtype = C1.Web.Mvc.Chart.ChartType.Column;
                switch (type)
                {
                    case "Bar":
                        rtype = ChartType.Bar;
                        break;
                    case "Scatter":
                        rtype = ChartType.Scatter;
                        break;
                    case "Line":
                        rtype = ChartType.Line;
                        break;
                    case "LineSymbols":
                        rtype = ChartType.LineSymbols;
                        break;
                    case "Area":
                        rtype = ChartType.Area;
                        break;
                    case "Spline":
                        rtype = ChartType.Spline;
                        break;
                    case "SplineSymbols":
                        rtype = ChartType.SplineSymbols;
                        break;
                    case "SplineArea":
                        rtype = ChartType.SplineArea;
                        break;
                }
                return rtype;
            }
        }

        public Stacking CStack
        {
            get
            {
                Stacking rstack = Stacking.None;
                switch (stack)
                {
                    case "Stacked":
                        rstack = Stacking.Stacked;
                        break;
                    case "Stacked 100%":
                        rstack = Stacking.Stacked100pc;
                        break;
                }
                return rstack;
            }
        }

        public SelectionMode CSelectionMode
        {
            get
            {
                SelectionMode sm = C1.Web.Mvc.Chart.SelectionMode.Series;
                switch (selectionmode)
                {
                    case "None":
                        sm = C1.Web.Mvc.Chart.SelectionMode.None;
                        break;
                    case "Point":
                        sm = C1.Web.Mvc.Chart.SelectionMode.Point;
                        break;
                    case "Series":
                        sm = C1.Web.Mvc.Chart.SelectionMode.Series;
                        break;
                }
                return sm;
            }
        }
    }
}