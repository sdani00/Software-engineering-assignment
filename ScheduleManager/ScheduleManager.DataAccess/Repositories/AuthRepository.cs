using Microsoft.AspNetCore.Identity;
using ScheduleManager.DataAccess.Context;
using ScheduleManager.DataAccess.Models;
using ScheduleManager.Domain.Login;
using ScheduleManager.Domain.Register;
using ScheduleManager.Domain.Repositories;

namespace ScheduleManager.DataAccess.Repositories
{
    public class AuthRepository : IAuthRepository
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ScheduleManagerContext _context;

        public AuthRepository(UserManager<User> userManager, SignInManager<User> signInManager, ScheduleManagerContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public async Task<bool> Register(RegisterModel user)
        {
            var dbUser = MapUser(user);

            var isUserRegistered = (await _userManager.CreateAsync(dbUser, user.Password)).Succeeded;
            var isUserRoleAdded = (await _userManager.AddToRoleAsync(dbUser, "User")).Succeeded;

            return isUserRegistered && isUserRoleAdded;
        }

        public async Task<string> Login(LoginModel user)
        {
            var registeredUser = await _userManager.FindByEmailAsync(user.Email);

            if (registeredUser == null)
            {
                return null;
            }

            var loginResult = await _signInManager.PasswordSignInAsync(registeredUser, user.Password, false, false);

            return loginResult.Succeeded ? (await _userManager.GetRolesAsync(registeredUser)).FirstOrDefault() : null;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        private User MapUser(RegisterModel user)
        {
            return new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.Email
            };
        }
    }
}
