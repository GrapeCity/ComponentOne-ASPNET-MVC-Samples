using System.Collections.Generic;
using System.Linq;

namespace MvcExplorer.Models
{
    public class StatisticMessage
    {        
        public string Month { get; set; }
        public int SMS { get; set; }
        public int Email { get; set; }
              
        public static IEnumerable<StatisticMessage> GetData()
        {
            System.Random rnd = new System.Random();
            string[] months = "jan,feb,mar,apr,may,jun,jul,aug,sep,oct,nov,dec".Split(',');
            return Enumerable.Range(0, months.Count()).Select(i =>
            {
                return new StatisticMessage
                {
                    Month = months[i],
                    SMS = rnd.Next(0, 50),
                    Email = rnd.Next(0,100)                    
                };
            });
        }
    }
}