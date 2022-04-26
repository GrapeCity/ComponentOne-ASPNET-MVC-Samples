using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiRowExplorer.Models
{
    public class Orders
    {
        private static object _lockObj = new object();
        private static Random Rand = new Random();

        private static IList<Order> _orders;
        private static IList<string> _cities;
        private static IList<Customer> _customers;
        private static IList<Shipper> _shippers;

        public class Shipper
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public bool Express { get; set; }
        }

        public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
        }

        public class Order
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public DateTime ShippedDate { get; set; }
            public double Amount { get; set; }
            public Customer Customer { get; set; }
            public Shipper Shipper { get; set; }
        }

        public static IList<Order> GetOrders()
        {
            if (_orders == null)
            {
                lock (_lockObj)
                {
                    if (_orders == null)
                    {
                        var today = DateTime.Now.Date;
                        var customers = GetCustomers();
                        var shippers = GetShippers();

                        _orders = new List<Order>();
                        for (int i = 0; i < 500; i++)
                        {
                            var shipped = today.AddDays(-Rand.Next(-1, 3000));
                            var order = new Order
                            {
                                Id = i,
                                Date = shipped.AddDays(-Rand.Next(1, 5)),
                                ShippedDate = shipped,
                                Amount = Rand.Next(10000, 500000) / 100,
                                Customer = customers[Rand.Next(0, customers.Count - 1)],
                                Shipper = shippers[Rand.Next(0, shippers.Count - 1)]
                            };
                            _orders.Add(order);
                        }
                    }
                }
            }

            return _orders;
        }

        public static IList<Shipper> GetShippers()
        {
            if(_shippers == null)
            {
                lock (_lockObj)
                {
                    if (_shippers == null)
                    {
                        _shippers = new List<Shipper>();
                        _shippers.Add(new Shipper { Id = 0, Name = "Speedy Express", Email = "express.speedy@gmail.com", Phone = "431-3234", Express = true });
                        _shippers.Add(new Shipper { Id = 1, Name = "Flash Delivery", Email = "flash@gmail.com", Phone = "431-6563", Express = true });
                        _shippers.Add(new Shipper { Id = 2, Name = "Logitrax", Email = "logitrax@gmail.com", Phone = "431-3981", Express = false });
                        _shippers.Add(new Shipper { Id = 3, Name = "Acme Inc", Email = "acme@gmail.com", Phone = "431-3113", Express = false });
                    }
                }
            }
            
            return _shippers;
        }

        public static IList<Customer> GetCustomers()
        {
            if (_customers == null)
            {
                lock (_lockObj)
                {
                    if (_customers == null)
                    {
                        var firstNames = new[] { "Aaron", "Paul", "John", "Mark", "Sue", "Tom", "Bill", "Joe", "Tony", "Brad", "Frank", "Chris", "Pat" };
                        var lastNames = new[] { "Smith", "Johnson", "Richards", "Bannon", "Wong", "Peters", "White", "Brown", "Adams", "Jennings" };
                        var cities = GetCities();
                        var states = new[] { "SP", "RS", "RN", "SC", "CS", "RT", "BC" };

                        _customers = new List<Customer>();
                        for (int i = 0; i < 50; i++)
                        {
                            var first = firstNames[Rand.Next(0, firstNames.Length - 1)];
                            var last = lastNames[Rand.Next(0, lastNames.Length - 1)];
                            var customer = new Customer
                            {
                                Id = i,
                                Name = first + " " + last,
                                Address = Rand.Next(100, 10000) + " " + lastNames[Rand.Next(0, lastNames.Length - 1)] + " St.",
                                City = cities[Rand.Next(0, cities.Count - 1)],
                                State = states[Rand.Next(0, states.Length - 1)],
                                Zip = string.Format("{0:d5}-{1:d3}", Rand.Next(10000, 99999), Rand.Next(100, 999)),
                                Email = first + "." + last + "@gmail.com",
                                Phone = string.Format("{0:d3}-{1:d4}", Rand.Next(100, 999), Rand.Next(1000, 9999))
                            };
                            _customers.Add(customer);
                        }
                    }
                }
            }

            return _customers;
        }

        public static IList<string> GetCities()
        {
            if (_cities == null)
            {
                lock (_lockObj)
                {
                    if(_cities == null)
                    {
                        _cities = new[] { "York", "Paris", "Rome", "Cairo", "Florence", "Sidney", "Hamburg", "Vancouver" };
                    }
                }
            }

            return _cities;
        }
    }
}