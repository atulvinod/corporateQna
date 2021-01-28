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
    public class CategoryService : ICategoryService
    {
        private readonly IMapper mapper;
        private readonly PetaPoco.Database database;
        public CategoryService(IConfiguration configuration, AutoMapper.IMapper mapper)
        {
            this.mapper = mapper;
            this.database = new PetaPoco.Database(configuration.GetConnectionString("DB"), "System.Data.SqlClient");
        }

        public int Create(Category category)
        {
            try
            {
                var data = this.mapper.Map<CorporateQnA.Services.Models.Category>(category);
                data.CreatedOn = DateTime.Now;
                return (int)this.database.Insert(data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            try
            {
                var category = this.database.Query<CorporateQnA.Services.Models.Category>("SELECT * FROM Category").Select(z => this.mapper.Map<CorporateQnA.Models.Category>(z));
                return category;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
