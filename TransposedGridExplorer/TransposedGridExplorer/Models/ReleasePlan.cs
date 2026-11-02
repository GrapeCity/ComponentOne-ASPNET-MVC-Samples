using System;
using System.Collections.Generic;

namespace TransposedGridExplorer.Models
{
    public class ReleasePlan
    {
        public string Product { get; set; }
        public DateTime LaunchDate { get; set; }
        public DateTime LaunchTime { get; set; }
        public string Category { get; set; }
        public string SupportPhone { get; set; }
        public string AccentColor { get; set; }
        public double Price { get; set; }

        public static IList<ReleasePlan> GetData()
        {
            var day = DateTime.Today;
            return new List<ReleasePlan>
            {
                new ReleasePlan { Product = "Aurora Phone",  LaunchDate = day.AddDays(14), LaunchTime = day.AddHours(9),                 Category = "Handheld", SupportPhone = "(415) 555-0132", AccentColor = "#4a90d9", Price = 799 },
                new ReleasePlan { Product = "Aurora Tablet", LaunchDate = day.AddDays(28), LaunchTime = day.AddHours(13).AddMinutes(30), Category = "Tablet", SupportPhone = "(415) 555-0148", AccentColor = "#7ac043", Price = 1099 },
                new ReleasePlan { Product = "Aurora Watch",  LaunchDate = day.AddDays(45), LaunchTime = day.AddHours(11),                Category = "Wearable", SupportPhone = "(415) 555-0177", AccentColor = "#e8a33d", Price = 349 },
                new ReleasePlan { Product = "Aurora Buds",   LaunchDate = day.AddDays(60), LaunchTime = day.AddHours(16),                Category = "Audio", SupportPhone = "(415) 555-0190", AccentColor = "#d1495b", Price = 179 }
            };
        }

        public static IList<string> GetCategories()
        {
            return new List<string> { "Handheld", "Tablet", "Wearable", "Audio" };
        }
    }
}
