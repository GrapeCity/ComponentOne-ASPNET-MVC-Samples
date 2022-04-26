using C1.JsonNet;
using ImageCombo.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace ImageCombo.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        [JsonConverter(typeof(Base64StringConverter))]
        public byte[] Picture { get; set; }

        public static List<Category> GetData()
        {
            var data = new List<Category>();
            var dt = new DataTable();
            var con = new SQLiteConnection(@"data source=|DataDirectory|C1NWind.db;");
            var adp = new SQLiteDataAdapter("Select * from Categories", con);
            adp.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var category = new Category();
                category.CategoryID = int.Parse(dt.Rows[i]["CategoryID"].ToString());
                category.CategoryName = (string)dt.Rows[i]["CategoryName"];
                category.Description = (string)dt.Rows[i]["Description"];
                category.Picture = (byte[])dt.Rows[i]["Picture"];
                data.Add(category);
            }
            return data;
        }
    }
}