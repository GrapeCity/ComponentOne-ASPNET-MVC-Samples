using System;

namespace MvcExplorer.Models
{
    public class Task
    {
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Parent { get; set; }
        public byte Percent { get; set; }
    }
}