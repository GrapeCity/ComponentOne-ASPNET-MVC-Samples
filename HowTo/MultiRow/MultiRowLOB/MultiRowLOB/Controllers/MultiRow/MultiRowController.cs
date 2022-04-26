using MultiRowLOB.Models;
using System;
using System.Web.Mvc;
using System.Threading;

namespace MultiRowLOB.Controllers
{
    public partial class MultiRowController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            var culture = Request.QueryString["culture"];
            if (!string.IsNullOrEmpty(culture))
            {
                C1Culture.CurrentCulture = culture;
            }

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(CultureHelper.GetImplementedCulture(C1Culture.CurrentCulture));
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }
    }
}
