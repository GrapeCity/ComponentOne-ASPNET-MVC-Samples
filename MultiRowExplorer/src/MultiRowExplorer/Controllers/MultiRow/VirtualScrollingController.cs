using MultiRowExplorer.Models;
using Microsoft.AspNetCore.Mvc;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;


namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        //
        // GET: /VirtualScrolling/

        public ActionResult VirtualScrolling()
        {
            return View();
        }

        public ActionResult VirtualScrolling_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Sale.GetData(100000)));
        }
    }
}
