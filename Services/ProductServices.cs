using System.Collections.Generic;
using System.Linq;
using YoKartApi.Data;
using YoKartApi.Models;

namespace YoKartApi.Services
{
    public class ProductServices : IProductServices
    {
        public readonly YoKartApiDbContext _context;
        private static bool isAscending = true;

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

            if (obj.Sort != null)
            {

                switch (obj.Sort)
                {
                    case "CategoryName":
                        Rangeproducts = isAscending
                              ? tempProduct.OrderBy(m => _context.Categories.Find(m.CategoryId).CategoryName)
                              : tempProduct.OrderByDescending(m => _context.Categories.Find(m.CategoryId).CategoryName);
                        break;
                    case "SubCategoryName":
                        Rangeproducts = isAscending
                        ? tempProduct.OrderBy(m => _context.SubCategories.Find(m.SubCategoryId).SubCategoryName)
                            : tempProduct.OrderByDescending(m => _context.SubCategories.Find(m.SubCategoryId).SubCategoryName);
                        break;
                    case "ProductName":
                        Rangeproducts = isAscending
                        ? tempProduct.OrderBy(m => m.ProductName.ToLower())
                        : tempProduct.OrderByDescending(m => m.ProductName.ToLower());
                        break;
                    case "ProductImage":
                        Rangeproducts = isAscending
                        ? tempProduct.OrderBy(m => m.ProductImage)
                        : tempProduct.OrderByDescending(m => m.ProductImage);
                        break;
                    case "ProductPrice":
                        Rangeproducts = isAscending
                        ? tempProduct.OrderBy(m => Convert.ToDecimal(m.ProductPrice))
                        : tempProduct.OrderByDescending(m => Convert.ToDecimal(m.ProductPrice));
                        break;
                    case "ProductDescription":
                        Rangeproducts = isAscending
                        ? tempProduct.OrderBy(m => m.ProductDescription)
                        : tempProduct.OrderByDescending(m => m.ProductDescription);
                        break;
                    default:
                        break;
                }
                isAscending = !isAscending;
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


        public List<Product> ProductSearch(List<Product> Products, string search)
        {
            var ProductList = new List<Product>();
            if (!string.IsNullOrEmpty(search))
            {
                ProductList = Products.Where(p => (p.ProductName.Contains(search))
                                                  || (p.ProductImage.Contains(search))
                                                  || (_context.Categories.FirstOrDefault(m => m.CategoryId == p.CategoryId).CategoryName.Contains(search))
                                                  || (_context.SubCategories.FirstOrDefault(m => m.SubCategoryId == p.SubCategoryId).SubCategoryName.Contains(search))
                                                  ).ToList();
            }
            else
            {
                ProductList = Products;
            }
            return ProductList;
        }


        //Random Product Listing
        public List<Product> RandomProduct(List<Product> products)
        {
            Random random = new Random();
            int n = products.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                var value = products[k];
                products[k] = products[n];
                products[n] = value;
            }
            return products;
        }

    }
}
