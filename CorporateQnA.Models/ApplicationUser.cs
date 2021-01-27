using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string Position { get; set; }

        public string Location { get; set; }

        public string Department { get; set; }
    }
}
