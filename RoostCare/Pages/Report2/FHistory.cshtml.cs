using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoostCare.Models.Infrastracture;
using RoostCare.Models.ViewModel;

namespace RoostCare.Pages.Report2
{
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public class FHistoryModel : PageModel
    {
        private readonly IRoosterRepository _repo;
        public FHistoryModel(IRoosterRepository repo)
        {
            _repo = repo;
        }

        public List<RoosterReportViewModel> RFReports { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public async Task OnGetAsync(DateTime? dTo = null, DateTime? dFrom = null)
        {
            var rec = await _repo.RoosterReportRecords(dTo, dFrom);
            DateFrom = dFrom;
            DateTo = dTo;
            RFReports = rec.ToList();
        }
    }
}
