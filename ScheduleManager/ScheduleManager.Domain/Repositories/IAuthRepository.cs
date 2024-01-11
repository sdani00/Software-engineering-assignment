using ScheduleManager.Domain.Login;
using ScheduleManager.Domain.Register;

namespace ScheduleManager.Domain.Repositories
{
    public interface IAuthRepository
    {
        Task<bool> Register(RegisterModel user);
        Task Logout();
        Task<string> Login(LoginModel user);
        Task<bool> ConfirmEmail(string email, string token);
    }
}
