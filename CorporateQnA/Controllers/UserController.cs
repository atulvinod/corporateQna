using CorporateQnA.Models;
using CorporateQnA.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Controllers
{
    [Route("userdata")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<UserDetails> GetAllUsersDetails()
        {
            return this.userService.GetUsersDetails();
        }

        [HttpGet]
        [Route("user")]
        public UserDetails GetSingleUserDetails(int userId)
        {
            return this.userService.GetSingleUserDetails(userId);
        }
    }
}
