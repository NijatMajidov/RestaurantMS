using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RMS.Business.DTOs.EmployeeDTOs
{
    public class RegisterEmployeeDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Surname { get; set; }
        [Required]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Role { get; set; }
        public string? Biography { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }
    }
}
