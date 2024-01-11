using MediatR;

namespace ScheduleManager.Application.GenerateToken
{
    public class GenerateTokenRequest : IRequest<string>
    {
        public string Email { get; set; }
    }
}
