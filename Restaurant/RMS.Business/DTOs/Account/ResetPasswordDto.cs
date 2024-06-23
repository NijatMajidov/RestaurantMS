using System.ComponentModel.DataAnnotations;

namespace RMS.Business.DTOs.Account
{
    public class ResetPasswordDto
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password), Compare(nameof(Password))]

        public string ConfirmPassword { get; set; }
    }
}
