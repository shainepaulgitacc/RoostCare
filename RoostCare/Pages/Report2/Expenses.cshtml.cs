using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;

namespace RoostCare.Pages.Report2
{
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public class ExpensesModel : PageModel
    {
        private readonly IBaseRepository<Expenses> _repo;
        public ExpensesModel(IBaseRepository<Expenses> repo)
        {
            _repo = repo;
        }
 
        public List<Expenses> Records { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public async Task OnGetAsync(DateTime? dTo = null, DateTime? dFrom = null)
        {
            var hatches = await _repo.GetAll();
            DateFrom = dFrom;
            DateTo = dTo;
            if (dTo != null || dFrom != null)
            {
                Records = hatches.Where(x => x.Date.Date >= dFrom?.Date && x.Date.Date <= dTo?.Date).ToList();
            }
            else
            {
                Records = hatches.ToList();
            }


        }
    }
}
