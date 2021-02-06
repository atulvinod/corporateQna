using CorporateQnA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services
{
    public interface IUserService
    {
        public int Create(Users user);

        public Users GetUser(int userid);

        public IEnumerable<UserDetails> GetUsersDetails();

        public UserDetails GetSingleUserDetails(int userId);
    }
}
