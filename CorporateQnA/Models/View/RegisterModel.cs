using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Models.View
{
    public class RegisterModel
    {
        public string ReturnUrl { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Position{ get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Password { get; set; }
        
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
