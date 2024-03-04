using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YoKartApi.Data;
using YoKartApi.Models;

namespace YoKartApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderApiController : ControllerBase
    {
        private readonly YoKartApiDbContext _context;
        private readonly ILogger<CategoryApiController> _logger;

        public OrderApiController(YoKartApiDbContext context, ILogger<CategoryApiController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("Orders")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).Where(o => o.OrderItems.
            Any(oi => oi.Product.ProductId == oi.ProductId)).ToList();

            return Ok(orders);
        }

        [HttpPost("addOrder")]
        public async Task<IActionResult> addOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return Ok(order);
        }

        [HttpGet("GetOrderbyUser")]
        public async Task<IActionResult> GetOrderbyUser(int id)
        {
            var orders = _context.Orders.Where(m => m.UserId == id).Include(o => o.OrderItems).ThenInclude(oi => oi.Product).FirstOrDefault(o => o.OrderItems.
            Any(oi => oi.Product.ProductId == oi.ProductId));
            return Ok(orders);
        }
    }
}
