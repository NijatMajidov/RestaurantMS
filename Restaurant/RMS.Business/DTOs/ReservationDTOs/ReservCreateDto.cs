using System.ComponentModel.DataAnnotations;

namespace RMS.Business.DTOs.ReservationDTOs
{
    public class ReservCreateDto
    {
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
         public int GuestCount { get; set; }
        [Required]
        public DateTime ReservDate {  get; set; }
        [Required]
        public TimeSpan ReservTime { get; set; }
        public string? Note { get; set; }
        [Required]
        public int TableId { get; set; }
        

    }
}
