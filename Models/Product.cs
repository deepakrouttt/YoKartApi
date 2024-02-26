using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YoKartApi.Models
{
    public class Product
    {
        [Required][Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int SubCategoryId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductImage { get; set; }
        [Required]
        public string ProductPrice { get; set; }
        [Required]
        public string ProductDescription { get; set; }

    }

    public class Paging
    {
        public int? page { get; set; }
        public long LowPrice { get; set; }
        public long HighPrice { get; set; }
    }
    public class ProductPagingData
    {
        public  List<Product> Product { get; set; }
        public  int pageSize = 2;
        public  int pageCount { get; set; }
        public  int Total { get; set; }
        public  int currentPage { get; set; }
    }
}
