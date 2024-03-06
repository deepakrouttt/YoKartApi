using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using YoKartApi.Data;
using YoKartApi.IServices;
using YoKartApi.Models;
using static YoKartApi.Models.Order;

namespace YoKartApi.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderApiController : ControllerBase
    {
        private readonly YoKartApiDbContext _context;
        private readonly ILogger<CategoryApiController> _logger;
        private readonly IOrderServices _service;

        public OrderApiController(YoKartApiDbContext context, ILogger<CategoryApiController> logger, IOrderServices service)
        {
            _context = context;
            _logger = logger;
            _service = service;
        }

        [HttpGet("Orders")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Products).Where(o => o.OrderItems.
            Any(oi => oi.Products.ProductId == oi.ProductId)).ToList();

            return Ok(orders);
        }

        [HttpGet("GetOrderbyUser")]
        public async Task<IActionResult> GetOrderbyUser(int id)
        {
            var orders = _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Products).
                SingleOrDefault(o => o.UserId == id);

            return Ok(orders);
        }

        [HttpPost("addOrder")]
        public async Task<IActionResult> AddProductToOrder(OrderDetails orderDetails)
        {
            var addProductOrder = await _service.AddProductToOrder(orderDetails);
            if (addProductOrder != null)
            {
                return Ok(addProductOrder);
            }
            else
            {
                return Ok("Order list not found");
            }
        }

        [HttpPost("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(OrderDetails orderDetails)
        {
            var addProductOrder = await _service.UpdateOrder(orderDetails);
            if (addProductOrder != null)
            {
                return Ok(addProductOrder);
            }
            else
            {
                return Ok("Order list not found");
            }
        }


        [HttpDelete("RemoveOrder")]
        public async Task<IActionResult> RemoveProductToOrder([FromQuery] OrderDetails orderDetails)
        {
            var RemoveOrderProduct = await _service.RemoveProductToOrder(orderDetails);
            if (RemoveOrderProduct != null)
            {
                return Ok(RemoveOrderProduct);
            }
            return NotFound();
        }
    }
}
