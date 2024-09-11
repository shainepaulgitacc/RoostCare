using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;
using RoostCare.Models.ViewModel.InputModel;

namespace RoostCare.Pages.IncomeManagement
{
	[Authorize]
	public class IndexModel : BasePageModel<Income, IncomeInputModel>
	{
		private readonly IBaseRepository<Income> _incRepo;
		private readonly IBaseRepository<Rooster> _roosterRepo;
		private readonly IBaseRepository<Hatch> _hatchRepo;
		public IndexModel(IBaseRepository<Income> incRepo, IBaseRepository<Rooster> roosterRepo, IBaseRepository<Hatch> hatchRepo, IMapper mapper) : base(incRepo, mapper)
		{
			_incRepo = incRepo;
			_roosterRepo = roosterRepo;
			_hatchRepo = hatchRepo;
		}


		public List<Income> Incomes { get; set; }
		public double TotalAmount { get; set; }

		public List<Rooster> Roosters { get; set; }
		public async Task OnGetAsync()
		{
			var incomes = await _incRepo.GetAll();
			Incomes = incomes.Where(x => x.IncomeCategory == IncomeCategories.CockFightEarnings).ToList();
			TotalAmount = (double)incomes.Where(x => x.IncomeCategory == IncomeCategories.CockFightEarnings).Sum(x => x.Amount);

			var roosters = await _roosterRepo.GetAll();
			Roosters = roosters.Where(x => x.RoosterCategory == RoosterCategory.ForSale).ToList();
		}
		public override async Task<IActionResult> OnPostAsync(string? returnUrl = null, string? Id = null)
		{
			await base.OnPostAsync(returnUrl, Id);
			return RedirectToPage();

		}
	}
}
