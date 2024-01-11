﻿using System.ComponentModel.DataAnnotations;

namespace ScheduleManager.Domain.Filter
{
    public class SearchCriteria : IValidatableObject
    {
        public string? Location { get; set; }
        public string? Client { get; set; }
        public Enums.RequestStatus Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate == null && EndDate != null)
            {
                yield return new ValidationResult(errorMessage: "Please choose a Start Date.", memberNames: new[] { "StartDate" });
            }
            else if (StartDate != null && EndDate == null)
            {
                yield return new ValidationResult(errorMessage: "Please choose an End Date.", memberNames: new[] { "EndDate" });
            }
            else if (EndDate <= StartDate)
            {
                yield return new ValidationResult(errorMessage: "End Date must be greater than Start Date.", memberNames: new[] { "EndDate" });
            }
        }
    }
}
