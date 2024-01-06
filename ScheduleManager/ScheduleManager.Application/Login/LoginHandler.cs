using MediatR;
using ScheduleManager.Domain.Repositories;

namespace ScheduleManager.Application.Login
{
    public class LoginHandler : IRequestHandler<LoginRequest, string>
    {
        private readonly IAuthRepository _authRepository;

        public LoginHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<string> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return null;
            }

            return await _authRepository.Login(request.Model);
        }
    }
}
