using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GridInForm.Models;
using System.IO;
using Newtonsoft.Json;

namespace GridInForm.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new Company
            {
                Name = "Mescius",
                Description = @"MESCIUS inc. is a privately held, multinational software corporation based in Sendai, Japan, 
that develops its own software products and provides outsourced product development services, consulting
services, software, and Customer relationship management services. It also established WINEstudios, a media design and digital production facility in Japan.",
                WebSite = "https://developer.mescius.com/",
                Founded = new DateTime(1980, 1, 1),
                Headquarters = "Sendai, Japan",
                Type = "Private",
                Industry = "Software services",
                Sales = Sale.GetData(50).ToList()
            });
        }

        [HttpPost]
        public ActionResult Save(FormCollection collection)
        {
            var company = new Company();
            company.Name = (string)collection.GetValue("Name").ConvertTo(typeof(string));
            company.Description = (string)collection.GetValue("Description").ConvertTo(typeof(string));
            company.WebSite = (string)collection.GetValue("WebSite").ConvertTo(typeof(string));
            company.Founded = (DateTime)collection.GetValue("Founded").ConvertTo(typeof(DateTime));
            company.Headquarters = (string)collection.GetValue("Headquarters").ConvertTo(typeof(string));
            company.Type = (string)collection.GetValue("Type").ConvertTo(typeof(string));
            company.Industry = (string)collection.GetValue("Industry").ConvertTo(typeof(string));
            var salesString = (string)collection.GetValue("Sales").ConvertTo(typeof(string));
            using (var textReader = new StringReader(salesString))
            using (var reader = new JsonTextReader(textReader))
            {
                company.Sales = new JsonSerializer().Deserialize(reader, typeof(List<Sale>)) as List<Sale>;
            }
            return View(company);
        }
    }
}