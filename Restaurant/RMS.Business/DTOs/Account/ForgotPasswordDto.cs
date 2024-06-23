using System.ComponentModel.DataAnnotations;

namespace RMS.Business.DTOs.Account
{
    public class ForgotPasswordDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
