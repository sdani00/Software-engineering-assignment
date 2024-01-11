using MediatR;
using ScheduleManager.Domain.Filter;
using ScheduleManager.Domain.GetBusinessTripForUser;

namespace ScheduleManager.Application.GetBusinessTripsForUser
{
    public class GetBusinessTripsForUserRequest : IRequest<IEnumerable<BusinessTripForUserModel>>
    {
        public string Email { get; set; }

        public SearchCriteria? SearchCriteria { get; set; }
    }
}
