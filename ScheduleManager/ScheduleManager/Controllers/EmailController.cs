using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Domain.Services;

namespace ScheduleManager.Controllers
{
    [Route("Email")]
    public class EmailController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public EmailController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ConfirmEmail/{userEmail}/{userToken}", Name = "ConfirmEmail/{userEmail}/{userToken}")]
        public async Task<IActionResult> ConfirmEmail(string userEmail, string userToken)
        {
            bool isEmailConfirmed = await _authenticationService.ConfirmEmail(userEmail, userToken);

            if (isEmailConfirmed)
                return View("ConfirmEmail");

            return View();
        }
    }
}
