using Microsoft.AspNetCore.Http;
using RMS.Core.Entities.Common;
using RMS.Core.Entities.Employee;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Core.Entities
{
    public class Table:BaseEntity
    {
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsReserved { get; set; }
        public string? WaiterId { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

    }
}
