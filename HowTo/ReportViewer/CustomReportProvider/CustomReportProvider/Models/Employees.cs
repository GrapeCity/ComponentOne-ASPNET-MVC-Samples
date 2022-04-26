using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CustomReportProvider.Models
{
    public static class Employees
    {
        private static DataSet _dataset;
        private static string[] _countries;
        private static object _lockObj = new object();

        public static DataSet DataSet
        {
            get
            {
                EnsureInit();
                return _dataset;
            }
        }

        public static string[] CountryNames
        {
            get
            {
                EnsureInit();
                return _countries;
            }
        }

        private static void EnsureInit()
        {
            if (_dataset == null)
            {
                lock (_lockObj)
                {
                    if (_dataset == null)
                    {
                        InitDataSet();
                    }
                }
            }
        }

        private static void InitDataSet()
        {
            _dataset = new DataSet();

            var doc = XElement.Load(HttpContext.Current.Server.MapPath("~/Content/nwind.xml"));
            foreach (var employee in doc.Elements("Employees"))
            {
                var country = employee.Element("Country").Value;
                var table = GetEmployeesDataTable(country);
                var row = table.NewRow();
                row["Country"] = country;
                row["LastName"] = employee.Element("LastName").Value;
                row["FirstName"] = employee.Element("FirstName").Value;
                row["City"] = employee.Element("City").Value;
                row["Address"] = employee.Element("Address").Value;
                row["Notes"] = employee.Element("Notes").Value;
                table.Rows.Add(row);
            }

            _countries = _dataset.Tables.OfType<DataTable>().Select(t => t.TableName).ToArray();
        }

        private static DataTable GetEmployeesDataTable(string name)
        {
            if (_dataset.Tables.Contains(name))
                return _dataset.Tables[name];

            var table = CreateEmployeesDataTable(name);
            _dataset.Tables.Add(table);
            return table;
        }

        private static DataTable CreateEmployeesDataTable(string name)
        {
            var table = new DataTable(name);
            table.Columns.Add("LastName");
            table.Columns.Add("FirstName");
            table.Columns.Add("Country");
            table.Columns.Add("City");
            table.Columns.Add("Address");
            table.Columns.Add("Notes");
            return table;
        }
    }
}