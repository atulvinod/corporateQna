using AutoMapper;
using CorporateQnA.Models;
using CorporateQnA.Services.ModelMaps.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services
{
    public class UserService : IUserService
    {
        private readonly PetaPoco.Database database;

        public UserService(IConfiguration configuration)
        {
            this.database = new PetaPoco.Database(configuration.GetConnectionString("DB"), "System.Data.SqlClient");
        }


        public int Create(Users user)
        {
            var check = this.database.FirstOrDefault<Users>("WHERE Email = @0", user.Email);
            if (check == null)
            {
                return (int)this.database.Insert(user);
            }
            else
            {
                return 0;
            }
        }

        public Users GetUser(int userid)
        {
            return this.database.FirstOrDefault<Users>("WHERE Id = @0", userid);
        }

        public IEnumerable<UserDetails> GetUsersDetails()
        {
            return this.database.Fetch<Models.UserDetails>().MapCollectionTo<UserDetails>();
        }

        public UserDetails GetSingleUserDetails(int userId)
        {
            return this.database.FirstOrDefault<Models.UserDetails>("WHERE Id = @0", userId).MapTo<UserDetails>();
        }
    }
}
