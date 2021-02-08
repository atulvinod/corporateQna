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
    public class CategoryService : ICategoryService
    {
        private readonly PetaPoco.Database database;
        public CategoryService(IConfiguration configuration)
        {
            this.database = new PetaPoco.Database(configuration.GetConnectionString("DB"), "System.Data.SqlClient");
        }

        public int Create(Category category)
        {
            try
            {
                var dataModel = category.MapTo<Models.Category>();
                dataModel.CreatedOn = DateTime.Now;
                return (int)this.database.Insert(dataModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            return this.database.Fetch<Models.Category>().MapCollectionTo<Category>();
        }

        public IEnumerable<CategoryDetails> GetCategoryDetails()
        {
            return this.database.Fetch<Models.CategoryDetails>().MapCollectionTo<CategoryDetails>();
        }
    }
}
