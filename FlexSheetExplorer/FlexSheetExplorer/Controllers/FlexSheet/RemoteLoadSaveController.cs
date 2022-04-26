using C1.C1Excel;
using C1.Web.Mvc.Serialization;
using C1.Web.Mvc.Sheet;
using FlexSheetExplorer.Models;
using System;
using System.IO;
using System.Web.Mvc;

namespace FlexSheetExplorer.Controllers
{
    public partial class FlexSheetController : Controller
    {
        private const string _FILE_PATH = "~/Content/uploadFile/save.xlsx";

        public ActionResult RemoteLoadSave(string loadType)
        {
            ViewBag.LoadSaveTypes = new string[] { "Xlsx", "Workbook" };

            if (loadType == "Workbook")
            {
                ViewBag.LoadAction = "RemoteLoadWorkbook";
            }
            else
            {
                loadType = "Xlsx";
                ViewBag.LoadAction = "RemoteLoadXlsx";
            }
            ViewBag.LoadType = loadType;

            return View();
        }

        public ActionResult RemoteLoadWorkbook() {
            return this.C1Json(FlexSheetHelper.Load( WorkbookOM.GetWorkbook()),
                   null, null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoteLoadXlsx() {
            return this.C1Json(FlexSheetHelper.Load("~/Content/xlsxFile/example1.xlsx"),
                   null, null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoteSaveFile([FlexSheetRequest]FlexSheetSaveRequest request)
        {
            var success = true;
            var error = "";
            var savePath = Server.MapPath(_FILE_PATH);
            try
            {
                Stream st = request.GetFileStream();
                Workbook wb = request.Workbook;
                using (FileStream fs = new FileStream(savePath, FileMode.Create))
                {
                    if (st != null)
                    {
                        byte[] byteArray = new Byte[st.Length];
                        st.Read(byteArray, 0, (int)st.Length);
                        fs.Write(byteArray, 0, byteArray.Length);
                    }
                    else if (wb != null)
                    {
                        wb.ToC1XLBook().Save(fs, FileFormat.OpenXml);
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                error = e.ToString();
            }

            return this.C1Json(FlexSheetHelper.Save(success, error));
        }

        public FileResult DownloadFile()
        {
            var name = Path.GetFileName(_FILE_PATH);
            return File(Server.MapPath(_FILE_PATH), "application/msexcel", Url.Encode(name));
        }
    }
}
