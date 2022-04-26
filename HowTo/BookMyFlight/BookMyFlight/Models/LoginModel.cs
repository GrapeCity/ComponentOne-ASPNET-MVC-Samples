using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookMyFlight.Models
{
    public class LoginModel
    {
        //For Login view
        public string UserName { get; set; }
        public string Password { get; set; }

        public LoginModel()
        {
            UserName = "andrew.fuller@gmail.com";
            Password = "123456789";
        }
    }
}