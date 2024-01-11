using ScheduleManager.Domain.Enums;
using ScheduleManager.Domain.Filter;
using ScheduleManager.Domain.GetBusinessTripForUser;
using ScheduleManager.Domain.RegisterBusinessTrip;

namespace ScheduleManager.Domain.Repositories
{
    public interface ITripRequestRepository
    {
        Task<bool> AddAsync(RegisterBusinessTripModel businessTripRequestBusinessModel, string email);

        Task<IEnumerable<BusinessTripForUserModel>> GetAllTripsForUserByCriteria(string email, SearchCriteria searchCriteria);

        Task<bool> UpdateStatusTrip(Guid id, RequestStatus status);
    }
}
