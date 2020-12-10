using System.Linq;
using Microsoft.AspNetCore.Mvc;
using C1.Web.Mvc.Serialization;
using System.Reflection;

namespace MvcExplorer.Controllers
{
    public partial class AutoCompleteController : Controller
    {
        public ActionResult CustomAction()
        {
            return View();
        }

        public ActionResult Heuristic(string query, int max)
        {
            var prefix = new[] { "What is ", "Where to find ", "Who is best at ", "Why ", "How to make " };
            return this.C1Json(prefix.Select(f => f + query + "?").ToList());
        }

        public ActionResult TypesInMscorlib(string query, int max)
        {
            query = query ?? string.Empty;
            var types = typeof(object).GetTypeInfo().Assembly.GetTypes();
            return this.C1Json(types
                .Where(t => t.FullName.ToUpper().Contains(query.ToUpper()))
                .Select(t => t.FullName)
                .Take(max).ToList());
        }
    }
}
