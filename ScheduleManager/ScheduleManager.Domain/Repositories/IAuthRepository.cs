using ScheduleManager.Domain.Register;

namespace ScheduleManager.Domain.Repositories
{
    public interface IAuthRepository
    {
        Task<bool> Register(RegisterModel user);
        Task Logout();
    }
}
