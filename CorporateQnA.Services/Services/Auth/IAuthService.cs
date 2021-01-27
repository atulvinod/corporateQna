using System.Threading.Tasks;

namespace CorporateQnA.Services.Auth
{
    public interface IAuthService
    {
        public Task<bool> Login(string email, string password);

        public Task<bool> Register(string name, string username, string email, string password, string location, string position, string department);

        public Task<string> Logout(string logoutId);
    }
}
