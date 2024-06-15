using Microsoft.AspNetCore.Http;
using RMS.Core.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Core.Entities
{
    public class Table:BaseEntity
    {
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

    }
}
