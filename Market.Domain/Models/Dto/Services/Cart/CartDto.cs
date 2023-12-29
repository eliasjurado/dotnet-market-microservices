namespace Market.Domain.Models.Dto.Services.Cart
{
    public class CartDto
    {
        public CartHeader CartHeader { get; set; }
        public ICollection<CartDetailDto> CartDetails { get; set; }
        public CartDto()
        {
            CartHeader = new CartHeader();
            CartDetails = new List<CartDetailDto>();
        }
    }
}
