using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Models
{
    public sealed class Product : BaseModel
    {
        [Key]
        public long ProductId { get; set; }
        [Required]
        public double ProductPrice { get; set; } = 0;
        [Required]
        public string ProductCategoryName { get; set; }
        [Required]
        public string ProductImageUrl { get; set; }
    }
}