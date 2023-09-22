using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyOpdracht.Models
{
    public class RegisterViewModel
    {
        [EmailAddress]
        [Required]
        [MaxLength(300)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }
    }

    public class LoginViewModel
    {
        [EmailAddress]
        [Required]
        [MaxLength(300)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

}
