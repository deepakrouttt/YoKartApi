using System.Collections.Generic;
using System.Linq;
using YoKartApi.Data;
using YoKartApi.Models;

namespace YoKartApi.Services
{
    public class ProductServices : IProductServices
    {
        public readonly YoKartApiDbContext _context;

        public ProductServices(YoKartApiDbContext context)
        {
            _context = context;
        }

        public ProductPagingData Productpaging(List<Product> Products, Paging obj)
        {

            var Rangeproducts = Products.Where(m => Convert.ToDecimal(m.ProductPrice) > obj.LowPrice);

            if (obj.HighPrice != 0)
            {
                Rangeproducts = Rangeproducts.Where(m => Convert.ToDecimal(m.ProductPrice) < obj.HighPrice);
            }

            var ProductList = Rangeproducts.ToList();
            myVar.pageCount = (int)Math.Ceiling(ProductList.Count / (double)myVar.pageSize);
            myVar.currentPage = obj.page ?? 1;
            var tempProduct = ProductList.Skip((myVar.currentPage - 1) * myVar.pageSize).Take(myVar.pageSize).ToList();
            myVar.totalProduct = ProductList.Count;

            //Sorting of Products
            if (obj.Sort != null)
            {
                switch (obj.Sort)
                {
                    case "CategoryName":
                        Rangeproducts = tempProduct.OrderBy(m => _context.Categories.Find(m.CategoryId).CategoryName);
                        break;
                    case "SubCategoryName":
                        Rangeproducts = tempProduct.OrderBy(m => _context.SubCategories.Find(m.SubCategoryId).SubCategoryName);
                        break;
                    case "ProductName":
                        Rangeproducts = tempProduct.OrderBy(m => m.ProductName.ToLower());
                        break;
                    case "ProductImage":
                        Rangeproducts = tempProduct.OrderBy(m => m.ProductImage);
                        break;
                    case "ProductPrice":
                        Rangeproducts = tempProduct.OrderBy(m => Convert.ToDecimal(m.ProductPrice));
                        break;
                    case "ProductDescription":
                        Rangeproducts = tempProduct.OrderBy(m => m.ProductDescription);
                        break;
                    default:
                        break;
                }

                return new()
                {
                    Product = Rangeproducts.ToList(),
                    pageSize = myVar.pageSize,
                    pageCount = myVar.pageCount,
                    totalProduct = myVar.totalProduct,
                    currentPage = myVar.currentPage
                };
            }

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
