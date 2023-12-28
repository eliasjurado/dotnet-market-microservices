using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Models
{
    public class Product : BaseModel
    {
        [Required]
        public decimal Price { get; set; } = 0;
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
