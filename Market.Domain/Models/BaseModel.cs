using System.ComponentModel.DataAnnotations;
using static Market.Infrastructure.Base;

namespace Market.Domain.Models
{
    public class BaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public ByteType IsDeleted { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }
        [Required]
        public Guid UpdatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        public BaseModel()
        {
            var dtNow = DateTime.Now;
            var anonymousUser = new Guid();
            Name = string.Empty;
            Description = string.Empty;
            IsDeleted = ByteType.No;
            CreatedBy = anonymousUser;
            UpdatedBy = anonymousUser;
            CreatedAt = dtNow;
            UpdatedAt = dtNow;
        }
    }
}
