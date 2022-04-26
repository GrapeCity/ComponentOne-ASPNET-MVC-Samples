using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using C1.Win.C1Document;
using System.Collections.Specialized;
using C1.Win.FlexReport;
using C1.Web.Api.Report.Models;
using C1.Web.Api.Report;

namespace CustomReportProvider.Models
{
    public class MemoryReportsProvider : ReportProvider
    {
        /// <summary>
        /// Gets catalog info of the specified path.
        /// </summary>
        /// <param name="providerName">The key used to register the provider.</param>
        /// <param name="path">The relative path of the folder or report.</param>
        /// <param name="recursive">Whether to return the entire tree of child items below the specified item.</param>
        /// <param name="args">The collection of HTTP query string variables.</param>
        /// <returns>A <see cref="CatalogItem"/> object.</returns>
        public override CatalogItem GetCatalogItem(string providerName, string path, bool recursive, NameValueCollection args = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the document for the specified report path.
        /// </summary>
        /// <param name="path">The relative path of the report, without the registered key for the report provider.</param>
        /// <param name="args">The collection of HTTP query string variables.</param>
        /// <returns>A <see cref="C1DocumentSource"/> created for the specified path.</returns>
        protected override C1DocumentSource CreateDocument(string path, NameValueCollection args)
        {
            // in this sample, the path is "ReportsRoot/DataSourceInMemory.flxr/Simple List".

            var index = path.LastIndexOf('/');
            var filePath = path.Substring(0, index);
            var reportName = path.Substring(index + 1);
            var fileName = System.IO.Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, filePath);

            var report = new C1FlexReport();
            report.Load(fileName, reportName);
            UpdateDataSource(report, args);
            return report;
        }

        private void UpdateDataSource(C1FlexReport report, NameValueCollection args)
        {
            if (report == null || args == null) return;

            var country = args["country"];
            var dataset = Employees.DataSet;
            var datatable = country != null && dataset.Tables.Contains(country)
                ? dataset.Tables[country]
                : dataset.Tables[0];

            report.DataSource.Recordset = datatable;
        }
    }
}