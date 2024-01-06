using MediatR;
using ScheduleManager.Domain.Login;

namespace ScheduleManager.Application.Login
{
    public class LoginRequest : IRequest<string>
    {
        public LoginModel Model { get; set; }
    }
}
