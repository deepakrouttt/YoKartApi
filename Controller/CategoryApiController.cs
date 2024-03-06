using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YoKartApi.Data;
using YoKartApi.Models;

namespace YoKartApi.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        private readonly YoKartApiDbContext _context;
        private readonly ILogger<CategoryApiController> _logger;

        public CategoryApiController(YoKartApiDbContext context, ILogger<CategoryApiController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            var categories = _context.Categories.Include(c => c.SubCategories).ToList();
            _logger.LogInformation("Processing GET request");
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var categories = _context.Categories.Include(c => c.SubCategories).ToList();
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound("Category not found");
            }

            return Ok(category);
        }


        [HttpGet("subcategories")]
        public IActionResult GetSubCategories()
        {
            var subcategories = _context.SubCategories.ToList();
            return Ok(subcategories);
        }
            
        [HttpGet("GetSubCategory/{id}")]
        public IActionResult GetSubCategory(int id)
        {
            var subcategories = _context.SubCategories.FirstOrDefault(c => c.SubCategoryId == id);

            return Ok(subcategories);
        }

        [HttpPost]
        [Route("addCategories")]
        public IActionResult AddCategories(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok(category);
        }

        [HttpPut]
        [Route("editCategories")]
        public IActionResult EditCategory(Category category)
        {
            var existingCategory = _context.Categories.Find(category.CategoryId);

            if (existingCategory == null)
            {
                return NotFound("Category not found");
            }

            existingCategory.CategoryName = category.CategoryName;

            _context.SaveChanges();

            return Ok(existingCategory);
        }

        [HttpPut]
        [Route("existCategories")]
        public IActionResult ExistCategory(Category category)
        {
            var existingCategory = _context.Categories.Find(category.CategoryId);

            if (existingCategory == null)
            {
                return NotFound("Category not found");
            }

            existingCategory.CategoryName = category.CategoryName;
            existingCategory.SubCategories = category.SubCategories;

            _context.SaveChanges();

            return Ok(existingCategory);
        }

        [HttpPut]
        [Route("existSubCategories")]
        public IActionResult ExistSubCategory(SubCategory category)
        {
            var existingCategory = _context.SubCategories.FirstOrDefault(c => c.SubCategoryId == category.SubCategoryId);

            if (existingCategory == null)
            {
                return NotFound("Category not found");
            }

            existingCategory.SubCategoryName = category.SubCategoryName;

            _context.SaveChanges();

            return Ok(existingCategory);
        }
        [HttpDelete]
        [Route("removeCategoryies")]
        public IActionResult RemoveCategories(int id)
        {
            var category = _context.Categories.Find(id);

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return Ok(category);
        }

        [HttpDelete]
        [Route("removeSubCategoryies")]
        public IActionResult RemoveSubCategories(int id)
        {
            var Subcategory = _context.SubCategories.Find(id);

            _context.SubCategories.Remove(Subcategory);
            _context.SaveChanges();

            return Ok(Subcategory);
        }
    }
}
