using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ScheduleManager.Application.RegisterBusinessTrip;
using ScheduleManager.DataAccess.Models.Enums;
using ScheduleManager.Domain.RegisterBusinessTrip;

namespace ScheduleManager.Pages
{
    [Authorize(Roles = "User")]
    public class CreateBusinessTripModel : PageModel
    {
        private readonly IMediator _mediator;

        public CreateBusinessTripModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public RegisterBusinessTripModel BusinessTripModel { get; set; }

        [BindProperty]
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

            var registerBusinessTripRequest = new RegisterBusinessTripRequest()
            {
                RegisterBusinessTripModel = BusinessTripModel,
                Email = User.Identity.Name
            };

            bool requestSent = await _mediator.Send(registerBusinessTripRequest);

            if (requestSent is true)
            {
                var returnUrl = Url.Content("~/ViewRequestsUser");
                return RedirectToPage(returnUrl);
            }
            
            RequestStatus = Status.Status.Rejected;
            return Page();
        }
    }
}
