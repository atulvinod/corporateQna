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
    public class CategoryService : BaseService, ICategoryService
    {

        /// <summary>
        /// Initializes an instance of category service
        /// </summary>
        /// <param name="configuration">The configuration</param>
        public CategoryService(IConfiguration configuration) : base(configuration) { }

        public int CreateCategory(Category category)
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

        /// <summary>
        /// Gets all the categories
        /// </summary>
        /// <returns>The category list</returns>
        public IEnumerable<Category> GetAllCategories()
        {
            return this.database.Fetch<Models.Category>().MapCollectionTo<Category>();
        }

        /// <summary>
        /// Gets all the category details
        /// </summary>
        /// <returns>The category details list</returns>
        public IEnumerable<CategoryDetails> GetAllCategoryDetails()
        {
            return this.database.Fetch<Models.CategoryDetails>().MapCollectionTo<CategoryDetails>();
        }
    }
}
