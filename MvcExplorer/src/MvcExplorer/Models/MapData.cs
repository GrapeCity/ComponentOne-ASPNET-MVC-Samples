using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcExplorer.Models
{
    public class MapData
    {
        public int Longitude { get; set; }
        public int Latitude1 { get; set; }
        public int Latitude2 { get; set; }
        public static List<MapData> GetData()
        {
            List<MapData> data = new List<MapData>();
            var len = 360;
            for (var i = 0; i <= len; i += 10)
            {
                data.Add(new MapData()
                {
                    Longitude = i,
                    Latitude1 = ((i % 11) * 30) + 60,
                    Latitude2 = ((i % 7) * 30) + 30
                });
            }
            return data;
        }
    }
}