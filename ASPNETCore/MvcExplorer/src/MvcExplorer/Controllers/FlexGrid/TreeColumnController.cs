using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        //
        // GET: /TreeColumn/

        public ActionResult TreeColumn()
        {
            ViewBag.DemoSettings = true;
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"TreeColumnIndex", new object[]{-1, 0, 1, 2}}
                }
            };

            var items = Folder.Create(Startup.Environment.ContentRootPath).Children;
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
