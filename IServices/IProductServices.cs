using YoKartApi.Models;

namespace YoKartApi.IServices
{
    public interface IProductServices
    {
        ProductPagingData Productpaging(List<Product> Products, filtering obj);

        List<Product> ProductSearch(List<Product> Products, string search);

        List<Product> RandomProduct(List<Product> products);
    }
}