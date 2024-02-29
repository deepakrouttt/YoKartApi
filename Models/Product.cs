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
        public Decimal LowPrice { get; set; }
        public Decimal HighPrice { get; set; }
        public string? Sort { get; set; }
    }
    public class ProductPagingData
    {
        public  List<Product> Product { get; set; }
        public  int pageSize;
        public  int pageCount { get; set; }
        public  int totalProduct { get; set; }
        public  int currentPage { get; set; }
    }
}
