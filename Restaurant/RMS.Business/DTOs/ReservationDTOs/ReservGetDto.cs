using System.ComponentModel.DataAnnotations;

namespace RMS.Business.DTOs.ReservationDTOs
{
    public class ReservGetDto
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [Required]
        [MaxLength(10)]
        public string Phone { get; set; } = null!;
        [Required]
        [MinLength(1)]
        public int NumberOfGuests { get; set; }
        [Required]
        public DateTime ReservationDate { get; set; }
        [Required]
        public TimeSpan ReservationTime { get; set; }
        [MaxLength(250)]
        public string Note { get; set; }
        public int? TableId { get; set; }
        public string? QrCodeData { get; set; }
    }
}
