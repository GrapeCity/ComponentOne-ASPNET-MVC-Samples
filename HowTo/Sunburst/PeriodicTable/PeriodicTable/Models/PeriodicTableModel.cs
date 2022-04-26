using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using C1.Web.Mvc.Chart;

namespace PeriodicTable.Models
{
    public class PeriodicTableModel
    {
        public Position LegendPosition { get; set; }
        public float InnerRadius { get; set; }
        public SelectionMode SelectionMode { get; set; }
        public string Binding { get; set; }
        public string BindingName { get; set; }
        public string ChildItemsPath { get; set; }
        public PieLabelPosition DataLabelPosition { get; set; }
        public string DataLabelContent { get; set; }
        public List<Group> Data { get; set; }
        public PeriodicTableModel()
        {
            LegendPosition = Position.None;
            InnerRadius = 0.3F;
            SelectionMode = SelectionMode.Point;
            Binding = "Value";
            BindingName = "GroupName,SubGroupName,Symbol";
            ChildItemsPath = "SubGroups,Elements";
            DataLabelPosition = PieLabelPosition.Center;
            DataLabelContent = "{name}";
            var data = new Models.DataSource();
            Data = data.Groups;
        }
    }
}