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
        public int Create(Category category);

        public IEnumerable<Category> GetCategories();
    }
}
