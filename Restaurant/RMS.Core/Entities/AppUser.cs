using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateOnly? Born { get; set; }
        public bool IsDeleted { get; set; }
        public string? ImageUrl { get; set; }
        public string? Biography { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

    }
}
