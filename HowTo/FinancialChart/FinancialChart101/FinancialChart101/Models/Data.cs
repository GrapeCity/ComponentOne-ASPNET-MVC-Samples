using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FinancialChart101.Models
{
    public class Data
    {
        public DateTime date { get; set; }        
        public decimal open { get; set; }
        public decimal high { get; set; }
        public decimal low { get; set; }
        public decimal close { get; set; }
        public decimal volume { get; set; }

        /// <summary>
        /// Gets the data from json file.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Data> GetData()
        {
            List<Data> list = new List<Data>();
            string ppath = System.AppDomain.CurrentDomain.BaseDirectory + "Content/data/fb.json";
            using (StreamReader r = new StreamReader(ppath))
            {
                string json = r.ReadToEnd();
                list = JsonConvert.DeserializeObject<List<Data>>(json);
            }
            return list;
        }

    }
}