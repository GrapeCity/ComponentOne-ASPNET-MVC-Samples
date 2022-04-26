using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExcelBook.Startup))]
namespace ExcelBook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
