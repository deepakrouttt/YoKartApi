using YoKartApi.Data;
using YoKartApi.Models;

namespace YoKartApi.Services
{
    public class ProductServices : IProductServices
    {
        public ProductPagingData Productpaging(List<Product> Products, Paging obj)
        {
            var Rangeproducts = Products.Where(m => Convert.ToInt64(m.ProductPrice) > obj.LowPrice &&
            Convert.ToInt64(m.ProductPrice) < obj.HighPrice).OrderBy(m => Convert.ToInt64(m.ProductPrice));

            var ProductList = Rangeproducts.ToList();
            myVar.pageCount = (int)Math.Ceiling(ProductList.Count / (double)myVar.pageSize);
            myVar.currentPage = obj.page ?? 1;
            var tempProduct = ProductList.Skip((myVar.currentPage - 1) * myVar.pageSize).Take(myVar.pageSize).ToList();
            myVar.totalProduct = ProductList.Count;

            return new()
            {
                Product = tempProduct,
                pageSize = myVar.pageSize,
                pageCount = myVar.pageCount,
                totalProduct = myVar.totalProduct,
                currentPage = myVar.currentPage
            };
        }



    }
}
