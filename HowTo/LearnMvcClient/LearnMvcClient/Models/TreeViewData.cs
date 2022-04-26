using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnMvcClient.Models
{
    public class TreeViewData
    {
        public string Header { get; set; }
        public string Keywords { get; set; }
        public string Image { get; set; }
        public bool NewItem { get; set; }
        public TreeViewData[] Items { get; set; }

        public static TreeViewData[] GetData(UrlHelper urlHelper)
        {
            return new TreeViewData[]
            {
                new TreeViewData
                {
                    Header = "Electronics",
                    Image = urlHelper.Content("~/Content/images/electronics.png"),
                    Items = new TreeViewData[]
                    {
                        new TreeViewData { Header="Trimmers/Shavers", Keywords="beard hair" },
                        new TreeViewData { Header="Tablets", Keywords="screen computer android ios facebook" },
                        new TreeViewData { Header="Phones", Keywords="talk listen email fabebook",
                            Image =urlHelper.Content("~/Content/images/phones.png"),
                            Items = new TreeViewData[] {
                                new TreeViewData { Header="Apple" },
                                new TreeViewData { Header="Motorola", NewItem=true },
                                new TreeViewData { Header="Nokia" },
                                new TreeViewData { Header="Samsung" }}
                        },
                        new TreeViewData { Header="Speakers", Keywords="music loudspeaker", NewItem=true },
                        new TreeViewData { Header="Monitors", Keywords="screen color lcd oled" }
                    }
                },
                new TreeViewData{
                    Header = "Toys",
                    Image = urlHelper.Content("~/Content/images/toys.png"),
                    Items = new TreeViewData[]{
                        new TreeViewData{ Header = "Shopkins", Keywords="animals collectibles" },
                        new TreeViewData{ Header = "Train Sets", Keywords="models rail collectibles" },
                        new TreeViewData{ Header = "Science Kit", Keywords="education physics chemistry", NewItem = true },
                        new TreeViewData{ Header = "Play-Doh", Keywords="clay sculpt models" },
                        new TreeViewData{ Header = "Crayola", Keywords="drawing painting wax chalk pencils" }
                    }
                },
                new TreeViewData{
                    Header = "Home",
                    Image = urlHelper.Content("~/Content/images/home.png"),
                    Items = new TreeViewData[] {
                        new TreeViewData{ Header = "Coffeee Maker", Keywords="kitchen appliance drink" },
                        new TreeViewData{ Header = "Breadmaker", Keywords="kitchen appliance food cooking", NewItem = true },
                        new TreeViewData{ Header = "Solar Panel", Keywords="electric sun renewable energy", NewItem = true },
                        new TreeViewData{ Header = "Work Table", Keywords="shop tools" },
                        new TreeViewData{ Header = "Propane Grill", Keywords="food cooking barbecue meat" }
                    }
                }
            };
        }
    }
}