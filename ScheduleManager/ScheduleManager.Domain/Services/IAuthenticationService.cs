namespace ScheduleManager.Domain.Services
{
    public interface IAuthenticationService
    {
        Task<string> GenerateEmailRegistrationToken(string email);
        Task SendEmail(string email, string subject, string body);
        Task<bool> ConfirmEmail(string email, string token);
    }
}
