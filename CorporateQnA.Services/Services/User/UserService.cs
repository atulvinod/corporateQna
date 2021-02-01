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


        public int Create(AppUser user)
        {
            var check = this.database.Query<AppUser>("Select * From Users WHERE Email = @0", user.Email).FirstOrDefault();
            if (check == null)
            {
                return (int)this.database.Insert(user);
            }
            else
            {
                return 0;
            }
        }

        public AppUser GetUser(int userid)
        {
            var user = this.database.Query<AppUser>("SELECT * FROM Users WHERE Id = @0", userid).FirstOrDefault();
            return user;
        }

        public IEnumerable<UserDetails> GetUsersDetails()
        {
            var users = this.database.Query<Models.UserDetails>("SELECT * FROM UserDetails").Select(s => this.mapper.Map<UserDetails>(s));
            return users;
        }

        public UserDetails GetSingleUserDetails(int userId)
        {
            var users = this.database.Query<Models.UserDetails>("SELECT * FROM UserDetails WHERE Id = @0", userId).Select(s => this.mapper.Map<UserDetails>(s)).FirstOrDefault();
            return users;
        }
    }
}
