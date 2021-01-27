using System;
using System.Threading.Tasks;

namespace CorporateQnA.Services.Auth
{
    public class AuthService : IAuthService
    {

        public AuthService()
        {
                
        }

        public Task<bool> Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Register(string username, string email, string password, string location, string designation)
        {
            throw new NotImplementedException();
        }
    }
}
