using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using ScheduleManager.DataAccess.Models;
using ScheduleManager.Domain.Repositories;
using ScheduleManager.Domain.Services;

namespace ScheduleManager.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;

        private readonly IEmailSender _emailSender;

        private readonly IAuthRepository _authRepository;

        public AuthenticationService(UserManager<User> userManager, IEmailSender emailSender,
            IAuthRepository authRepository)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _authRepository = authRepository;
        }

        public async Task<string> GenerateEmailRegistrationToken(string email)
        {
            User user = await _userManager.FindByEmailAsync(email);
            string token = null;

            if (user != null)
            {
                token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            }

            return token;
        }

        public async Task SendEmail(string email, string subject, string body)
        {
            await _emailSender.SendEmailAsync(email, subject, body);
        }

        public Task<bool> ConfirmEmail(string email, string token)
        { 
            return _authRepository.ConfirmEmail(email, token);
        }
    }
}
