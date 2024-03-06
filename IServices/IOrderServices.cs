using YoKartApi.Models;
using static YoKartApi.Models.Order;

namespace YoKartApi.IServices
{
    public interface IOrderServices
    {
        Task<Order> AddProductToOrder(OrderDetails orderDetails);
        Task<OrderItem> UpdateOrder(OrderDetails orderDetails);
        Task<OrderItem> RemoveProductToOrder(OrderDetails orderDetails);
    }
}