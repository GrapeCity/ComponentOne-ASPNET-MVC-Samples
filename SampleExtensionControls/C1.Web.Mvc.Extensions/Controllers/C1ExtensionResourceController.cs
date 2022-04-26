using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace C1.Web.Mvc.Extensions.Controllers
{
    /// <summary>
    /// The controller to handle returning embedded script resources.
    /// </summary>
    public class C1ExtensionResourceController : Controller
    {
        /// <summary>
        /// The action to return script resources.
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public ActionResult Scripts(string f)
        {
            string manifestResourceName = string.Concat("C1.Web.Mvc.Extensions.Scripts.", f);
            var stream = typeof(C1ExtensionResourceController).Assembly.GetManifestResourceStream(manifestResourceName);
            int read;
            int bufferLength = 1024;
            byte[] buffer = new byte[bufferLength];
            MemoryStream ms = new MemoryStream();
            while ((read = stream.Read(buffer, 0, bufferLength)) > 0)
            {
                ms.Write(buffer, 0, read);
            }

            return new FileContentResult(ms.ToArray(), "application/javascript;");
        }
    }
}
