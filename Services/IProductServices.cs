using YoKartApi.Models;

namespace YoKartApi.Services
{
    public interface IProductServices
    {
        ProductPagingData Productpaging(List<Product> Products, Paging obj);
    }
}