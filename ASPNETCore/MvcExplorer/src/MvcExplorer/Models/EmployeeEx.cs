using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MvcExplorer.Models
{
    public class EmployeeEx
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public EmployeeEx[] SubExployees { get; set; }

        public static IEnumerable<EmployeeEx> GetEmployees(int? leaderID, DbSet<Employee> employees)
        {
            return employees.Where(e => e.ReportsTo == leaderID).ToList().Select(e => new EmployeeEx
            {
                EmployeeID = e.EmployeeID,
                Name = e.FirstName + " " + e.LastName,
                SubExployees = employees.Any(ep => ep.ReportsTo == e.EmployeeID) ? new EmployeeEx[0] : null
            });
        }
    }
}