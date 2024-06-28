using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RMS.Business.DTOs.EmployeeDTOs
{
    public class UpdateEmployeeDto
    {
        
        [MinLength(3)]
        [MaxLength(50)]
        public string? Name { get; set; }
        
        [MinLength(3)]
        [MaxLength(50)]
        public string? Surname { get; set; }
        
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        
        [MinLength(3)]
        [MaxLength(100)]
        public string? UserName { get; set; }
        public string? Role { get; set; }
        public string? Biography { get; set; }
        public IFormFile? ImageFile { get; set; } 
    }
}
