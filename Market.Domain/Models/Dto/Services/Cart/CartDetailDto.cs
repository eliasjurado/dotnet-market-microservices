using Market.Domain.Models.Dto.Services.Product;

namespace Market.Domain.Models
{
    public class CartDetailDto
    {
        public long CartDetailId { get; set; }
        public long CartHeaderId { get; set; }
        public CartHeader CartHeader { get; set; }
        public long ProductId { get; set; }
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
