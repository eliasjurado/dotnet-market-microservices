using System.ComponentModel.DataAnnotations;
using static Market.Infrastructure.Base;

namespace Market.Domain.Models
{
    public class BaseModel
    {
        [Required]
        public ByteType IsDeleted { get; set; } = ByteType.No;
        [Required]
        public string CreatedBy { get; set; } = DefaultUser;
        [Required]
        public string UpdatedBy { get; set; } = DefaultUser;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
