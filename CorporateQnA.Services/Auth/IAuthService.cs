using CorporateQnA.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services.Auth
{
    interface IAuthService
    {
        public Task<bool> Login(LoginModel login);

        public Task<bool> Register(RegisterModel register);
    }
}
