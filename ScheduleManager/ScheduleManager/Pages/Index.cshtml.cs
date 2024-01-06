using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ScheduleManager.Pages
{ public class IndexModel : PageModel
    {
        private readonly string _redirectUrlLogin = "/Account/Login";

        public IActionResult OnGet()
        {
            if (User is null)
            {
                return RedirectToPage(_redirectUrlLogin);
            }

            return RedirectToPage(_redirectUrlLogin);
        }
    }
}