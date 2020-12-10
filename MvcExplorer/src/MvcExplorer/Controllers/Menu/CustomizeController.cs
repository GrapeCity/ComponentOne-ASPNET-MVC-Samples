using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class MenuController : Controller
    {
        public ActionResult Customize()
        {
            MenuItemInfo[] menuItems = new MenuItemInfo[]
            {
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
                  }
            };
            return View(menuItems);
        }
    }

    public class MenuItemInfo
    {
        public string Header { get; set; }
        public List<MenuItemInfo> Items { get; set; }
    }
}
