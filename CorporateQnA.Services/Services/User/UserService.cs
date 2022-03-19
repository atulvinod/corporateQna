using AutoMapper;
using CorporateQnA.Models;
using CorporateQnA.Services.ModelMaps.Extensions;
using CorporateQnA.Services.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services
{
    public class UserService : BaseService, IUserService
    {
        /// <summary>
        /// Initializes an instance of UserService
        /// </summary>
        /// <param name="configuration">The configuration</param>
        public UserService(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user">The user model</param>
        /// <returns>The created user identifier</returns>
        public int CreateUser(Users user)
        {
            var check = this.database.FirstOrDefault<Users>("WHERE Email = @0", user.Email);
            if (check == null)
            {
                var userId = (int)this.database.Insert(user);
                return userId;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the user 
        /// </summary>
        /// <param name="userid">The user id</param>
        /// <returns>The user</returns>
        public Users GetUserById(int userid)
        {
            return this.database.FirstOrDefault<Users>("WHERE Id = @0", userid);
        }

        /// <summary>
        /// Gets the all users details
        /// </summary>
        /// <returns>The user details list</returns>
        public IEnumerable<UserDetails> GetAllUsersDetails()
        {
            return this.database.Fetch<Models.UserDetails>().MapCollectionTo<UserDetails>();
        }

        /// <summary>
        /// Gets the user details by id
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <returns>The user</returns>
        public UserDetails GetUserDetailsById(int userId)
        {
            return this.database.FirstOrDefault<Models.UserDetails>("WHERE Id = @0", userId).MapTo<UserDetails>();
        }
    }
}
