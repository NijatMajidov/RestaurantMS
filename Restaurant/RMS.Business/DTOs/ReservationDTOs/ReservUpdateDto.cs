using System.ComponentModel.DataAnnotations;

namespace RMS.Business.DTOs.ReservationDTOs
{
    public class ReservUpdateDto
    {
        public int Id {  get; set; }
        public int NumberOfGuests { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan ReservationTime { get; set; }
        public string Note { get; set; }
        public int? TableId { get; set; }
        public string? QrCodeData { get; set; }
    }
}
