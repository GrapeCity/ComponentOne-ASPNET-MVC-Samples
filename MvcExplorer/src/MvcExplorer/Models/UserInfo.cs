﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MvcExplorer.Models
{
    public class UserInfo
    {
        public UserInfo()
        {
            Birthdate = DateTime.Now;
        }
        public int Id { get; set; }

        [Required]
        [RegularExpression(pattern: "^[a-zA-Z0-9]{4,10}$", ErrorMessageResourceType =typeof(Localization.ValidationRes), ErrorMessageResourceName = "Register_Name_ErrorMessage")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(16)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(11)]
        public string Phone { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Industry { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public string FavoriteColor { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(3)]
        public string[] Skills { get; set; }

        [Required]
        public string[] Hobbies { get; set; }
    }
}