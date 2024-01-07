using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ScheduleManager.Application.Logout;

namespace ScheduleManager.Pages.Account
{
    [Authorize]
    public class LogoutModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly ILogger<LogoutModel> _logger;
        private readonly string _redirectUrlAfterLogout = "/Account/Login";

        public LogoutModel(IMediator mediator, ILogger<LogoutModel> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await _mediator.Send(new LogoutRequest());

            return RedirectToPage(_redirectUrlAfterLogout);
        }
    }
}
