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
        public UserService(IConfiguration configuration)
        {
            this.database = new PetaPoco.Database(configuration.GetConnectionString("DB"), "System.Data.SqlClient");
        }


        public int Create(AppUser user)
        {
            var check = this.database.Query<AppUser>("Select * From Users WHERE Email = @0", user.Email);
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


    }
}
