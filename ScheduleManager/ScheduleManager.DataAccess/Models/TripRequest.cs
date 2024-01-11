using ScheduleManager.DataAccess.Models.Enums;

namespace ScheduleManager.DataAccess.Models
{
    public class TripRequest
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string LeavingFrom { get; set; }

        public string ClientLocation { get; set; }

        public BusinessTripRequestStatus Status { get; set; }

        public string Client { get; set; }

        public bool Card { get; set; }
    }
}

