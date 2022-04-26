using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiRowLOB.Models
{
    public class DataService
    {
        public class Product
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public double UnitPrice { get; set; }
            public double ProfitUnitPrice { get; set; }
        }

        public class Transfer
        {
            public double DebtorAcc { get; set; }
            public int DebtorType { get; set; }
            public double DebtorAmt { get; set; }
            public double DebtorTax { get; set; }
            public double CreditorAcc { get; set; }
            public int CreditorType { get; set; }
            public double CreditorAmt { get; set; }
            public double CreditorTax { get; set; }
            public string Brief { get; set; }
            public string Note { get; set; }
            public string DebtorTaxCategory { get; set; }
            public string CreditorTaxCategory { get; set; }
        }

        public class TransferSlipData
        {
            public IList<Transfer> Items { get; set; }
            public DateTime Date { get; set; }
            public string SlipNo { get; set; }
            public string Settlement { get; set; }
        }

        public class OrderSlip
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public int Quantity { get; set; }
            public string PackageUnit { get; set; }
            public double UnitPrice { get; set; }
            public double Amount { get; set; }
            public int ShippingId { get; set; }
            public bool Discontinued { get; set; }
            public string Remarks { get; set; }
            public string Description { get; set; }
        }

        public class OrderDetail
        {
            public string OrderId { get; set; }
            public int PartId { get; set; }
            public int HandlingId { get; set; }
            public string ProcessingStatus { get; set; }
            public int PurchaseId { get; set; }
            public string Completed { get; set; }
            public DateTime OrderDate { get; set; }
            public string PartName { get; set; }
            public string Company { get; set; }
            public string Person { get; set; }
            public bool Accepted { get; set; }
            public int Quantity1 { get; set; }
            public int Quantity2 { get; set; }
            public int Quantity3 { get; set; }
            public string Unit { get; set; }
            public DateTime DeliveryDate { get; set; }
            public DateTime ProcessingDate { get; set; }
            public DateTime ShippingDate { get; set; }
            public string PackingRequest { get; set; }
        }

        public class Purchase
        {
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public string Color { get; set; }
            public int PackageUnit { get; set; }
            public string Size { get; set; }
            public int Case { get; set; }
            public int Quantity { get; set; }
            public string Deal { get; set; }
            public string Id { get; set; }
            public double UnitCost { get; set; }
            public double Cost { get; set; }
            public double Price { get; set; }
            public string Remarks { get; set; }
        }

        public class Order
        {
            public string OrderId { get; set; }
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public string SpecialNote { get; set; }
            public int Quantity { get; set; }
            public double UnitPrice { get; set; }
            public int ShippingWarehouse { get; set; }
            public double Amount { get; set; }
            public bool OnHold { get; set; }
            public DateTime OrderDate { get; set; }
            public DateTime DeliveryDate { get; set; }
        }

        public class Sale
        {
            public int Id { get; set; }
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public double ProfitUnitPrice { get; set; }
            public double UnitPrice { get; set; }
            public double TotalProfit { get; set; }
            public double SalesAmount { get; set; }
            public double ProfitRate { get; set; }
            public string Unit { get; set; }
            public string Marker { get; set; }
        }

        private static IList<Product> _products;
        private static IList<OrderSlip> _orders;
        private static IList<Purchase> _purchases;
        private static IList<string> _parts = new string[] { "Widget", "Gadget", "Doohickey" };
        public static IList<string> _companies = new string[] { "Alfreds Futterkiste", "Ernst Handel", "Eastern Connection", "Du monde entier", "Consolidated Holdings", "Chop-suey Chinese" };
        public static IList<string> _persons = new string[] { "Laurence Lebihan", "Elizabeth Lincoln", "Victoria Ashworth", "Patricio Simpson" };

        static DataService()
        {
            InitProducts();
            InitOrdersSlipData();
            InitPurchases();
        }

        private static void InitProducts()
        {
            _products = new List<Product>();
            _products.Add(new Product { Id = "D0001", Name = "Chai", UnitPrice = 12, ProfitUnitPrice = 3.11 });
            _products.Add(new Product { Id = "D0002", Name = "Chang", UnitPrice = 8.5, ProfitUnitPrice = 2.03 });
            _products.Add(new Product { Id = "D0003", Name = "Aniseed Syrup", UnitPrice = 6.85, ProfitUnitPrice = 2.35 });
            _products.Add(new Product { Id = "D0004", Name = "Chef Anton\'s Gumbo Mix", UnitPrice = 17.25, ProfitUnitPrice = 3.3 });
            _products.Add(new Product { Id = "D0005", Name = "Ikura", UnitPrice = 20, ProfitUnitPrice = 2.95 });
            _products.Add(new Product { Id = "D0006", Name = "Mishi Kobe Niku", UnitPrice = 14, ProfitUnitPrice = 3 });
        }

        private static void InitOrdersSlipData()
        {
            _orders = new List<OrderSlip>();
            _orders.Add(new OrderSlip
            {
                ProductId = 1,
                ProductName = "Chai",
                CategoryId = 1,
                CategoryName = "Beverages",
                Quantity = 100,
                PackageUnit = "10 boxes x 20 bags",
                UnitPrice = 18,
                Amount = 100 * 18,
                ShippingId = 1,
                Discontinued = false,
                Remarks = "",
                Description = "Soft drinks, coffees, teas, beers, and ales"
            });
            _orders.Add(new OrderSlip
            {
                ProductId = 2,
                ProductName = "Chang",
                CategoryId = 1,
                CategoryName = "Beverages",
                Quantity = 120,
                PackageUnit = "24 - 12 oz bottles",
                UnitPrice = 19,
                Amount = 120 * 19,
                ShippingId = 1,
                Discontinued = false,
                Remarks = "",
                Description = "Soft drinks, coffees, teas, beers, and ales"
            });
            _orders.Add(new OrderSlip
            {
                ProductId = 3,
                ProductName = "Aniseed Syrup",
                CategoryId = 2,
                CategoryName = "Condiments",
                Quantity = 80,
                PackageUnit = "12 - 550 ml bottles",
                UnitPrice = 10,
                Amount = 80 * 10,
                ShippingId = 1,
                Discontinued = false,
                Remarks = "",
                Description = ""
            });
            _orders.Add(new OrderSlip
            {
                ProductId = 4,
                ProductName = "Chef Anton\'s Gumbo Mix",
                CategoryId = 2,
                CategoryName = "Condiments",
                Quantity = 125,
                PackageUnit = "36 boxes",
                UnitPrice = 21.35,
                Amount = 125 * 21.35,
                ShippingId = 2,
                Discontinued = false,
                Remarks = "",
                Description = ""
            });
            _orders.Add(new OrderSlip
            {
                ProductId = 5,
                ProductName = "Uncle Bob\'s Organic Dried Pears",
                CategoryId = 7,
                CategoryName = "Produce",
                Quantity = 60,
                PackageUnit = "12 - 1 lb pkgs",
                UnitPrice = 30,
                Amount = 60 * 30,
                ShippingId = 2,
                Discontinued = false,
                Remarks = "",
                Description = "Dried fruit and bean curd"
            });
            _orders.Add(new OrderSlip
            {
                ProductId = 6,
                ProductName = "Mishi Kobe Niku",
                CategoryId = 6,
                CategoryName = "Meat/Poultry",
                Quantity = 25,
                PackageUnit = "18 - 500 g pkgs",
                UnitPrice = 97,
                Amount = 25 * 97,
                ShippingId = 4,
                Discontinued = true,
                Remarks = "",
                Description = "Prepared meats"
            });
            _orders.Add(new OrderSlip
            {
                ProductId = 7,
                ProductName = "Ikura",
                CategoryId = 8,
                CategoryName = "Seafood",
                Quantity = 60,
                PackageUnit = "12 - 200 ml jars",
                UnitPrice = 31,
                Amount = 60 * 31,
                ShippingId = 4,
                Discontinued = false,
                Remarks = "",
                Description = "Seaweed and fish"
            });
            _orders.Add(new OrderSlip
            {
                ProductId = 8,
                ProductName = "Queso Cabrales",
                CategoryId = 4,
                CategoryName = "Dairy Products",
                Quantity = 90,
                PackageUnit = "1 kg pkg",
                UnitPrice = 21,
                Amount = 90 * 21,
                ShippingId = 5,
                Discontinued = false,
                Remarks = "",
                Description = "Cheeses"
            });
        }

        public static void InitPurchases()
        {
            _purchases = new List<Purchase>();
            _purchases.Add(new Purchase
            {
                ProductId = "DC0001",
                ProductName = "Chai",
                Color = "Red",
                PackageUnit = 12,
                Size = "40*30*20",
                Case = 1,
                Quantity = 15,
                Deal = "",
                Id = "",
                UnitCost = 200,
                Cost = 15 * 200,
                Price = 15 * 200 * 1.35,
                Remarks = ""
            });
            _purchases.Add(new Purchase
            {
                ProductId = "DC0002",
                ProductName = "Chang",
                Color = "Blue",
                PackageUnit = 16,
                Size = "25*35*25",
                Case = 0,
                Quantity = 10,
                Deal = "",
                Id = "",
                UnitCost = 180,
                Cost = 10 * 180,
                Price = 10 * 180 * 1.35,
                Remarks = ""
            });
            _purchases.Add(new Purchase
            {
                ProductId = "DC0003",
                ProductName = "Aniseed Syrup",
                Color = "Green",
                PackageUnit = 10,
                Size = "50*40*40",
                Case = 2,
                Quantity = 12,
                Deal = "",
                Id = "",
                UnitCost = 150,
                Cost = 12 * 150,
                Price = 12 * 150 * 1.35,
                Remarks = ""
            });
            _purchases.Add(new Purchase
            {
                ProductId = "DC0004",
                ProductName = "Chef Anton\'s Gumbo Mix",
                Color = "Yellow",
                PackageUnit = 24,
                Size = "30*40*40",
                Case = 1,
                Quantity = 8,
                Deal = "",
                Id = "",
                UnitCost = 220,
                Cost = 8 * 220,
                Price = 8 * 220 * 1.35,
                Remarks = ""
            });
            _purchases.Add(new Purchase
            {
                ProductId = "DC0005",
                ProductName = "Ikura",
                Color = "Black",
                PackageUnit = 15,
                Size = "20*20*30",
                Case = 0,
                Quantity = 5,
                Deal = "",
                Id = "",
                UnitCost = 300,
                Cost = 5 * 300,
                Price = 5 * 300 * 1.35,
                Remarks = ""
            });
            _purchases.Add(new Purchase
            {
                ProductId = "DC0006",
                ProductName = "Mishi Kobe Niku",
                Color = "White",
                PackageUnit = 18,
                Size = "25*20*30",
                Case = 0,
                Quantity = 6,
                Deal = "",
                Id = "",
                UnitCost = 360,
                Cost = 6 * 360,
                Price = 6 * 360 * 1.35,
                Remarks = ""
            });
        }

        public static IList<OrderSlip> OrdersSlipData
        {
            get { return _orders; }
        }

        public static IList<Product> Products
        {
            get { return _products; }
        }

        public static IList<Purchase> Purchases
        {
            get { return _purchases; }
        }

        public static TransferSlipData GenerateTransferSlipData(int count)
        {
            var rand = new Random(0);
            var items = new List<Transfer>();

            for (var i = 0; i < count; i++)
            {
                var debtorAcc = rand.Next(0, 5);
                var debtorType = 0;
                var debtorAmt = rand.Next(0, 10000);
                var creditorAcc = rand.Next(0, 4);
                var creditorType = rand.Next(0, 4);
                var creditorAmt = rand.Next(0, 10000);

                var item = new Transfer
                {
                    DebtorAcc = debtorAcc,
                    DebtorType = debtorType,
                    DebtorAmt = debtorAmt,
                    DebtorTax = debtorAmt * 0.09,
                    CreditorAcc = creditorAcc,
                    CreditorType = creditorType,
                    CreditorAmt = creditorAmt,
                    CreditorTax = creditorAmt * 0.09,
                    Brief = "",
                    Note = "",
                    DebtorTaxCategory = "",
                    CreditorTaxCategory = ""
                };
                items.Add(item);
            }

            var slipData = new TransferSlipData();
            slipData.Items = items;
            slipData.Date = DateTime.Now;
            slipData.SlipNo = "s128";
            slipData.Settlement = "Normal";

            return slipData;
        }

        public static IList<OrderDetail> GetOrderDetails(int count)
        {
            var rand = new Random(0);
            var orderDetails = new List<OrderDetail>();
            for (int i = 0; i < count; i++)
            {
                var orderDate = new DateTime(2016, i % 12 + 1, i % 28 + 1);
                orderDetails.Add(new OrderDetail
                {
                    OrderId = "PT-0000" + (i + 1),
                    PartId = rand.Next(10000, 99999),
                    HandlingId = rand.Next(100, 999),
                    ProcessingStatus = "Testing",
                    PurchaseId = rand.Next(1000, 9999),
                    Completed = "",
                    OrderDate = orderDate,
                    PartName = _parts[rand.Next(0, 3)],
                    Company = _companies[rand.Next(0, 6)],
                    Person = _persons[rand.Next(0, 4)],
                    Accepted = 1 % 3 == 0,
                    Quantity1 = rand.Next(100, 999),
                    Quantity2 = rand.Next(100, 999),
                    Quantity3 = rand.Next(100, 999),
                    Unit = "Copy",
                    DeliveryDate = orderDate.AddDays(45),
                    ProcessingDate = orderDate.AddDays(15),
                    ShippingDate = orderDate.AddDays(30),
                    PackingRequest = ""
                });
            }

            return orderDetails;
        }

        public static IList<Order> GetOrders(int count)
        {
            var rand = new Random(0);
            var orders = new List<Order>();
            for(int i = 0; i < count; i++)
            {
                var product = _products[rand.Next(0, 6)];
                var quantity = rand.Next(100, 999);
                var orderDate = new DateTime(2016, i % 12 + 1, i % 28 + 1);
                orders.Add(new Order
                {
                    OrderId = string.Format("C{0:d4}", i + 1),
                    ProductId = product.Id,
                    ProductName = product.Name,
                    SpecialNote = "",
                    Quantity = quantity,
                    UnitPrice = product.UnitPrice,
                    ShippingWarehouse = rand.Next(0, 4),
                    Amount = quantity * product.UnitPrice,
                    OnHold = rand.Next(0, 10) > 5,
                    OrderDate = orderDate,
                    DeliveryDate = orderDate.AddDays(45)
                });
            }

            return orders;
        }

        public static IList<Sale> GetSalesSlip(int count)
        {
            var rand = new Random(0);
            var sales = new List<Sale>();
            for(int i = 0; i < count; i++)
            {
                var product = _products[rand.Next(0, 6)];
                var quantity = rand.Next(10, 50);
                var profitUnitPrice = product.ProfitUnitPrice * 10;
                var unitPrice = product.UnitPrice * 10;
                var salesAmount = unitPrice * quantity;
                var totalProfit = profitUnitPrice * quantity;

                sales.Add(new Sale
                {
                    Id= i + 1,
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = quantity,
                    ProfitUnitPrice = profitUnitPrice,
                    UnitPrice = unitPrice,
                    TotalProfit = totalProfit,
                    SalesAmount = salesAmount,
                    ProfitRate = totalProfit / salesAmount,
                    Unit = "box",
                    Marker =""
                });
            }

            return sales;
        }
    }
}