using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ScheduleManager.Application.Register;
using ScheduleManager.Domain.Register;

namespace ScheduleManager.Pages.Account
{
    [AllowAnonymous]
    public class RegisterPageModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RegisterPageModel> _logger;
        private readonly string _redirectUrlAfterRegister = "/Index";

        public RegisterPageModel(IMediator mediator, ILogger<RegisterPageModel> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [BindProperty]
        public RegisterModel RegisterModel { get; set; }

        public Status.Status RequestStatus { get; set; }


        public void OnGet()
        {
            RequestStatus = Status.Status.Pending;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var registerRequest = new RegisterRequest()
            {
                RegisterModel = RegisterModel
            };
            
            bool registrationIsConfirmed = await _mediator.Send(registerRequest);

            if (!registrationIsConfirmed)
            {
                RequestStatus = Status.Status.Rejected;
                return Page();
            }

            return Redirect(_redirectUrlAfterRegister);
        }
    }
}
