using MediatR;
using ScheduleManager.Domain.Repositories;

namespace ScheduleManager.Application.Register
{
    public class RegisterHandler : IRequestHandler<RegisterRequest, bool>
    {
        private readonly IAuthRepository _authRepository;
        public RegisterHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public async Task<bool> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return false;
            }

            return await _authRepository.Register(request.RegisterModel);
        }
    }
}
