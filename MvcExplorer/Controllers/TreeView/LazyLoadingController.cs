using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using MvcExplorer.Models;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    partial class TreeViewController : Controller
    {

        // GET: LazyLoad
        public ActionResult LazyLoading()
        {
            return View();
        }

        public ActionResult Load()
        {
            return this.C1Json(EmployeeEx.GetEmployees(null), useCamelCasePropertyName: false);
        }

        public ActionResult LazyLoading_LoadAction([C1JsonRequest]TreeNode node)
        {
            var leaderID = (int?)node.DataItem["EmployeeID"];
            return this.C1Json(EmployeeEx.GetEmployees(leaderID), useCamelCasePropertyName: false);
        }
    }
}