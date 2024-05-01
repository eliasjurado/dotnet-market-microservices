namespace Market.Domain.Models.Dto.Services.Cart
{
    public class CartDetailDto
    {
        public Guid CreatedBy { get; set; }
        public long CartDetailId { get; set; }
        public long CartHeaderId { get; set; }
        //public CartHeader CartHeader { get; set; }
        public long ProductId { get; set; }
        public double ProductPrice { get; set; }
        //public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
