using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ScheduleManager.Application.Login;

namespace ScheduleManager.Pages.Account
{
    public class LoginModel : PageModel
    {

        private readonly IMediator _mediator;
        private readonly ILogger<LoginModel> _logger;
        private readonly string _redirectUrlAfterLoginForUser = "/ViewRequestsUser";
        private readonly string _redirectUrlAfterLoginForBto = "/ViewRequestsSMA";

        public LoginModel(IMediator mediator, ILogger<LoginModel> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [BindProperty]
        public Domain.Login.LoginModel LoginUser { get; set; }
        public Status.Status RequestStatus { get; set; }

        public bool Redirected { get; set; }

        public bool IsLockedOut { get; set; }

        public void OnGet(bool redirected)
        {
            RequestStatus = Status.Status.Pending;
            Redirected = redirected;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var loginRequest = new LoginRequest()
            {
                Model = LoginUser
            };

            string result = await _mediator.Send(loginRequest);

            if (string.IsNullOrEmpty(result))
            {
                RequestStatus = Status.Status.Rejected;
                return Page();
            }

            if (result == "User")
            {
                return  LocalRedirect(_redirectUrlAfterLoginForUser);
            }
            else if (result == "SMA")
            {
                return LocalRedirect(_redirectUrlAfterLoginForBto);
            }
            else if (result == "LockedOut")
            {
                IsLockedOut = true;
                return Page();
            }
            
            return Page();
        }   
    
    }
}
