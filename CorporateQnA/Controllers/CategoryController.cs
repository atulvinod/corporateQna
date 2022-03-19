using CorporateQnA.Models;
using CorporateQnA.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Controllers
{
    [Route("category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        

        [HttpPost]
        [Route("create")]
        public IActionResult CreateCategory(Category category)
        {
            try
            {
                return Ok(this.categoryService.CreateCategory(category));
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet]
        public  IEnumerable<Category> GetCategory()
        {
            return this.categoryService.GetAllCategories();
        }

        [HttpGet]
        [Route("details")]
        public IEnumerable<CategoryDetails> GetCategoryDetails()
        {
            return this.categoryService.GetAllCategoryDetails();
        }
    }
}
