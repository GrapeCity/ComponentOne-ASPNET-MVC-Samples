using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {        
        int? nodeId;
        private static readonly Random random = new Random();
        // GET: LazyLoading
        public ActionResult LazyLoading()
        {
            List<LazyLoadingItems> items = new List<LazyLoadingItems>();
            items.Add(CreateNode());
            items.Add(CreateNode());
            return View(items);
        }

        private LazyLoadingItems CreateNode(bool dummy = false)
        {
            var first = "Al,Bob,Cal,Dan,Ed,Fred,Greg,Hal,Ian,Jack,Karl,Lou,Moe,Nate,Oleg,Paul,Quincy,Rod,Sue,Uwe,Vic,Walt,Xiu,Yuri,Zack".Split(',');

            var last = "Adams,Baum,Cole,Dell,Evans,Fell,Green,Hill,Isman,Jones,Krup,Lee,Monk,Nye,Opus,Pitt,Quaid,Riems,Stark,Trell,Unger,Voss,Wang,Xie,Zalm".Split(',');

            var name = dummy ? "" : first[random.Next(0, 24)] + ' ' + last[random.Next(0, 24)];
            var childrenList = new List<LazyLoadingItems>();
            if (!dummy)
            {
                childrenList.Add(CreateNode(true));
            }
            if (nodeId == null)
                nodeId = 0;
            var node = new LazyLoadingItems
            {
                id = (int)nodeId,
                name = name,
                children = new List<LazyLoadingItems>()
            };
            node.children.AddRange(childrenList);
            nodeId++;
            return node;
        }
        [HttpGet]
        public JsonResult GetTreeNodes(int id)
        {
            nodeId = id + 1;
            var newNode = CreateNode();
            string str = JsonConvert.SerializeObject(newNode);
            return Json(str);
        }

    }
    public class LazyLoadingItems
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<LazyLoadingItems> children { get; set; }

    }
}
