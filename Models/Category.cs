using System.ComponentModel.DataAnnotations;

namespace YoKartApi.Models
{
    public class Category
    {
        [Required]
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public String CategoryName { get; set; }
        [Required]
        public List<SubCategory> SubCategories { get; set; }

    }
}
