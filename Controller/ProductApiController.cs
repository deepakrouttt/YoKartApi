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

        [HttpGet("GetProducts")]
        public IActionResult GetProducts()
        {
            var products = _context.Products.ToList();

            return Ok(products);
        }


        [HttpGet("GetProductsRange")]
        public IActionResult GetProducts(long? low, long? high)
        {
            if (high == null) { high = long.MaxValue; };
            var products = _context.Products.Where(m => Convert.ToInt64(m.ProductPrice) > (low ?? 0) && 
            Convert.ToInt64(m.ProductPrice) < high);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProducts(int id)
        {
            var products = _context.Products.Find(id);

            return Ok(products);
        }

        [HttpGet("GetProductsForSubcategory")]
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

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> EditProduct(Product _product)
        {
            var product = _context.Products.FirstOrDefault(s => s.ProductId == _product.ProductId);

            if (product == null)
            {
                return NotFound("Category not found");
            }

            product.CategoryId = _product.CategoryId;
            product.SubCategoryId = _product.SubCategoryId;
            product.ProductName = _product.ProductName;
            product.ProductImage = _product.ProductImage;
            product.ProductPrice = _product.ProductPrice;
            product.ProductDescription = _product.ProductDescription;

            _context.SaveChanges();
            return Ok(product);
        }
        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(m => m.ProductId == id);
            _context.Products.Remove(product);
            _context.SaveChanges();

            return Ok(product);
        }
    }
}

