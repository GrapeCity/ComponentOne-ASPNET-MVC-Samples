using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Xml.Serialization;

namespace PeriodicTable.Models
{
    public class Element
    {
        [XmlElement("atomic-number")]
        public double AtomicNumber { get; set; }

        [XmlElement("atomic-weight")]
        public double AtomicWeight { get; set; }

        [XmlElement("element")]
        public string ElementName { get; set; }

        [XmlElement("symbol")]
        public string Symbol { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }

        public double Value { get { return 1; } }
    }

    public class ElementRoot
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("fields")]
        public Element Element { get; set; }
    }

    [XmlRoot("Elements")]
    public class ElementsCollection
    {
        [XmlElement("PeriodicElement")]
        public ElementRoot[] Elements { get; set; }
    }
}