using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcExplorer.Models
{
    public class UsersData
    {
        public static List<UserInfo> Users 
        {
            get
            {
                return new List<UserInfo>() 
                {
                    new UserInfo(){Id=1, Name="John", Email="John@gmail.com", Phone="1424685445", Country="Albania", Industry="Computers", Birthdate= DateTime.Parse("2001/1/1")},
                    new UserInfo(){Id=2, Name="Mary", Email="Mary@gmail.com", Phone="1296479754", Country="American", Industry="Electronics", Birthdate= DateTime.Parse("1985/3/2")},
                    new UserInfo(){Id=3, Name="David", Email="David@gmail.com", Phone="1217654653", Country="Australia", Industry="Telecom", Birthdate= DateTime.Parse("1999/3/1")},
                    new UserInfo(){Id=4, Name="Sunny", Email="Sunny@gmail.com", Phone="1756456786", Country="Bosnia", Industry="Internet", Birthdate= DateTime.Parse("1989/4/3")},
                    new UserInfo(){Id=5, Name="James", Email="James@gmail.com", Phone="1209687543", Country="Botswana", Industry="Accounting", Birthdate= DateTime.Parse("1994/3/2")},
                    new UserInfo(){Id=6, Name="Maria", Email="Maria@gmail.com", Phone="1543578643", Country="Bahrain", Industry="Accounting", Birthdate= DateTime.Parse("1998/4/2")},
                    new UserInfo(){Id=7, Name="Michael", Email="Michael@gmail.com", Phone="1215457467", Country="Argentina", Industry="Finance", Birthdate= DateTime.Parse("2003/2/2")},
                    new UserInfo(){Id=8, Name="Michelle", Email="Michelle@gmail.com", Phone="1534357546", Country="Bulgaria", Industry="Finance", Birthdate= DateTime.Parse("2001/1/1")}
                };
            }
        }

        public static string[] Countries
        {
            get
            {
                return Models.Countries.GetCountries().ToArray();
            }
        }

        public static object[] Industries
        {
            get
            {
                return new[] { null, "Computers", "Telecom", "Internet", "Electronics", "Instrument", "Accounting", "Finance", "Banking", "Insurance", "Others" }.Select(s => new { name = s ?? "(none)", value = s }).ToArray();
            }
        }

        public static string[] SkillsList
        {
            get
            {
                return new[] { "c#", "c++", "vb", "java", "javascript", "sql", "oracle" };
            }
        }

        public static string[] HobbyList
        {
            get
            {
                return new[] { "Reading", "Games", "Skating", "Fishing", "Running", "Photography", "Climbing", "Chess", "Volleyball", "Badminton", "Others" };
            }
        }
    }
}
