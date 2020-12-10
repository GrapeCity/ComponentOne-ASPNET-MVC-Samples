using System.Collections.Generic;

namespace MvcExplorer.Models
{
    public class Products
    {
        public static List<string> GetProducts()
        {
            return new List<string>
            {
                "PlayStation 4", "XBOX ONE", "Wii U", "PlayStation Vita", "PlayStation 3", "XBOX 360", "PlayStation Portable", "3 DS",
                "Dream Cast", "Nintendo 64", "Wii", "PlayStation 2", "PlayStation 1", "XBOX"
            };
        }
    }
}