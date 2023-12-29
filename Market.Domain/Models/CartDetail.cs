using Market.Domain.Models.Dto.Services.Product;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.Models
{
    public class CartDetail : BaseModel
    {
        [Key, Column(Order = 0)]
        public long CartDetailId { get; set; }
        [Key, Column(Order = 1)]
        public long CartHeaderId { get; set; }
        [ForeignKey("CartHeaderId")]
        public CartHeader CartHeader { get; set; }
        public long ProductId { get; set; }
        [NotMapped]
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
