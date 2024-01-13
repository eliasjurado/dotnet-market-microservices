namespace Market.Domain.Models.Dto.Services.Cart
{
    public class CartDto
    {
        public CartHeaderDto CartHeader { get; set; }
        public ICollection<CartDetailDto> CartDetails { get; set; }
        public CartDto()
        {
            CartHeader = new CartHeaderDto();
            CartDetails = new List<CartDetailDto>();
        }
    }
}
