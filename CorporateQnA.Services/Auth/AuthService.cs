using CorporateQnA.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services.Auth
{
    public class AuthService : IAuthService
    {
        public Task<bool> Login(LoginModel login)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Register(RegisterModel register)
        {
            throw new NotImplementedException();
        }
    }
}
