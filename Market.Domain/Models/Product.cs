using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.Models
{
    public sealed class Product : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductId { get; set; }
        [Required]
        public double ProductPrice { get; set; } = 0;
        [Required]
        public string ProductCategoryName { get; set; }
        [Required]
        public string ProductImageUrl { get; set; }
    }
}