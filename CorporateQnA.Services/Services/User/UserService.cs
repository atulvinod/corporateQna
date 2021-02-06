using AutoMapper;
using CorporateQnA.Models;
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
        private readonly IMapper mapper;

        public UserService(IConfiguration configuration, IMapper mapper)
        {
            this.database = new PetaPoco.Database(configuration.GetConnectionString("DB"), "System.Data.SqlClient");
            this.mapper = mapper;
        }


        public int Create(Users user)
        {
            var check = this.database.Fetch<Users>("WHERE Email = @0", user.Email).FirstOrDefault();
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
            var user = this.database.FirstOrDefault<Users>("WHERE Id = @0", userid);
            return user;
        }

        public IEnumerable<UserDetails> GetUsersDetails()
        {
            var users = this.database.Fetch<Models.UserDetails>().Select(s => this.mapper.Map<UserDetails>(s));
            return users;
        }

        public UserDetails GetSingleUserDetails(int userId)
        {
            var users = this.database.Fetch<Models.UserDetails>("WHERE Id = @0", userId).Select(s => this.mapper.Map<UserDetails>(s)).FirstOrDefault();
            return users;
        }
    }
}
