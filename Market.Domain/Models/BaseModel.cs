using System.ComponentModel.DataAnnotations;
using static Market.Infrastructure.Base;

namespace Market.Domain.Models
{
    public class BaseModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public ByteType IsDeleted { get; set; } = ByteType.No;
        [Required]
        public Guid CreatedBy { get; set; } = new Guid();
        [Required]
        public Guid UpdatedBy { get; set; } = new Guid();
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
