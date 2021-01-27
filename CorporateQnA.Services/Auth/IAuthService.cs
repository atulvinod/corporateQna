using System.Threading.Tasks;

namespace CorporateQnA.Services.Auth
{
    interface IAuthService
    {
        public Task<bool> Login(string email, string password);

        public Task<bool> Register(string username, string email, string password, string location, string designation);
    }
}
