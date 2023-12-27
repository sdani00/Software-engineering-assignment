using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ScheduleManager.DataAccess.Context;
using ScheduleManager.DataAccess.Models;
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

            return isUserRegistered;
        }

        public Task Logout()
        {
            throw new NotImplementedException();
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
