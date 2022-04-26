using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexSheetExplorer.Models
{
    public class FontName
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public static List<FontName> GetFontNameList()
        {
            return new List<FontName>{
                new FontName{Name= "Arial", Value= "Arial, Helvetica, sans-serif" },
                new FontName{ Name= "Arial Black", Value= "\"Arial Black\", Gadget, sans-serif" },
                new FontName{ Name= "Comic Sans MS", Value= "\"Comic Sans MS\", cursive, sans-serif" },
                new FontName{ Name= "Courier New", Value= "\"Courier New\", Courier, monospace" },
                new FontName{ Name= "Georgia", Value= "Georgia, serif" },
                new FontName{ Name= "Impact", Value= "Impact, Charcoal, sans-serif" },
                new FontName{ Name= "Lucida Console", Value= "\"Lucida Console\", Monaco, monospace" },
                new FontName{ Name= "Lucida Sans Unicode", Value= "\"Lucida Sans Unicode\", \"Lucida Grande\", sans-serif" },
                new FontName{ Name= "Palatino Linotype", Value= "\"Palatino Linotype\", \"Book Antiqua\", Palatino, serif" },
                new FontName{ Name= "Tahoma", Value= "Tahoma, Geneva, sans-serif" },
                new FontName{ Name= "Segoe UI", Value= "\"Segoe UI\", \"Roboto\", sans-serif" },
                new FontName{ Name= "Times New Roman", Value= "\"Times New Roman\", Times, serif" },
                new FontName{ Name= "Trebuchet MS", Value= "\"Trebuchet MS\", Helvetica, sans-serif" },
                new FontName{ Name= "Verdana", Value= "Verdana, Geneva, sans-serif" }
            };
        }
    }

    public class FontSize
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public static List<FontSize> GetFontSizeList()
        {
            return new List<FontSize>{ 
                new FontSize{Name= "8", Value= "8px" }, new FontSize{ Name= "9", Value= "9px" },
                new FontSize{ Name= "10", Value= "10px" },new FontSize{ Name= "11", Value= "11px" },
                new FontSize{ Name= "12", Value= "12px" }, new FontSize{ Name= "14", Value= "14px" },
                new FontSize{ Name= "16", Value= "16px" }, new FontSize{ Name= "18", Value= "18px" }, 
                new FontSize{ Name= "20", Value= "20px" },new FontSize{ Name= "22", Value= "22px" },
                new FontSize{ Name= "24", Value= "24px" }
            };
        }
    }
}