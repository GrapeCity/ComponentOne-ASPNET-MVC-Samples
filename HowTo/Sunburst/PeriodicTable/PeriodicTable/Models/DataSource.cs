using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace PeriodicTable.Models
{
    public class DataSource
    {
        private List<Group> _groups;

        public List<Group> Groups
        {
            get
            {
                if (_groups == null)
                    _groups = new List<Group>();
                return _groups;
            }
        }

        public DataSource()
        {
            var metals = new Group("Metals");
            //Add subgroups to metals
            metals.SubGroups.Add(new SubGroup("Alkali Metal") { Characteristics = "Shiny, Soft, Highly Reactive,Low Melting Point" });
            metals.SubGroups.Add(new SubGroup("Alkaline Earth Metal") { Characteristics = "Ductile,Malleable,Low Density,High Melting Point" });
            metals.SubGroups.Add(new SubGroup("Transition Metal") { Characteristics = "High Melting Point, High Density" });
            metals.SubGroups.Add(new SubGroup("Lanthanide") { Characteristics = "Soluble, Highly Reactive," });
            metals.SubGroups.Add(new SubGroup("Actinide") { Characteristics = "Radioactive,Paramagnetic" });
            metals.SubGroups.Add(new SubGroup("Others") { Characteristics = "Brittle, Poor Metals,Low Melting Point" });

            var nonmetals = new Group("Non Metals");
            //Add subgroups to non-metals
            nonmetals.SubGroups.Add(new SubGroup("Halogen") { Characteristics = "Toxic,Highly Reactive,Poor Conductors" });
            nonmetals.SubGroups.Add(new SubGroup("Noble Gas") { Characteristics = "Colorless,Odorless,Low Chemical Reactivity" });
            nonmetals.SubGroups.Add(new SubGroup("Others") { Characteristics = "Volatile, Low Elasticity, Good Insulators" });

            var others = new Group("Others");
            //Add subgroups to others
            others.SubGroups.Add(new SubGroup("Metalloids") { Characteristics = "Metallic looking solids, SemiConductors" });
            others.SubGroups.Add(new SubGroup("Transactinides") { Characteristics = "Radioactive,Synthetic Elements" });

            Groups.Add(metals);
            Groups.Add(nonmetals);
            Groups.Add(others);
            GroupElements();
            for (int gCount = 0; gCount < Groups.Count; gCount++)
            {
                for (int sgCount = 0; sgCount < Groups[gCount].SubGroups.Count; sgCount++)
                {
                    var temp = Groups[gCount].SubGroups[sgCount];
                    Groups[gCount].SubGroups[sgCount].Count = temp.Elements.Count;
                }
            }
        }

        void GroupElements()
        {
            var path = HttpContext.Current.Server.MapPath("~/Content/Periodic Table of Elements.xml");
            var elementsCollection = Utils.DeserializeXml(path);
            var metals = "Alkali Metal|Alkaline Earth Metal|Metal|Transition Metal|Lanthanide|Actinide".Split('|');
            var nonmetals = "Nonmetal|Noble Gas|Halogen".Split('|');
            Group group;
            SubGroup sub;
            for (int i = 0; i < elementsCollection.Elements.Length; i++)
            {
                var e = elementsCollection.Elements[i];
                if (metals.Contains(e.Element.Type))
                {
                    group = Groups[0];
                    if (e.Element.Type == "Metal")
                        sub = group.SubGroups.Find(s => s.SubGroupName.Equals("Others"));
                    else
                        sub = group.SubGroups.Find(s => s.SubGroupName.Equals(e.Element.Type));
                    sub.Elements.Add(e.Element);
                }
                else if (nonmetals.Contains(e.Element.Type))
                {
                    group = Groups[1];
                    if (e.Element.Type == "Nonmetal")
                        sub = group.SubGroups.Find(s => s.SubGroupName.Equals("Others"));
                    else
                        sub = group.SubGroups.Find(s => s.SubGroupName.Equals(e.Element.Type));
                    sub.Elements.Add(e.Element);
                }
                else
                {
                    group = Groups[2];
                    if (e.Element.Type == "Metalloid")
                        sub = group.SubGroups.Find(s => s.SubGroupName.Equals("Metalloids"));
                    else
                        sub = group.SubGroups.Find(s => s.SubGroupName.Equals("Transactinides"));
                    sub.Elements.Add(e.Element);
                }
            }
        }
    }

    public class Group
    {
        private List<SubGroup> _subGroups;
        public string GroupName { get; set; }
        public List<SubGroup> SubGroups
        {
            get
            {
                if (_subGroups == null)
                    _subGroups = new List<SubGroup>();
                return _subGroups;
            }

        }
        public Group() { }
        public Group(string name)
        {
            GroupName = name;
        }
    }

    public class SubGroup
    {
        private List<Element> _elements;

        public string SubGroupName { get; set; }

        public string Characteristics { get; set; }

        public int Count { get; set; }

        public List<Element> Elements
        {
            get
            {
                if (_elements == null)
                    _elements = new List<Element>();
                return _elements;
            }
        }

        public SubGroup() { }

        public SubGroup(string name)
        {
            SubGroupName = name;
        }
    }
}