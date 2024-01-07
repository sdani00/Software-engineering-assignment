using MediatR;
using ScheduleManager.Domain.Repositories;

namespace ScheduleManager.Application.Logout
{
    public class LogoutHandler : IRequestHandler<LogoutRequest, bool>
    {
        private readonly IAuthRepository _authRepository;

        public LogoutHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<bool> Handle(LogoutRequest request, CancellationToken cancellationToken)
        {
            await _authRepository.Logout();
            return new bool();
        }
    }
}
