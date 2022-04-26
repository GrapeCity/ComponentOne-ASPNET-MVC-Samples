using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#if CORE1_0 || NETCOREAPP1_0
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
#else
using Microsoft.AspNetCore.Identity;
#endif

namespace DynamicDashboard.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
    }
}
