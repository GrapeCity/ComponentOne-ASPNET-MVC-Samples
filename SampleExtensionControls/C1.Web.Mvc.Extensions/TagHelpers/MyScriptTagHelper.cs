using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Caching.Memory;
using C1.Web.Mvc.Extensions.FileProvider;

namespace C1.Web.Mvc.Extensions.TagHelpers
{
    /// <summary>
    /// custom tag helper for create script tag of Resources/Scripts/input.js
    /// </summary>
    [HtmlTargetElement("input-script")]
    public class MyInputTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "script";
            output.Attributes.Add("src", "Resources/Scripts/input.js");
        }
    }

    /// <summary>
    /// custom tag helper for create script tag of Resources/Scripts/mvc.input.js
    /// </summary>
    [HtmlTargetElement("mvc-input-script")]
    public class MyMvcInputTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "script";
            output.Attributes.Add("src", "Resources/Scripts/mvc.input.js");
        }
    }
}