using LearnMvcClient.Models;
using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: CreateControls
        public ActionResult CreateControls()
        {
            ViewBag.Countries = Countries.GetCountries();
            ViewBag.Sales = Sale.GetData(50);
            ViewBag.FruitSales = Fruit.GetFruitsSales();
            return View();
        }
    }
}