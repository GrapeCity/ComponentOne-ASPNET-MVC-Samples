using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class MenuController : Controller
    {
        public ActionResult Customize()
        {
            return View();
        }

        public ActionResult GetMenuItems([C1JsonRequest]CollectionViewRequest<MenuItemInfo> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, new MenuItemInfo[] {
          new MenuItemInfo()
          {
            Header = "File",
            Items = new List<MenuItemInfo>()
            {
              new MenuItemInfo() { Header = "Save" },
              new MenuItemInfo() { Header = "Open" }
            }
          },
          new MenuItemInfo()
          {
            Header = "Edit",
            Items = new List<MenuItemInfo>()
            {
              new MenuItemInfo() { Header = "Search" },
              new MenuItemInfo() { Header = "Replace" }
            }
          },
        }));
        }
    }

    public class MenuItemInfo
    {
        public string Header { get; set; }
        public List<MenuItemInfo> Items { get; set; }
    }
}
