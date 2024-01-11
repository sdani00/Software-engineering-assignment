using Microsoft.EntityFrameworkCore;
using ScheduleManager.DataAccess.Context;
using ScheduleManager.DataAccess.Models;
using ScheduleManager.DataAccess.Models.Enums;
using ScheduleManager.Domain.Enums;
using ScheduleManager.Domain.Filter;
using ScheduleManager.Domain.GetBusinessTripForUser;
using ScheduleManager.Domain.RegisterBusinessTrip;
using ScheduleManager.Domain.Repositories;

namespace ScheduleManager.DataAccess.Repositories
{
    public class TripRequestRepository : ITripRequestRepository
    {
        private readonly ScheduleManagerContext _context;

        public TripRequestRepository(ScheduleManagerContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(RegisterBusinessTripModel businessTripRequestBusinessModel, string email)
        {
            var trip = new TripRequest
            {
                Id = new Guid(),
                Email = email,
                Client = businessTripRequestBusinessModel.Client,
                ClientLocation = businessTripRequestBusinessModel.ClientLocation,
                StartDate = businessTripRequestBusinessModel.StartDate,
                EndDate = businessTripRequestBusinessModel.EndDate,
                Card = businessTripRequestBusinessModel.Card,
                LeavingFrom = businessTripRequestBusinessModel.LeavingFrom
            };

            await _context.TripRequests.AddAsync(trip);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<BusinessTripForUserModel>> GetAllTripsForUserByCriteria(string email, SearchCriteria searchCriteria)
        {
            var model = await _context.TripRequests.Where(e => e.Email.Equals(email))
                .Join(_context.Users,
                    user => user.Email,
                    request => request.Email,
                    (request, user) => new
                    {
                        user.FirstName,
                        user.LastName,
                        user.Email,
                        request.Id,
                        request.Client,
                        request.ClientLocation,
                        request.StartDate,
                        request.EndDate,
                        request.Status,
                    }).Select(user => new BusinessTripForUserModel()
                {
                    Id = user.Id,
                    StartDate = user.StartDate,
                    EndDate = user.EndDate,
                    Status = (RequestStatus)user.Status,
                    Client = user.Client,
                    ClientLocation = user.ClientLocation
                }).ToListAsync();
            model = FilterBusinessTripsByCriteria(model, searchCriteria).Result;
            return model;
        }

        public async Task<bool> UpdateStatusTrip(Guid id, RequestStatus status)
        {
            var currentBusinessTripRequest = await _context.TripRequests.FirstOrDefaultAsync(user => user.Id.Equals(id));
            currentBusinessTripRequest.Status = BusinessTripRequestStatusToBusinessTripStatus(status);
            _context.TripRequests.Update(currentBusinessTripRequest);
            return await _context.SaveChangesAsync() == 1;
        }

        private static BusinessTripRequestStatus BusinessTripRequestStatusToBusinessTripStatus(RequestStatus status)
        {
            TripRequest dbStatus = new()
            {
                Status = (Models.Enums.BusinessTripRequestStatus)status
            };
            return (BusinessTripRequestStatus)status;
        }

        private Task<List<BusinessTripForUserModel>> FilterBusinessTripsByCriteria(IEnumerable<BusinessTripForUserModel> businessTrips, SearchCriteria searchCriterias)
        {
            IQueryable<BusinessTripForUserModel> result = businessTrips.AsQueryable<BusinessTripForUserModel>();

            if (searchCriterias == null)
            {
                return Task.FromResult(result.Where(bt => bt.Status.Equals(RequestStatus.Pending)).ToList());
            }
            result = result.Where(bt => bt.Status.Equals(searchCriterias.Status));

            if (!string.IsNullOrEmpty(searchCriterias.Location))
            {
                result = result
                    .Where(bt => bt.ClientLocation.Equals(searchCriterias.Location));
            }

            if (!string.IsNullOrEmpty(searchCriterias.Client))
            {
                result = result
                    .Where(bt => bt.Client.Equals(searchCriterias.Client));
            }

            if (searchCriterias.StartDate != null && searchCriterias.EndDate != null)
            {
                result = result
                    .Where(bt => bt.StartDate >= searchCriterias.StartDate && bt.EndDate <= searchCriterias.EndDate);
            }

            return Task.FromResult(result.ToList());
        }
    }
}
