using YoKartApi.Models;

namespace YoKartApi.Services
{
    public interface IProductServices
    {
        ProductPagingData Productpaging(List<Product> Products, Paging obj);

        List<Product> ProductSearch(List<Product> Products, string search);

        List<Product> RandomProduct(List<Product> products);
    }
}