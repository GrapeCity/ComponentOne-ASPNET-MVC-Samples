using C1.Web.Mvc.Serialization;
using C1.Web.Mvc.Sheet;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace FlexSheetExplorer.Controllers
{
    public partial class FlexSheetController : Controller
    {
        private const string FILE_PATH = "Content\\xlsxFile\\RemoteSave.xlsx";
        private readonly string _webRootPath;

#if NETCORE31
        public FlexSheetController(IWebHostEnvironment hostingEnvironment)
#else
        public FlexSheetController(IHostingEnvironment hostingEnvironment)
#endif
        {
            _webRootPath = hostingEnvironment.WebRootPath;
        }


        public ActionResult RemoteLoadSave()
        {
            return View();
        }

        public ActionResult RemoteLoadXlsx()
        {
            return this.C1Json(FlexSheetHelper.Load("~/Content/xlsxFile/example1.xlsx"));
        }

        public JsonResult RemoteSaveFile([FlexSheetRequest]FlexSheetSaveRequest request)
        {
            var success = true;
            var error = "";
            var savePath = Path.Combine(_webRootPath, FILE_PATH);
            try
            {
                Stream st = request.GetFileStream();
                using (FileStream fs = new FileStream(savePath, FileMode.Create))
                {
                    if (st != null)
                    {
                        st.CopyTo(fs);
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
            var savePath = Path.Combine(_webRootPath, FILE_PATH);
            var name = Path.GetFileName(FILE_PATH);
            return File(new FileStream(savePath, FileMode.Open, FileAccess.Read),
                "application/msexcel", name);
        }
    }
}
