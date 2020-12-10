using C1.Web.Mvc;
using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;
using C1.Web.Mvc.Serialization;

namespace MvcExplorer.Controllers
{
    partial class TreeViewController : Controller
    {
        private readonly C1NWindEntities _db;

        public TreeViewController(C1NWindEntities db)
        {
            _db = db;
        }

        // GET: LazyLoad
        public ActionResult LazyLoading()
        {
            return View();
        }

        public ActionResult Load()
        {
            return this.C1Json(EmployeeEx.GetEmployees(null, _db.Employees), false);
        }

        public ActionResult LazyLoading_LoadAction([C1JsonRequest]TreeNode node)
        {
            var leaderID = (int?)node.DataItem["EmployeeID"];
            return this.C1Json(EmployeeEx.GetEmployees(leaderID, _db.Employees), false);
        }
    }
}