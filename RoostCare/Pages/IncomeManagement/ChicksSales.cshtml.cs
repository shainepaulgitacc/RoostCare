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
    public class HatchListView
    {
        public Income Income { get; set; }
        public Hatch Hatch { get; set; }
    }
    [Authorize]
    public class ChicksSalesModel:BasePageModel<Income,IncomeInputModel>
    {

        private readonly IBaseRepository<Income> _incRepo;
        private readonly IBaseRepository<Rooster> _roosterRepo;
        private readonly IBaseRepository<Hatch> _hatchRepo;
        private readonly IMapper _mapper;
        public ChicksSalesModel(IBaseRepository<Income> incRepo, IBaseRepository<Rooster> roosterRepo, IBaseRepository<Hatch> hatchRepo, IMapper mapper):base(incRepo,mapper)
        {
            _incRepo = incRepo;
            _roosterRepo = roosterRepo;
            _hatchRepo = hatchRepo;
            _mapper = mapper;
        }
        public List<Income> Incomes { get; set; }
        public double TotalAmount { get; set; }

        public List<Hatch> Hatches { get; set; }
        public async Task OnGetAsync()
        {
            var incomes = await _incRepo.GetAll();
            TotalAmount = (double)incomes.Where(x => x.IncomeCategory == IncomeCategories.LiveStockSalesChicks).Sum(x => x.Amount);
            Incomes = incomes.Where(x => x.IncomeCategory == IncomeCategories.LiveStockSalesChicks).ToList();

        }

        public override async Task<IActionResult> OnPostAsync(string? returnUrl = null, string? Id = null)
        {

            var hatches = await _hatchRepo.GetAll();
            var incomes = await _incRepo.GetAll();
            var totalAvailable = hatches.Sum(x => x.NumberOfHatchlings) - incomes.Where(x => x.IncomeCategory == IncomeCategories.LiveStockSalesChicks).Sum(x => x.Quantity);
            if(totalAvailable < Input.Quantity)
            {
                TempData["validation-message"] = "Invalid input: the quantity must be less than or equal to the total number of chick records.";
                return RedirectToPage();
            }
           await base.OnPostAsync(returnUrl, Id);
            return RedirectToPage();
        }
    }
}
