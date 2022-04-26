using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace MvcExplorer.Models
{
    public class UserInfo
    {
        public UserInfo() {
            Birthdate = DateTime.Now;
        }
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Validation), ErrorMessageResourceName = "Required_Name_ErrorMessage")]
        [RegularExpression(pattern: "^[a-zA-Z0-9]{4,10}$", ErrorMessageResourceType = typeof(Resources.Validation), ErrorMessageResourceName = "Register_Name_ErrorMessage")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Validation), ErrorMessageResourceName = "Required_Email_ErrorMessage")]
        [RegularExpression(pattern: @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessageResourceType = typeof(Resources.Validation), ErrorMessageResourceName = "Register_Email_ErrorMessage")]
        public string Email { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Validation), ErrorMessageResourceName = "Required_Phone_ErrorMessage")]
        [StringLength(11, MinimumLength = 8)]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Validation), ErrorMessageResourceName = "Required_Country_ErrorMessage")]
        public string Country { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Validation), ErrorMessageResourceName = "Required_Industry_ErrorMessage")]
        public string Industry { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Validation), ErrorMessageResourceName = "Required_Birthdate_ErrorMessage")]
        public DateTime Birthdate { get; set; }

        [Required]
        public Color? FavoriteColor { get; set; }

        [Required]
        public string[] Skills { get; set; }

        [Required]
        public string[] Hobbies { get; set; }
    }
}