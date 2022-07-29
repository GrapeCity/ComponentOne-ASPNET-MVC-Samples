using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Models
{
    public class Property
    {
        public string Header { get; set; }
        public string Image { get; set; }
        public bool NewItem { get; set; }
        public bool Checked { get; set; }
        public Property[] Items { get; set; }
        public static Property[] GetData(IUrlHelper urlHelper)
        {
            return new Property[]
            {
                new Property
                {
                    Header = "Electronics",
                    Image = urlHelper.Content("~/Content/images/electronics.png"),
                    Items = new Property[]
                    {
                        new Property { Header="Trimmers/Shavers" },
                        new Property { Header="Tablets" },
                        new Property { Header="Phones",
                            Image = urlHelper.Content("~/Content/images/phones.png"),
                            Items = new Property[] {
                                new Property { Header="Apple" },
                                new Property { Header="Motorola", NewItem=true, Checked = true },
                                new Property { Header="Nokia" },
                                new Property { Header="Samsung" }}
                        },
                        new Property { Header="Speakers", NewItem=true, Checked = true },
                        new Property { Header="Monitors" }
                    }
                },
                new Property{
                    Header = "Toys",
                    Image = urlHelper.Content("~/Content/images/toys.png"),
                    Items = new Property[]{
                        new Property{ Header = "Shopkins" },
                        new Property{ Header = "Train Sets" },
                        new Property{ Header = "Science Kit", NewItem = true },
                        new Property{ Header = "Play-Doh" },
                        new Property{ Header = "Crayola" }
                    }
                },
                new Property{
                    Header = "Home",
                    Image = urlHelper.Content("~/Content/images/home.png"),
                    Items = new Property[] {
                        new Property{ Header = "Coffeee Maker" },
                        new Property{ Header = "Breadmaker", NewItem = true },
                        new Property{ Header = "Solar Panel", NewItem = true },
                        new Property{ Header = "Work Table" },
                        new Property{ Header = "Propane Grill" }
                    }
                }
            };
        }
    }
}