namespace Market.Domain.Models.Dto.Web
{
    public sealed record ProductViewModel
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public double ProductPrice { get; set; }
        public string Description { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductImageUrl { get; set; }
    }
}
