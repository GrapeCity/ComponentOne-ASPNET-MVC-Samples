using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace MultiRowExplorer.Models
{
    public class UserInfo
    {
        public UserInfo()
        {
            Birthdate = DateTime.Now;
        }
        public int Id { get; set; }

        [Required]
        [RegularExpression(pattern: "^[a-zA-Z0-9]{4,10}$", ErrorMessage = "The username must be alphanumeric and contains 4 to 10 characters.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(pattern: @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "The Email field is not a valid e-mail address.")]
        public string Email { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 8)]
        public string Phone { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Industry { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public Color? FavoriteColor { get; set; }

        [Required]
        public string[] Skills { get; set; }
    }
}