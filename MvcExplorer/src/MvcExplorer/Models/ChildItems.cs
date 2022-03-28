using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static MvcExplorer.Models.Worker;
using static MvcExplorer.Models.GrandParent;

namespace MvcExplorer.Models
{
    public class ChildItems
    {
        public List<GrandParent> Parents { get; set; }
        public List<Worker> Workers { get; set; }

        public ChildItems()
        {
            // family tree data (homogeneous collection)
            Parents = new List<GrandParent>()
            {
                new GrandParent{
                    name= "Albert",
                    children =  new List<Parent>
                    {
                        new Parent{ name = "Anton" },
                        new Parent{ name = "Annette" }
                    }
                 },
                new GrandParent{
                    name = "Benjamin",
                    children = new List<Parent>
                    {
                        new Parent{
                            name="Bridget",
                            children=new List<Children>
                            {
                                new Children{ name ="Billy"},
                                new Children{ name ="Bernard"}
                            }
                        },
                        new Parent{ name = "Bella" },
                        new Parent{ name = "Bob" }
                    }
                 },
                new GrandParent{
                    name= "Charlie",
                    children = new List<Parent>
                    {
                        new Parent{ name = "Chris" },
                        new Parent{ name = "Connie" },
                        new Parent{ name = "Carrie" }
                    }
                 },
                new GrandParent{
                    name= "Douglas",
                    children = new List<Parent>
                    {
                        new Parent{ name = "Dinah" },
                        new Parent{ name = "Donald" }
                    }
                 }
            };

            // worker tree data (heterogeneous collection)
            Workers = new List<Worker>()
            {
                new Worker{
                    name= "Jack Smith",
                    checks =  new List<Check>
                    {
                        new Check{
                            name="check1",
                            earning=new List<Earning>
                            {
                                new Earning{ name ="hourly", hours=4, rate=7},
                                new Earning{ name ="overtime", hours=4, rate=7},
                                new Earning{ name ="bonus", hours=4, rate=7}
                            }
                        },
                        new Check{
                            name="check2",
                            earning=new List<Earning>
                            {
                                new Earning{ name ="hourly", hours=4, rate=7},
                                new Earning{ name ="overtime", hours=4, rate=7}
                            }
                        }
                    }
                 },
                new Worker{
                    name = "Bill Smith",
                    checks = new List<Check>
                    {
                        new Check{
                            name="check1",
                            earning=new List<Earning>
                            {
                                new Earning{ name ="hourly", hours=4, rate=7},
                                new Earning{ name ="overtime", hours=4, rate=7},
                                new Earning{ name ="bonus", hours=4, rate=7}
                            }
                        }
                    }
                 },
                new Worker{
                    name= "Jack Smith",
                    checks = new List<Check>
                    {
                        new Check{
                            name="check1",
                            earning=new List<Earning>
                            {
                                new Earning{ name ="hourly", hours=4, rate=7},
                                new Earning{ name ="overtime", hours=4, rate=7}
                            }
                        },
                        new Check{
                            name="check2",
                            earning=new List<Earning>
                            {
                                new Earning{ name ="hourly", hours=4, rate=7},
                                new Earning{ name ="overtime", hours=4, rate=7}
                            }
                        }
                    }
                 },
                new Worker{
                    name= "Tom Smith",
                    checks = new List<Check>
                    {
                        new Check
                        {
                            name = "check1",
                            earning=new List<Earning>
                            {
                                new Earning{ name ="hourly", hours=4, rate=7},
                                new Earning{ name ="overtime", hours=4, rate=7},
                                new Earning{ name ="bonus", hours=4, rate=7}
                            }
                        },
                        new Check
                        {
                            name = "check2",
                            earning=new List<Earning>
                            {
                                new Earning{ name ="hourly", hours=4, rate=7},
                                new Earning{ name ="overtime", hours=4, rate=7},
                                new Earning{ name ="bonus", hours=4, rate=7}
                            }
                        }
                    }
                 }
            };
        }
    }
    public class GrandParent
    {
        public string name { get; set; }
        public List<Parent> children { get; set; }

        public partial class Parent
        {
            public string name { get; set; }
            public List<Children> children { get; set; }
        }
        public partial class Children
        {
            public string name { get; set; }
        }
    }

    public class Worker
    {
        public string name { get; set; }
        public List<Check> checks { get; set; }
        public partial class Check
        {
            public string name { get; set; }
            public List<Earning> earning { get; set; }
        }
        public partial class Earning
        {
            public string name { get; set; }
            public decimal hours { get; set; }
            public decimal rate { get; set; }
        }
    }

}