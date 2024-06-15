using Microsoft.AspNetCore.Identity;

namespace RMS.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly? Born { get; set; }
        public bool IsDeleted { get; set; }
    }
}
