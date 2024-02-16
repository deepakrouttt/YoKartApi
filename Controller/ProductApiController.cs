using Microsoft.AspNetCore.Mvc;
using YoKartApi.Data;
using YoKartApi.Models;

namespace YoKartApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly YoKartApiDbContext _context;

        public ProductApiController(YoKartApiDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetProduct")]
        public IActionResult GetProductsForSubcategory(int subcategoryId)
        {
            var products = _context.Products.Where(p => p.SubCategoryId == subcategoryId).ToList();

            return Ok(products);
        }
        [HttpPost("AddProduct")]
        public IActionResult AddProduct(Product _product)
        {
            _context.Products.Add(_product);
            _context.SaveChanges();

            return Ok(_product);
        }
    }
}

