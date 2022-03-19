using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateQnA.Models;

namespace CorporateQnA.Services
{
    public interface ICategoryService
    {
        /// <summary>
        /// Creates a new category
        /// </summary>
        /// <param name="category">The new category</param>
        /// <returns>The created category identifier</returns>
        public int CreateCategory(Category category);

        /// <summary>
        /// Gets all the available categories
        /// </summary>
        /// <returns>The category list</returns>
        public IEnumerable<Category> GetAllCategories();

        /// <summary>
        /// Gets all the category details
        /// </summary>
        /// <returns>The category details list</returns>
        public IEnumerable<CategoryDetails> GetAllCategoryDetails();
    }
}
