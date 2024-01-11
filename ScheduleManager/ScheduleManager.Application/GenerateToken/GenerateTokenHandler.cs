using MediatR;
using ScheduleManager.Application.Services;
using ScheduleManager.Domain.Services;

namespace ScheduleManager.Application.GenerateToken
{
    public class GenerateTokenHandler : IRequestHandler<GenerateTokenRequest, string>
    {
        private readonly IAuthenticationService _authenticationService;

        public GenerateTokenHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<string> Handle(GenerateTokenRequest request, CancellationToken cancellationToken)
        {
            return await _authenticationService.GenerateEmailRegistrationToken(request.Email);
        }
    }
}
