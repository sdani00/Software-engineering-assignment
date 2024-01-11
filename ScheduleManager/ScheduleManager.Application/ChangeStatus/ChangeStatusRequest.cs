using MediatR;
using ScheduleManager.Domain.Enums;

namespace BusinessTripPlanner.Application.ChangeStatus;

public class ChangeStatusRequest : IRequest<bool>
{
	public RequestStatus Status { get; set; }

	public Guid TripId { get; set; }
}
