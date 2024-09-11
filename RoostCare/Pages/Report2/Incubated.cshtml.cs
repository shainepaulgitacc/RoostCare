using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.FileSystemGlobbing;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;

namespace RoostCare.Pages.Report2
{
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public class IncubatedModel : PageModel
    {
        private readonly IBaseRepository<Incubation> _repo;
        public IncubatedModel(IBaseRepository<Incubation> repo)
        {
            _repo = repo;
        }
        public List<Incubation> IncubationTrackers { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public async Task OnGetAsync(DateTime? dTo = null, DateTime? dFrom = null)
        {
            var incubations = await _repo.GetAll();
            DateFrom = dFrom;
            DateTo = dTo;
            if (dTo != null || dFrom != null)
            {
                IncubationTrackers = incubations.Where(x => x.AddedAt.Date >= dFrom?.Date && x.AddedAt.Date <= dTo?.Date).ToList();
            }
            else
            {
                IncubationTrackers = incubations.ToList();
            }

        }
    }
}
