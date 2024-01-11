using BusinessTripPlanner.Application.ChangeStatus;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ScheduleManager.Application.GetBusinessTripsForUser;
using ScheduleManager.DataAccess.Models;
using ScheduleManager.Domain.Enums;
using ScheduleManager.Domain.Filter;
using ScheduleManager.Domain.GetBusinessTripForUser;

namespace ScheduleManager.Pages
{
    public class ViewRequestsUserModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly string _returnURL = "/ViewBusinessTripRequestUser";

        public ViewRequestsUserModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public IEnumerable<BusinessTripForUserModel> UserTripsRequests { get; set; }

        [BindProperty]
        public SearchCriteria SearchCriteria { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            return await RefreshPage();
        }

        public async Task<IActionResult> OnPostFilterAsync()
        {
            return await RefreshPage();

        }

        public async Task<IActionResult> OnPostCancelAsync(Guid Id)
        {
            var request = new GetBusinessTripsForUserRequest()
            {
                Email = User.Identity.Name,
            };

            UserTripsRequests = await _mediator.Send(request);

            if (UserTripsRequests != null)
            {
                ChangeStatusRequest requestChangeStatus = new()
                {
                    Status = RequestStatus.Cancelled,
                    TripId = Id
                };

                bool changedStatusSuccessfully = await _mediator.Send(requestChangeStatus);

                if (changedStatusSuccessfully)
                {
                    
                    string viewRequests = Request.GetDisplayUrl().ToString().Substring(0, Request.GetDisplayUrl().ToString().LastIndexOf("/")) + "/ViewRequestsSMA";
                }
            }

            return LocalRedirect(_returnURL);
        }

        private async Task<IActionResult> RefreshPage()
        {
            GetBusinessTripsForUserRequest request = new()
            {
                Email = User.Identity.Name,
                SearchCriteria = SearchCriteria
            };

            if (ModelState.IsValid)
            {
                UserTripsRequests = await _mediator.Send(request);
                return Page();
            }

            return Page();
        }
    }
}
