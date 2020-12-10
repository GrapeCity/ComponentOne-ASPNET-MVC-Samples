using System;
using System.Collections.Generic;
using System.Linq;
using MvcExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
		public ActionResult DetailRow()
		{
			var customers = _db.Customers.Take(10).ToList();
			var model = customers.Select(c => new CustomerWithOrders
			{
				CustomerID = c.CustomerID,
				CompanyName = c.CompanyName,
				Country = c.Country,
				City = c.City,
				Orders = (_db.Orders.Where(o => o.CustomerID == c.CustomerID).ToList())
			});
			return View(model);
		}
	}
}
