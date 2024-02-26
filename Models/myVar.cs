namespace YoKartApi.Models
{
    public static class myVar
    {
        public static int pageSize = 2;
        public static int pageCount { get; set; }
        public static int Total { get; set; }
        public static int currentPage { get; set; }

        public static ProductPagingData PagingProduct(IEnumerable<Product> products,Paging obj)
        {
            var productList = products.ToList();
            pageCount = (int)Math.Ceiling(productList.Count / (double)pageSize);

            currentPage = obj.page ?? 1;
            var tempProduct = products.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new()
            {
                Product = tempProduct,
                pageSize = pageSize,
                pageCount = pageCount,
                Total = Total,
                currentPage = currentPage
            };
        }
    }
}
