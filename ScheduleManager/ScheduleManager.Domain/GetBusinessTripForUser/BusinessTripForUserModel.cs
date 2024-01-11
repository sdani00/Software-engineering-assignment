using ScheduleManager.Domain.Enums;

namespace ScheduleManager.Domain.GetBusinessTripForUser
{
    public class BusinessTripForUserModel
    {
        public Guid Id { get; set; }

        public string ClientLocation { get; set; }

        public string Client { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public RequestStatus Status { get; set; }
    }
}
