using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;
using System.Runtime.InteropServices;

namespace RoostCare.Pages.Report2
{
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public class HatchedModel : PageModel
    {
        private readonly IBaseRepository<Hatch> _hatchRepo;
        public HatchedModel(IBaseRepository<Hatch> hatchRepo)
        {
            _hatchRepo = hatchRepo;
        }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public List<Hatch> Hatches { get; set; }
        public async Task OnGetAsync(DateTime? dTo = null, DateTime? dFrom = null)
        {
            var hatches = await _hatchRepo.GetAll();
            DateFrom = dFrom;
            DateTo = dTo;
            if (dTo != null || dFrom != null)
            {
                Hatches = hatches.Where(x => x.AddedAt.Date >= dFrom?.Date && x.AddedAt.Date <= dTo?.Date).ToList();
            }
            else
            {
                Hatches = hatches.ToList();
            }

           
        }
    }
}
