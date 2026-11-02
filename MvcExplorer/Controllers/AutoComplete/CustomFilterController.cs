using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class AutoCompleteController : Controller
    {
        // Each row is the same word written three ways: hiragana, katakana and
        // half-width katakana. Typing any one form should match all three.
        private static readonly List<string> _kanaWords = new List<string>
        {
            "あいう", "アイウ", "ｱｲｳ",
            "あいえ", "アイエ", "ｱｲｴ",
            "かきく", "カキク", "ｶｷｸ",
            "さしす", "サシス", "ｻｼｽ"
        };

        public ActionResult CustomFilter()
        {
            ViewBag.KanaWords = _kanaWords;
            return View();
        }
    }
}
