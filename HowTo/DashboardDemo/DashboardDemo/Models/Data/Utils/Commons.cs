using System;
using System.IO;
using System.Globalization;
using Point = System.Drawing.Point;
using System.Drawing;
using System.Linq;

namespace DashboardModel
{
    public enum CategoryType
    {
        All,
        Sports,
        City,
        MultiUtility,
        NewEntry,
    }

    public enum CampainTaskType
    {
        All,
        InProgress,
        Completed,
        Deferred,
        Urgent
    }

    public enum OpportunityLevel
    {
        High,
        Medium,
        Low,
        Unlikely
    }

    public class DateRange
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public double? NStart { get; set; }
        public double? NEnd { get; set; }
    }

    public class SaleGoalItem
    {
        public string Name { get; set; }
        public double Sales { get; set; }
        public double Goal { get; set; }
        public double Profit { get; set; }
        public double CompletePrecent { get { return Sales / Goal; } }
    }

    public class SaleItem
    {
        public string Name { get; set; }
        public double Sales { get; set; }
        public double Profit { get; set; }
        public double Id { get; set; }
        public DateTime Date { get; set; }
        public string Month { get { return Date.ToString("MM/yyyy"); } }
    }

    public class RegionSaleItem : SaleItem
    {
        public Point Locat { get; set; }
    }

    public class ProductItem
    {
        public string Label { get; set; }
        public string ImagePath { get; set; }

        private string BuildImagPath(Product product)
        {
#if ASPNETCORE
            var folder = DashboardDemo.Startup.Environment.WebRootPath;
#else
            var folder = System.Web.HttpContext.Current.Server.MapPath("~");
#endif
            var productFolder = Path.Combine(folder, "Content", "products");
            if (!Directory.Exists(productFolder))
            {
                Directory.CreateDirectory(productFolder);
            }
            var fileName = "product_" + product.Id.ToString(CultureInfo.InvariantCulture) + ".jpg";
            var webPath = "/Content/products/" + fileName;
            var imagePath = Path.Combine(productFolder, fileName);
            if (!File.Exists(imagePath))
            {
                using (var ms = new MemoryStream(product.Image))
                {
                    using (var bmp = new Bitmap(ms))
                    {
                        bmp.Save(imagePath);
                    }
                }
            }
            return webPath;
        }

        public ProductItem(Model model, Product product)
        {
            var category = model.Categories.First(x => x.Id == product.CategoryId);
            Label = string.Format(" {0} \u00B7 {1} \u00B7 {2:C} ", product.ToString(), category.ToString(), product.Cost.ToString());
            ImagePath = BuildImagPath(product);
        }
    }
    public class CampainTaskItem
    {
        private Sale _saleData;
        private Model _model;

        public string Subject { get; set; }
        public string AssignedTo { get; set; }
        public string ItemName { get; set; }
        public DateTime DueDate { get; set; }
        public double Complete { get; set; }

        internal bool Deferred
        {
            get { return _saleData.Date > DueDate; }
        }
        internal bool Urgent
        {
            get { return Math.Abs((_saleData.Date - _saleData.GetCampaign(_model).Start).Days) < 3; }
        }

        public CampainTaskItem(Model model, Sale sale)
        {
            _model = model;
            _saleData = sale;
            var campaign = sale.GetCampaign(model);
            var customer = sale.GetCustomer(model);
            Subject = campaign.Name;
            AssignedTo = customer.Name;

            var product = sale.GetProduct(model);
            ItemName = product.Name;
            DueDate = campaign.Finish;

            var baseTime = new DateTime(2017, 10, 1);

            double total = (campaign.Finish - campaign.Start).Days;
            double current = (baseTime - campaign.Start).Days;
            Complete = Math.Round(Math.Min(1, Math.Max(0, current / total)), 2);
        }
    }
    public class CurrentPriorBudgetItem
    {
        public double Sales { get; set; }
        public double Profit { get; set; }
        public double ProirProfit { get; set; }
        public double ProirSales { get; set; }
        public DateTime Date { get; set; }
    }
    public class ProductsWiseSaleItem
    {
        public int ID { get; set; }
        public double Sale { get; set; }
        public double Quantity { get; set; }
        public string Category { get; set; }
        public string Product { get; set; }
        public string Region { get; set; }
        public DateTime Date { get; set; }
    }
    public class Opportunity
    {
        public OpportunityLevel Level { get; set; }
        public double Sales { get; set; }
        public string LevelName { get { return Level.ToString(); } }
    }
}