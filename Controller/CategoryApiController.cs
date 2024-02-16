using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using YoKartApi.Data;
using YoKartApi.Models;

namespace YoKartApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        private readonly YoKartApiDbContext _context;

        public CategoryApiController(YoKartApiDbContext context)
        {
            _context = context;
        }

        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            var categories = _context.Categories.Include(c => c.SubCategories).ToList();
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
