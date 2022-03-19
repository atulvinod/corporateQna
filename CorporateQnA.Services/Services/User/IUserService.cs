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
        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user">The user model</param>
        /// <returns>The created user id</returns>
        public int CreateUser(Users user);

        /// <summary>
        /// Gets the user by Id
        /// </summary>
        /// <param name="userid">The user id</param>
        /// <returns>The user</returns>
        public Users GetUserById(int userid);

        /// <summary>
        /// Gets the all user's details
        /// </summary>
        /// <returns>The user list</returns>
        public IEnumerable<UserDetails> GetAllUsersDetails();

        /// <summary>
        /// Gets the user details by id
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <returns>The user details</returns>
        public UserDetails GetUserDetailsById(int userId);
    }
}
