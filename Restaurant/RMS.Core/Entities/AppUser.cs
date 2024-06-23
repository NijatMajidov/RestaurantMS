using Microsoft.AspNetCore.Identity;

namespace RMS.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateOnly? Born { get; set; }
        public bool IsDeleted { get; set; }

    }
}
