using System;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesExplorer.Models
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
        [EmailAddress]
        public string Email { get; set; }

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
    }
}