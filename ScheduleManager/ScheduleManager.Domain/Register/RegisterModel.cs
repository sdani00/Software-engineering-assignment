using System.ComponentModel.DataAnnotations;

namespace ScheduleManager.Domain.Register
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "First name is required.")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "First name can contain only letters.")]
        [MaxLength(50, ErrorMessage = "Maximum length of this field is 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Last name can contain only letters.")]
        [MaxLength(50, ErrorMessage = "Maximum length of this field is 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [RegularExpression("^[a-zA-Z]+[a-zA-Z0-9]*@gmail\\.com$", ErrorMessage = "Incorrect email (required format: <firstName>.<lastName>@gmail.com).")]
        [MaxLength(80, ErrorMessage = "Maximum length of this field is 80 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(10, ErrorMessage = "Password must have at least 10 characters.")]
        [MaxLength(50, ErrorMessage = "Maximum length of this field is 50 characters.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "A password confirmation is required.")]
        [DataType(DataType.Password)]
        [MinLength(10, ErrorMessage = "Password must have at least 10 characters.")]
        [MaxLength(50, ErrorMessage = "Maximum length of this field is 50 characters.")]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmationPassword { get; set; }
    }
}
