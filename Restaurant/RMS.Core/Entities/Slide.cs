using Microsoft.AspNetCore.Http;
using RMS.Core.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Core.Entities
{
    public class Slide : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string SubTitle { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
