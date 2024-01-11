using MediatR;
using Microsoft.Extensions.Logging;
using ScheduleManager.Domain.Services;

namespace ScheduleManager.Application.SendConfirmationEmail
{
    public class SendConfirmationEmailHandler : IRequestHandler<SendConfirmationEmailRequest, bool>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ITemplateFillerService _templateFillerService;
        private readonly ILogger<SendConfirmationEmailHandler> _logger;

        public SendConfirmationEmailHandler(IAuthenticationService authenticationService,
            ITemplateFillerService templateFillerService, ILogger<SendConfirmationEmailHandler> logger)
        {
            _authenticationService = authenticationService;
            _templateFillerService = templateFillerService;
            _logger = logger;
        }

        public async Task<bool> Handle(SendConfirmationEmailRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
               return false;
            }

            var pathToTemplate = Path.GetDirectoryName(Directory.GetCurrentDirectory())
                +Path.DirectorySeparatorChar.ToString() + 
                "ScheduleManager.Notification" + 
                Path.DirectorySeparatorChar.ToString() + 
                "Templates" + 
                Path.DirectorySeparatorChar.ToString()
                + "Email Templates" +
                Path.DirectorySeparatorChar.ToString() +
                "ConfirmAccountRegistration.cshtml";

            object model = new
            {
                FirstName = request.RegisterModel.FirstName,
                LastName = request.RegisterModel.LastName,
                Link = request.ConfirmationLink
            };

            var body = _templateFillerService.FillTemplate(pathToTemplate, model);

            return _authenticationService.SendEmail(request.RegisterModel.Email, "Confirm your email", body).IsCompletedSuccessfully;
        }
    }
}
