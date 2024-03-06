using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YoKartApi.Data;
using YoKartApi.IServices;
using YoKartApi.Models;
using static YoKartApi.Models.Order;

namespace YoKartApi.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly YoKartApiDbContext _context;

        public OrderServices(YoKartApiDbContext context)
        {
            _context = context;
        }

        public async Task<Order> AddProductToOrder(OrderDetails orderDetails)
        {
            var existingOrder = _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Products).
               SingleOrDefault(o => o.UserId == orderDetails.UserId);

            if (existingOrder != null)
            {
                var existingProduct = _context.Products.Find(orderDetails.ProductId);
                if (existingProduct != null)
                {
                    var newOrderItem = new OrderItem
                    {
                        ProductId = orderDetails.ProductId,
                        Quantity = orderDetails.Quantity,
                        Products = existingProduct
                    };
                    existingOrder.OrderItems.Add(newOrderItem);
                    _context.SaveChanges();
                }
            }
            return existingOrder;
        }

        public async Task<OrderItem> RemoveProductToOrder(OrderDetails orderDetails)
        {
            var existingOrder = _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Products).
              SingleOrDefault(o => o.UserId == orderDetails.UserId);

            if (existingOrder != null)
            {
                var existingProduct = existingOrder.OrderItems.FirstOrDefault(m => m.ProductId == orderDetails.ProductId);

                existingOrder.OrderItems.Remove(existingProduct);
                _context.SaveChanges();

                return existingProduct;
            }
            return null;
        }

        public async Task<OrderItem> UpdateOrder(OrderDetails orderDetails)
        {
            var existingOrder = _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Products).
              SingleOrDefault(o => o.UserId == orderDetails.UserId);

            if (existingOrder != null)
            {
                var existingProduct = existingOrder.OrderItems?.FirstOrDefault(m => m.ProductId == orderDetails.ProductId);
                if (existingProduct != null)
                {
                    existingProduct.Quantity = orderDetails.Quantity;
                }
                _context.SaveChanges();
                return existingProduct;
            }
            return null;
        }
    }
}
