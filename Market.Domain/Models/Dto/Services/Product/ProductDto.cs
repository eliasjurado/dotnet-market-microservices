namespace Market.Domain.Models.Dto.Services.Product
{
    public class ProductDto
    {
        public virtual long ProductId { get; set; }
        public string Name { get; set; }
        public double ProductPrice { get; set; }
        public string Description { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductImageUrl { get; set; }
    }
}
