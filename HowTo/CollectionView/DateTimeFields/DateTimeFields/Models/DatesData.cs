using System;
using System.Collections.Generic;
using System.Linq;

namespace DateTimeFields.Models
{
    public class DatesData
    {
        /// <summary>
        /// The primary key.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// A DateTime field which Kind is Utc.
        /// </summary>
        public DateTime UtcDateTime { get; set; }

        /// <summary>
        /// A DateTime field which Kind is Unspecified.
        /// </summary>
        public DateTime UnspecifiedDateTime { get; set; }

        /// <summary>
        /// A DateTime field which Kind is Local.
        /// </summary>
        public DateTime LocalDateTime { get; set; }

        /// <summary>
        /// Get the data.
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        public static IEnumerable<DatesData> GetData(int total)
        {
            var rand = new Random(0);
            var dt = DateTime.Now;
            var list = Enumerable.Range(0, total).Select(i =>
            {
                return new DatesData
                {
                    Id = i + 1,
                    UtcDateTime = new DateTime(dt.Year, i % 12 + 1, 25, 7, 0, 0, DateTimeKind.Utc),
                    UnspecifiedDateTime = new DateTime(dt.Year, i % 12 + 1, 25, 7, 0, 0, DateTimeKind.Unspecified),
                    LocalDateTime = new DateTime(dt.Year, i % 12 + 1, 25, 7, 0, 0, DateTimeKind.Local)
                };
            });

            return list;
        }
    }
}