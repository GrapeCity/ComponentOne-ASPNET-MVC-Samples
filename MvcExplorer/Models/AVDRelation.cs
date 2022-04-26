using System.Collections.Generic;
using System.Linq;

namespace MvcExplorer.Models
{
    // This class is used to get the relation between acceleration, velocity, distance and Time.
    public class AVDRelation
    {
        public int A { get; set; }
        public int V { get; set; }
        public double D { get; set; }
        public int T { get; set; }

        public static IEnumerable<AVDRelation> getGata(int total)
        {
            var list = Enumerable.Range(0, total).Select(i =>
                {
                    return new AVDRelation
                    {
                        A = i,
                        V = i * i,
                        D = 0.5 * i * i * i,
                        T = i
                    };
                }
                );
            return list;
        }
    }
}