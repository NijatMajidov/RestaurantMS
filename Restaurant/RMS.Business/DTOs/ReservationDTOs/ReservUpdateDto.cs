using System.ComponentModel.DataAnnotations;

namespace RMS.Business.DTOs.ReservationDTOs
{
    public class ReservUpdateDto
    {
        public int GuestCount { get; set; }
        public DateTime ReservDate { get; set; }
        public TimeSpan ReservTime { get; set; }
        [MaxLength(250)]
        public string Note { get; set; }
    }
}
