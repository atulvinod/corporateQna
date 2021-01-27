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
            return (int)this.database.Insert(user);
        }


    }
}
