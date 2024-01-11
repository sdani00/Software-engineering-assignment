using System.ComponentModel.DataAnnotations;

namespace ScheduleManager.Domain.RegisterBusinessTrip
{
    public class RegisterBusinessTripModel : IValidatableObject
    {
        [Required(ErrorMessage = "Departure location is required.")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Leaving location should contain only letters.")]
        [MaxLength(50, ErrorMessage = "Maximum length of this field is 50 characters.")]
        public string LeavingFrom { get; set; }

        [Required(ErrorMessage = "Client location is required.")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Client location should contain only letters.")]
        [MaxLength(50, ErrorMessage = "Maximum length of this field is 50 characters.")]
        public string ClientLocation { get; set; }

        public bool Card { get; set; }

        [Required(ErrorMessage = "Client name is required.")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Client name should be a valid name.")]
        [MaxLength(50, ErrorMessage = "Maximum length of this field is 50 characters.")]
        public string Client { get; set; }

        public DateTime StartDate { get; set; }


        public DateTime EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate < DateTime.Today)
            {
                yield return new ValidationResult("Start date can't be in the past", new[] { nameof(StartDate) });
            }

            if (StartDate.Date > EndDate.Date)
            {
                yield return new ValidationResult("Start date has to be before end date.", new[] { nameof(StartDate) });
            }

            if (StartDate.Date == EndDate.Date)
            {
                yield return new ValidationResult("Start date shouldn't be equal with end date", new[] { nameof(StartDate) });
            }

            if (StartDate == DateTime.Today)
            {
                yield return new ValidationResult("Start date can't be the current one", new[] { nameof(StartDate) });
            }
        }
    }
}
