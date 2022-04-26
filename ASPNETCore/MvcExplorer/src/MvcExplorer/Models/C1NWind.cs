using Microsoft.EntityFrameworkCore;
using System;

namespace MvcExplorer.Models
{
#if ODATA_SERVER && NETCORE10
    public interface IODataService
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Order> Orders { get; set; }
    }

    public partial class C1NWindEntities : IODataService
    {
    }
#endif
    public partial class C1NWindEntities : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }

        public C1NWindEntities(DbContextOptions<C1NWindEntities> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(b =>
            {
                b.Property<string>("CustomerID");
                b.HasKey("CustomerID");
                b.HasAnnotation("Relational:TableName", "Customers");
            });
            modelBuilder.Entity<Order>(b =>
            {
                b.HasAnnotation("Relational:TableName", "Orders");
            });
            modelBuilder.Entity<Category>(b =>
            {
                b.Property<int>("CategoryID");
                b.HasKey("CategoryID");
                b.HasAnnotation("Relational:TableName", "Categories");
            });
            modelBuilder.Entity<Employee>(b =>
            {
                b.HasAnnotation("Relational:TableName", "Employees");
            });
            modelBuilder.Entity<Product>(b =>
            {
                b.Property<int>("ProductID");
                b.HasKey("ProductID");
                b.HasAnnotation("Relational:TableName", "Products");
            });
            //modelBuilder.ForSqlServerUseIdentityColumns();//C1WEB-28027: change database by use Sqlite.
        }
    }

    public partial class Customer
    {
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
    }
    public partial class Order
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        public decimal? Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
    }
    public partial class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
    public partial class Employee
    {
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string Extension { get; set; }
        public byte[] Photo { get; set; }
        public string Notes { get; set; }
        public int? ReportsTo { get; set; }
    }
    public partial class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int? SupplierID { get; set; }
        public int? CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
    }
}
