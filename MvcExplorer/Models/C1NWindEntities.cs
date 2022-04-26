using System.Data.Entity;
using System.Web;

namespace MvcExplorer.Models
{
    public class C1NWindEntitiesOData : DbContext
    {
        public C1NWindEntitiesOData()
            :base("name=C1NWindEntities")
        {
            // ensure database file is not read-only
            // (in case someone forgets to check it out of source control)
            lock (typeof(C1NWindEntitiesOData))
            {
                var path = HttpContext.Current.Request.PhysicalApplicationPath;
                path = System.IO.Path.Combine(path, "App_Data");
                foreach (var fn in System.IO.Directory.GetFiles(path, "*.mdf"))
                {
                    var fi = new System.IO.FileInfo(fn);
                    fi.IsReadOnly = false;
                }
            }
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}