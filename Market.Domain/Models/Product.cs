using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Models
{
    public sealed class Product : BaseModel
    {
        [Required]
        public double Price { get; set; } = 0;
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}