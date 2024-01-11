using MediatR;
using ScheduleManager.Domain.Repositories;

namespace ScheduleManager.Application.RegisterBusinessTrip
{
    public class RegisterBusinessTripHandler : IRequestHandler<RegisterBusinessTripRequest, bool>
    {
        private readonly ITripRequestRepository _tripRequestRepository;

        public RegisterBusinessTripHandler(ITripRequestRepository tripRequestRepository)
        {
            _tripRequestRepository = tripRequestRepository;
        }

        public async Task<bool> Handle(RegisterBusinessTripRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return false;
            }

            return await _tripRequestRepository.AddAsync(request.RegisterBusinessTripModel, request.Email);
        }
    }
}
