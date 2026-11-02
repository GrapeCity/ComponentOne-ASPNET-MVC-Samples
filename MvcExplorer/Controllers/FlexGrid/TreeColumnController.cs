using System.Collections.Generic;
using System.Web.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        public ActionResult TreeColumn()
        {
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"TreeColumnIndex", new object[]{-1, 0, 1, 2}}
                }
            };

            var items = Folder.Create(Server.MapPath("~")).Children;
            NumberTreeItems(items, 1);
            return View(items);
        }

        // Numbers the items depth first, so the "#" column follows the order the
        // rows appear in when the tree is fully expanded.
        private static int NumberTreeItems(IList<ITreeItem> items, int next)
        {
            foreach (var item in items)
            {
                item.Index = next++;
                if (item.Children != null)
                {
                    next = NumberTreeItems(item.Children, next);
                }
            }
            return next;
        }
    }
}
