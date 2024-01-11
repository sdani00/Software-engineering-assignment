using MediatR;
using ScheduleManager.Domain.Register;

namespace ScheduleManager.Application.SendConfirmationEmail
{
    public class SendConfirmationEmailRequest : IRequest<bool>
    {
        public RegisterModel RegisterModel { get; set; }

        public string ConfirmationLink { get; set; }
    }
}
