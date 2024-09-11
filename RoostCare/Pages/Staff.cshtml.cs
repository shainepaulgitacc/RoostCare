using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RoostCare.Pages
{
	[Authorize(Roles = "staff")]
	public class StaffModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
