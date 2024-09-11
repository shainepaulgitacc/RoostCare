using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;

namespace RoostCare.Pages.Report2
{
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public class IncomeModel : PageModel
    {
        private readonly IBaseRepository<Income> _repo;
        private readonly IBaseRepository<Rooster> _roosterRepo;
        public IncomeModel(IBaseRepository<Income> repo, IBaseRepository<Rooster> roosterRepo)
        {
            _repo = repo;
            _roosterRepo = roosterRepo;
        }

        public List<Income> Records { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public async Task OnGetAsync(DateTime? dTo = null, DateTime? dFrom = null)
        {
            var  incomes= await _repo.GetAll();
            var rooster = await _roosterRepo.GetAll();
            var joined = incomes
                .GroupJoin(rooster,
                i => i.RoosterId,
                r => r.Id,
                (i, r) => new
                {
                    Income = i,
                    Rooster = r.FirstOrDefault()
                })
                .GroupBy(x => x.Income.Id)

                .Select(f => new Income
                {
                    Id = f.First().Income.Id,
                    IncomeCategory = f.First().Income.IncomeCategory,
                    Date = f.First().Income.Date,
                    Amount = f.Sum(x => x.Income.Amount) > 0 ? f.Sum(x => x.Income.Amount) : f.Where(x => x.Rooster != null && x.Rooster?.RoosterCategory == RoosterCategory.ForSale).Sum(x => x.Rooster?.Price)


                })
                .ToList();
                
            DateFrom = dFrom;
            DateTo = dTo;
            if (dTo != null || dFrom != null)
            {
                Records = joined.Where(x => x.Date.Date >= dFrom?.Date && x.Date.Date <= dTo?.Date).ToList();
            }
            else
            {
                Records = joined.ToList();
            }


        }
    }
}
