using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace MvcExplorer.Controllers
{
    public partial class UndoController : Controller
    {
        // GET: Index
        public ActionResult Index(IFormCollection collection)
        {
            return View();
        }

        public ActionResult UndoStack_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Sale.GetData(50)));
        }
    }
}