using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace PeriodicTable.Models
{
    class Utils
    {
        public static ElementsCollection DeserializeXml(string path)
        {
            using (var reader = new StreamReader(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ElementsCollection));
                return (ElementsCollection)xmlSerializer.Deserialize(reader);
            }
        }
    }
}