using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColumnLayout.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ColumnLayout = LoadColumnLayoutFromFile();
            return View(Sale.GetData(50));
        }

        public JsonResult SaveColumLayout(string columnLayout)
        {
            bool result = true;
            string exception = null;
            JsonResult res = new JsonResult();
            try
            {
                SaveColumnLayoutToFile(columnLayout);
            }
            catch (Exception e)
            {
                result = false;
                exception = e.ToString();
            }
            res.Data = new { Result = result, Exception = exception };
            return (res);
        }

        private string LoadColumnLayoutFromFile()
        {
            string path = getFilePath();
            string columnLayout = "";
            if (System.IO.File.Exists(path))
            {
                StreamReader sr = new StreamReader(path, System.Text.Encoding.Default);
                columnLayout = sr.ReadToEnd();
                sr.Close();
            }
            return columnLayout;
        }

        private void SaveColumnLayoutToFile(string columnLayout)
        {
            string path = getFilePath();
            if (!System.IO.File.Exists(path))
            {
                System.IO.File.Create(path);
            }
            else
            {
                FileInfo file = new FileInfo(path);
                file.IsReadOnly = false;
            }
            StreamWriter sr = new StreamWriter(path, false, System.Text.Encoding.Default);
            sr.Write(columnLayout);
            sr.Close();
        }

        private string getFilePath()
        {
            return Server.MapPath("~/ColumnLayout.json");
        }
    }
}
