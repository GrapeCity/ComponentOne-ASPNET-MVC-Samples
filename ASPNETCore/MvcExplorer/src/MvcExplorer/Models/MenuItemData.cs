namespace MvcExplorer.Models
{
    public class MenuItemData
    {
        public string Header { get; set; }
        public MenuItemData[] Items { get; set; }

        public static MenuItemData[] GetData()
        {
            return new MenuItemData[]
            {
                new MenuItemData{
                    Header = "Angular",
                    Items = new MenuItemData[] {
                    new MenuItemData{ Header = "<a href=\"ng/intro\">Introduction</a>" },
                    new MenuItemData{ Header = "<a href=\"ng/samples\">Samples</a>" },
                    new MenuItemData{ Header = "<a href=\"ng/perf\">Performance</a>" }}
                },
                new MenuItemData{
                    Header = "React",
                    Items = new MenuItemData[] {
                    new MenuItemData{ Header = "<a href=\"rc/intro\">Introduction</a>" },
                    new MenuItemData{ Header = "<a href=\"rc/samples\">Samples</a>" },
                    new MenuItemData{ Header = "<a href=\"rc/perf\">Performance</a>" }}
                },
                new MenuItemData{
                    Header = "Vue",
                    Items = new MenuItemData[] {
                    new MenuItemData{ Header = "<a href=\"vue/intro\">Introduction</a>" },
                    new MenuItemData{ Header = "<a href=\"vue/samples\">Samples</a>" },
                    new MenuItemData{ Header = "<a href=\"vue/perf\">Performance</a>" }}
                },
                new MenuItemData{
                    Header = "Knockout",
                    Items = new MenuItemData[] {
                        new MenuItemData{ Header = "<a href=\"ko/intro\">Introduction</a>" },
                        new MenuItemData{ Header = "<a href=\"ko/samples\">Samples</a>" },
                        new MenuItemData{ Header = "<a href=\"ko/perf\">Performance</a>" }}
                }
            };
        }
    }
}