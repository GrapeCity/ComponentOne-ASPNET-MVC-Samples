using System.Web.Mvc;
using System.Data.Entity.Validation;
using System.Linq;
using MvcExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        public ActionResult DetailRow()
        {
            var customers = db.Customers.Take(10).ToList();
            var model = customers.Select(c => new CustomerWithOrders
            {
                CustomerID = c.CustomerID,
                CompanyName = c.CompanyName,
                Country = c.Country,
                City = c.City,
                Orders = (db.Orders.Where(o => o.CustomerID == c.CustomerID).ToList())
            });
            return View(model);
        }

    }
}
