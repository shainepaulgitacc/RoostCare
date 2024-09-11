using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoostCare.Models.Infrastracture;

namespace RoostCare.Pages.Scanner
{
    [Authorize(Roles = "Staff")]
    [ValidateAntiForgeryToken]
    public class IndexModel : PageModel
    {
        public readonly IRoosterRepository _rRepo;
        public IndexModel(IRoosterRepository rRepo)
        {
            _rRepo = rRepo;
        }
        [BindProperty]
        public string Id { get; set; }
        public async Task OnGetAsync()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var rooster = await _rRepo.GetOne(Id);
            if (rooster == null)
                return BadRequest("Cant find the record");
            var prevUrl = "/Scanner/Index";
            return RedirectToPage("/RoosterManagement/ViewRoosterInformation", new {Id, prevUrl});
        }
    }
}
