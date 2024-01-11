

using MediatR;
using ScheduleManager.Domain.Repositories;

namespace BusinessTripPlanner.Application.ChangeStatus;

public class ChangeStatusHandler : IRequestHandler<ChangeStatusRequest, bool>
{
	private readonly ITripRequestRepository _businessTripRequestRepository;

	public ChangeStatusHandler(ITripRequestRepository businessTripRequestRepository)
	{
		_businessTripRequestRepository = businessTripRequestRepository;
	}

	public async Task<bool> Handle(ChangeStatusRequest? request, CancellationToken cancellationToken)
	{
		if (request == null)
			return false;

		return await _businessTripRequestRepository.UpdateStatusTrip(request.TripId, request.Status);
	}
}
