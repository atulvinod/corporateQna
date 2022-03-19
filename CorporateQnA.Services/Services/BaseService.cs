using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services.Services
{
    public class BaseService
    {

        protected readonly PetaPoco.Database database;

        /// <summary>
        /// Creates an instance of base service
        /// </summary>
        /// <param name="configuration">The configuration</param>
        public BaseService(IConfiguration configuration)
        {
            this.database = new PetaPoco.Database(configuration.GetConnectionString("DB"), "System.Data.SqlClient");
        }
    }
}
