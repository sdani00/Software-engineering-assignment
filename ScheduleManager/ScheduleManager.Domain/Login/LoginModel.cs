using System.ComponentModel.DataAnnotations;

namespace ScheduleManager.Domain.Login
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        [MaxLength(50, ErrorMessage = "Maximum length of this field is 50 characters")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(50, ErrorMessage = "Maximum length of this field is 50 characters")]
        public string? Password { get; set; }
    }
}
