﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexGrid101.Models
{
    public class FlexGridModel
    {
        public IDictionary<string, object[]> Settings { get; set; }
        public IEnumerable<Sale> CountryData { get; set; }
        public IEnumerable<ITreeItem> TreeData;

        //Add for editing source
        public static List<Sale> Source = Sale.GetData(500).ToList<Sale>();
        public FlexGridModel()
        {

        }
    }
}