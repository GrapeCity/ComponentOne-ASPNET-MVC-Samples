using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CloudFileExplorer.Controllers.FileManager
{
    public class FileManagerController : Controller
    {
        public IActionResult LocalStorage()
        {
            return View();
        }
    }
}