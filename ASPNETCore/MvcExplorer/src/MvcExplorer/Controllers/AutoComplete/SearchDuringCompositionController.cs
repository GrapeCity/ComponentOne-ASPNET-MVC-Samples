using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class AutoCompleteController : Controller
    {
        // One list per IME language. The words in each list share a leading
        // syllable, so the drop-down keeps narrowing while that syllable is still
        // being composed.
        private static readonly IDictionary<string, IList<string>> _composingWords =
            new Dictionary<string, IList<string>>
            {
                {
                    "korean", new List<string>
                    {
                        "가", "가나다", "가나다라", "가족", "강아지",
                        "한국", "한국어", "한강",
                        "서울", "서울역", "서울시청"
                    }
                },
                {
                    "japanese", new List<string>
                    {
                        "あい", "あいさつ", "あいじょう", "あさ", "あさひ",
                        "かい", "かいしゃ", "かいぎ",
                        "とうきょう", "とうきょうえき"
                    }
                },
                {
                    "chinese", new List<string>
                    {
                        "中", "中国", "中国人", "中文",
                        "北京", "北京市", "北京大学",
                        "上海", "上海市", "汉语"
                    }
                }
            };

        public ActionResult SearchDuringComposition()
        {
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"Language", new object[]{"Korean", "Japanese", "Chinese"}}
                }
            };

            ViewBag.ComposingWords = _composingWords;
            return View(_composingWords["korean"]);
        }
    }
}
