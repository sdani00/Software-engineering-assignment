using System.ComponentModel.DataAnnotations;

namespace ScheduleManager.Domain.Enums
{
    public enum RequestStatus
    {
        [Display(Name = "Pending")]
        Pending,
        [Display(Name = "Accepted")]
        Accepted,
        [Display(Name = "Rejected")]
        Rejected,
        [Display(Name = "Cancelled")]
        Cancelled,
    }
}
