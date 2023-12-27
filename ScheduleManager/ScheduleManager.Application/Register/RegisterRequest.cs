using MediatR;
using ScheduleManager.Domain.Register;

namespace ScheduleManager.Application.Register
{
    public class RegisterRequest : IRequest<bool>
    {
        public RegisterModel RegisterModel { get; set; }
    }
}
