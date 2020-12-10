using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class ListBoxController : Controller
    {
        public ActionResult ItemTemplate()
        {
            var list = CustomerOrder.GetOrderData(100).ToList();
            return View(list);
        }
    }
}
