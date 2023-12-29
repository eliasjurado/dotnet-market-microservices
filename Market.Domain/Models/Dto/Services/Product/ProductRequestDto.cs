using System.Text.Json.Serialization;

namespace Market.Domain.Models.Dto.Services.Product
{
    public sealed class ProductRequestDto : ProductDto
    {
        [JsonIgnore]
        public override long ProductId { get; set; }
    }
}
