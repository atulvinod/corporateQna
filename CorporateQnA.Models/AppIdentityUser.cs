using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Models
{
    public class AppIdentityUser : IdentityUser
    {
        public int UserId { get; set; }
    }
}
