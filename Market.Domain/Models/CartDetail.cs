using Market.Domain.Models.Dto.Services.Product;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Domain.Models
{
    public class CartDetail : BaseModel
    {
        [Key, Column(Order = 0)]
        public new Guid CreatedBy { get; set; } = Guid.NewGuid();
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CartHeaderId { get; set; }
        [Key, Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CartDetailId { get; set; }
        [ForeignKey("CreatedBy,CartHeaderId")]
        public CartHeader CartHeader { get; set; }
        public long ProductId { get; set; }
        [NotMapped]
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
