using RMS.Core.Entities.Common;

namespace RMS.Core.Entities
{
    public class Reservation : BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Phone { get; set; } 
        public int NumberOfGuests { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan ReservationTime { get; set; }
        public string? Note { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int TableId { get; set; } 
        public Table Table { get; set; }
        public string? QrCodeData { get; set; }
    }
}
