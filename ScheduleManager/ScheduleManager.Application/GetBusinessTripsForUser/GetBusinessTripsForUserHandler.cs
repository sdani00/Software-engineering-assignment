using System.Collections;
using MediatR;
using ScheduleManager.Domain.GetBusinessTripForUser;
using ScheduleManager.Domain.Repositories;

namespace ScheduleManager.Application.GetBusinessTripsForUser
{
    public class GetBusinessTripsForUserHandler : IRequestHandler<GetBusinessTripsForUserRequest, IEnumerable<BusinessTripForUserModel>>
    {
        private readonly ITripRequestRepository _businessTripRequestRepository;

        public GetBusinessTripsForUserHandler(ITripRequestRepository businessTripRequestRepository)
        {
            _businessTripRequestRepository = businessTripRequestRepository;
        }

        public async Task<IEnumerable<BusinessTripForUserModel>> Handle(GetBusinessTripsForUserRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return null;
            }

            if (request.SearchCriteria != null)
                FormatRequest(request);

            return await _businessTripRequestRepository.GetAllTripsForUserByCriteria(request.Email, request.SearchCriteria);
        }

        private void FormatRequest(GetBusinessTripsForUserRequest request)
        {
            if (!string.IsNullOrEmpty(request.SearchCriteria.Location))
            {
                request.SearchCriteria.Location = request.SearchCriteria.Location.Trim().ToLower();
                request.SearchCriteria.Location = char.ToUpper(request.SearchCriteria.Location[0]) + request.SearchCriteria.Location.Substring(1);
            }

            if (!string.IsNullOrEmpty(request.SearchCriteria.Client))
            {
                request.SearchCriteria.Client = request.SearchCriteria.Client.Trim().ToLower();
                request.SearchCriteria.Client = char.ToUpper(request.SearchCriteria.Client[0]) + request.SearchCriteria.Client.Substring(1);
            }

        }
    }
}
