using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;
using RoostCare.Models.ViewModel;
using RoostCare.Models.ViewModel.InputModel;

namespace RoostCare.Pages.IncomeManagement
{

    public class RoosterList
    {
        public Rooster Rooster { get; set; }
        public Income? Income { get; set; }
    }
   
    [ValidateAntiForgeryToken]
    [Authorize]
    public class CockSalesModel : PageModel
    {
        private readonly IBaseRepository<Income> _incomeRepo;
        private readonly IBaseRepository<Rooster> _roosterRepo;
        private readonly IMapper _mapper;
        public CockSalesModel(IBaseRepository<Income> incomeRepo, IBaseRepository<Rooster> roosterRepo,IMapper mapper)
        {
            _incomeRepo = incomeRepo;
            _roosterRepo = roosterRepo;
            _mapper = mapper;
        }
        [BindProperty]
        public List<string> RoostersId { get; set; }
        public List<IncomeListViewModel> Incomes { get; set; }
        public List<RoosterList> Roosters { get; set; }
        public double TotalAmount { get; set;}

        [BindProperty]
        public IncomeInputModel Input { get; set; }
        public async Task OnGetAsync()
        {
            var incomes = await _incomeRepo.GetAll();
            var roosters = await _roosterRepo.GetAll();


            Roosters = roosters
                .GroupJoin(incomes,
                r => r.Id,
                i => i.RoosterId,
                (r, i) => new
                {
                    Income = i,
                    Rooster = r
                })
                .Select(x => new RoosterList
                {
                    Rooster = x.Rooster,
                    Income = x.Income.FirstOrDefault()
                }).Where(x => x.Income?.RoosterId == null && x.Rooster.RoosterCategory == RoosterCategory.ForSale).ToList() ;

           

            var result = incomes
                .Join(roosters,
                i => i.RoosterId,
                r => r.Id,
                (i, r) => new
                {
                   Income = i,
                   Rooster = r
                })
                .GroupBy(result => result.Income.Id)
                .Select(s => new IncomeListViewModel
                {
                    Id = s.Key,
                    Date = s.First().Income.Date,
                    IncomeCategories = s.First().Income.IncomeCategory,
                    Amount = s.Sum(x => x.Rooster.Price)
                }).ToList();

            TotalAmount = result.Where(x => x.IncomeCategories == IncomeCategories.LiveStockSalesRoosters).Sum(x => x.Amount);
            Incomes = result;
            
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            foreach(var roosterId in RoostersId)
            {
                var converted = _mapper.Map<Income>(Input);
                converted.RoosterId = int.Parse(roosterId);
                await _incomeRepo.Add(converted);
            }
            TempData["validation-message"] = "Successfully added";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetDelete(string rId = null)
        {
            try
            {
                await _incomeRepo.Delete(rId);
                TempData["validation-message"] = "Successfully deleted";
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToPage();
        }

    }
}
