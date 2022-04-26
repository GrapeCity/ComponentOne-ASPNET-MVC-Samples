using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DashboardModel
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public virtual int CategoryId { get; set; }
        public byte[] Image { get; set; }
        public override string ToString()
        {
            return Name;
        }

        private Category category;
        public virtual Category GetCategory(Model model)
        {
            if (category == null)
                category = model.Categories.First(x => x.Id == this.CategoryId);
            return category;
        }
    }

    public class Region
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        [XmlIgnore]
        public virtual List<Shop> Shops { get; set; }
        [XmlIgnore]
        public virtual List<Customer> Customers { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }

    public class Shop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual int RegionId { get; set; }
        [XmlIgnore]
        public virtual Region Region { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }

    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }

    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual int RegionId { get; set; }
        public override string ToString()
        {
            return Name;
        }

        private Region region;
        public virtual Region GetRegion(Model model)
        {
            if (region == null)
                region = model.Regions.First(x => x.Id == this.RegionId);
            return region;
        }
    }

    public class Campaign
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        [XmlElement(DataType = "date")]
        public DateTime Start { get; set; }
        [XmlElement(DataType = "date")]
        public DateTime Finish { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }

    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [XmlElement(DataType = "date")]
        public DateTime Date { get; set; }
        public virtual int ProductId { get; set; }
        public double Quantity { get; set; }
        public double Cost { get; set; }
        public double Summ { get; set; }
        public virtual int CustomerId { get; set; }
        public virtual int CampaignId { get; set; }
        
        private Campaign campaign;
        public virtual Campaign GetCampaign(Model model)
        {
            if (campaign == null)
                campaign = model.Campaigns.First(x => x.Id == this.CampaignId);
            return campaign;
        }

        private Customer customer;
        public virtual Customer GetCustomer(Model model)
        {
            if (customer == null)
                customer = model.Customers.First(x => x.Id == this.CustomerId);
            return customer;
        }

        
        private Product product;
        public virtual Product GetProduct(Model model)
        {
            if (product == null)
                product = model.Products.First(x => x.Id == this.ProductId);
            return product;
        }
    }

    public class LeadState
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }

    public class LeadHistoryItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public virtual int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        [XmlElement(DataType = "date")]
        public virtual DateTime Date { get; set; }
        public virtual int LeadStateId { get; set; }
        public virtual LeadState LeadState { get; set; }
        public string Comment { get; set; }
        public override string ToString()
        {
            return string.Format("{0} - {1} - {2}", Customer, Date, LeadState);
        }
    }

    public class BudgetItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [XmlElement(DataType = "date")]
        public virtual DateTime Date { get; set; }
        public virtual int CategoryId { get; set; }
        public double Goal { get; set; }
        public double Expences { get; set; }
        public double Sales { get; set; }
        public double Profit { get; set; }
    }

    public class RegionWiseSale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int RegionId { get; set; }
        public string Region { get; set; }
        public string Customer { get; set; }
        public string Product { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Sales { get; set; }
        public double Profit { get; set; }

        [XmlElement(DataType = "date")]
        public DateTime Date { get; set; }
    }

    public class OpportunityItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public double Sales { get; set; }

        public int LevelId { get; set; }
        public string Level { get; set; }
        public string Customer { get; set; }

        [XmlElement(DataType = "date")]
        public DateTime Date { get; set; }
    }

    public class ProfitAndSale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public double Sales { get; set; }
        public double Profit { get; set; }
        public double Quantity { get; set; }
        public string Product { get; set; }
        public string Customer { get; set; }
        public string Region { get; set; }
        public string Category { get; set; }
        public string Campaign { get; set; }

        [XmlElement(DataType = "date")]
        public DateTime Date { get; set; }
    }

    public class RegionSalesGoal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public double Sales { get; set; }
        public double Profit { get; set; }
        public double Goal { get; set; }
        [XmlElement(DataType = "date")]
        public virtual DateTime Date { get; set; }
        public virtual int RegionId { get; set; }
        [XmlIgnore]
        public string Name { get; set; }
    }

    public class Model
    {
        public readonly DateTime StartDate = new DateTime(2016, 1, 1);
        public readonly DateTime EndDate = new DateTime(2017, 8, 1);

        public Category[] Categories { get; set; }
        public Product[] Products { get; set; }
        public Region[] Regions { get; set; }
        public Shop[] Shops { get; set; }
        public Customer[] Customers { get; set; }
        public Campaign[] Campaigns { get; set; }
        public Sale[] Sales { get; set; }
        public LeadState[] LeadStates { get; set; }
        public LeadHistoryItem[] LeadHistory { get; set; }
        public BudgetItem[] Budget { get; set; }
        public RegionWiseSale[] RegionWiseSales { get; set; }
        public OpportunityItem[] Opportunities { get; set; }
        public ProfitAndSale[] ProfitAndSales { get; set; }
        public RegionSalesGoal[] RegionSales { get; set; }

        public static Model GetPopulated(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(Model));
            return (Model)serializer.Deserialize(stream);
        }

        public static Model GetPopulated(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                return GetPopulated(stream);
            }
        }

        public void Unload(string filePath)
        {
            using (var stream = File.CreateText(filePath))
            {
                new XmlSerializer(typeof(Model)).Serialize(stream, this);
            }
        }
    }

    public class RangeSelectorModel
    {
        public IList<SaleItem> DateSaleList { get; set; }
        public DateRange Range { get; set; }
    }
}
