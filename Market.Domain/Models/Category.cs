using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Models
{
    public class Category : BaseModel
    {
        [Key]
        public long CategoryId { get; set; }
    }
}
