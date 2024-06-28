using RMS.Core.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace RMS.Core.Entities
{
    public class Restaurant : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string Country { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string City { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string District { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string Street { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string BuildNum { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;
    }
}
