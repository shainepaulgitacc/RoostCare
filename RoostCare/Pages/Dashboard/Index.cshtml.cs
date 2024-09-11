using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;

namespace RoostCare.Pages.Dashboard
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IRoosterRepository _roosterRepo;
        private readonly IBaseRepository<Incubation> _incRepo;
        private readonly IBaseRepository<Hatch> _hatchRepo;
        private readonly IBaseRepository<Income> _incomeRepo;
        private readonly IBaseRepository<Expenses> _expRepo;
        private readonly IBaseRepository<FightHistory> _fhRepo;
        public IndexModel(
             IRoosterRepository roosterRepo,
             IBaseRepository<Incubation> incRepo,
              IBaseRepository<Hatch> hatchRepo,
              IBaseRepository<Income> incomeRepo,
              IBaseRepository<Expenses> expRepo,
              IBaseRepository<FightHistory> fhRepo)
        {
            _roosterRepo = roosterRepo;
            _incRepo = incRepo;
            _hatchRepo = hatchRepo;
            _incomeRepo = incomeRepo;
            _expRepo = expRepo;
            _fhRepo = fhRepo;
        }
        public int CountRoosters { get; set; }
        public int CountForCockFight { get; set; }
        public int CountForSale { get; set; }
        public int CountTotalEggs { get; set; }
        public int CountTotalChicks { get; set; }
        public double TotalIncome { get; set; }
        public double TotalExpenses { get; set; }
        public double TotalBalance { get; set; }
        public async Task OnGetAsync()
        {
            var roosters = await _roosterRepo.GetAll();
            var fightHistories = await _fhRepo.GetAll();
            var forCockFightCount = roosters.Where(x => x.RoosterCategory == RoosterCategory.ForCockFight)
                .GroupJoin(fightHistories,
                r => r.Id,
                f => f.RoosterId,
                (r, f) => new
                {
                    Rooster = r,
                    FightHistory = f.FirstOrDefault()
                })
                .GroupBy(x => x.Rooster.Id)
                .Select(f => new
                {
                    Rooster = f.First().Rooster,
                    HasLoose = fightHistories.Where(x => x.RoosterId == f.Key).Any(x => x.FightResult == FightResult.Loss)

                })
                .Count(x => !x.HasLoose);
			var incomes = await _incomeRepo.GetAll();
			CountRoosters = roosters.Count();
            CountForCockFight = forCockFightCount;
            var roostersRecord = await _roosterRepo.RoosterRecords();
            CountForSale = roostersRecord.Where(x => x.Income == null && x.Rooster.RoosterCategory == RoosterCategory.ForSale).Count();
			var incubations = await _incRepo.GetAll();
            var hatchlings = await _hatchRepo.GetAll();
            var deductions = (int)hatchlings.Sum(x => x.NumberOfHatchlings); 
            var tEggs = incubations.Sum(x => x.NumberOfEggs);
            CountTotalEggs = tEggs - deductions;
            var incomeForChicksSales = (int)incomes.Where(x => x.IncomeCategory == IncomeCategories.LiveStockSalesChicks).Sum(x=> x.Quantity);
            var hatchlingsCount =  hatchlings.Sum(x => x.NumberOfHatchlings)??0;
            var totalChicks = hatchlingsCount - incomeForChicksSales;
            CountTotalChicks = totalChicks;
            var totalCockSales = incomes
                .Join(roosters,
                i => i.RoosterId,
                r => r.Id,
                (i, r) => new
                {
                    Income = i,
                    Rooster = r
                }).Sum(x =>x.Rooster.Price);
            var totalIncome = (double)incomes.Sum(x => x.Amount) + totalCockSales;
            TotalIncome = totalIncome;
            var expenses = await _expRepo.GetAll();
            TotalExpenses = expenses.Sum(x => x.Amount);
            TotalBalance =  totalIncome - expenses.Sum(x => x.Amount);
        }

        public async Task<JsonResult> OnGetIncomeExpenses()
        {
            var expenses = await _expRepo.GetAll();
            var incomes = await _incomeRepo.GetAll();
            var roosters = await _roosterRepo.GetAll();
            var cYear = DateTime.Now.Year;
            var result = new IncomeExpensesPerMonth();
            result.ExpensesPerMonth = new ExpensesPerMonth
            {
                January = expenses.Where(x => x.Date.Month == 1 && x.Date.Year == cYear).Sum(x =>x.Amount),
                February= expenses.Where(x => x.Date.Month == 2 && x.Date.Year == cYear).Sum(x => x.Amount),
                March = expenses.Where(x => x.Date.Month == 3 && x.Date.Year == cYear).Sum(x => x.Amount),
                April = expenses.Where(x => x.Date.Month == 4 && x.Date.Year == cYear).Sum(x => x.Amount),
                May = expenses.Where(x => x.Date.Month == 5 && x.Date.Year == cYear).Sum(x => x.Amount),
                June = expenses.Where(x => x.Date.Month == 6 && x.Date.Year == cYear).Sum(x => x.Amount),
                July = expenses.Where(x => x.Date.Month == 7 && x.Date.Year == cYear).Sum(x => x.Amount),
                August = expenses.Where(x => x.Date.Month == 8 && x.Date.Year == cYear).Sum(x => x.Amount),
                September = expenses.Where(x => x.Date.Month == 9 && x.Date.Year == cYear).Sum(x => x.Amount),
                October = expenses.Where(x => x.Date.Month == 10 && x.Date.Year == cYear).Sum(x => x.Amount),
                November = expenses.Where(x => x.Date.Month == 11 && x.Date.Year == cYear).Sum(x => x.Amount),
                December = expenses.Where(x => x.Date.Month == 12 && x.Date.Year == cYear).Sum(x => x.Amount),
            };


            var cockSales = incomes
               .Join(roosters,
               i => i.RoosterId,
               r => r.Id,
               (i, r) => new
               {
                   Income = i,
                   Rooster = r
               }).ToList();

            var jan = (double)incomes.Where(x => x.Date.Month == 1 && x.Date.Year == cYear).Sum(x => x.Amount) + cockSales.Where(x => x.Income.Date.Month == 1 && x.Income.Date.Year == cYear).Sum(x => x.Rooster.Price);
            var feb = (double)incomes.Where(x => x.Date.Month == 2 && x.Date.Year == cYear).Sum(x => x.Amount) + cockSales.Where(x => x.Income.Date.Month == 2 && x.Income.Date.Year == cYear).Sum(x => x.Rooster.Price);
            var mar = (double)incomes.Where(x => x.Date.Month == 3 && x.Date.Year == cYear).Sum(x => x.Amount) + cockSales.Where(x => x.Income.Date.Month == 3 && x.Income.Date.Year == cYear).Sum(x => x.Rooster.Price);
            var ap = (double)incomes.Where(x => x.Date.Month == 4 && x.Date.Year == cYear).Sum(x => x.Amount) + cockSales.Where(x => x.Income.Date.Month == 4 && x.Income.Date.Year == cYear).Sum(x => x.Rooster.Price);
            var may = (double)incomes.Where(x => x.Date.Month == 5 && x.Date.Year == cYear).Sum(x => x.Amount) + cockSales.Where(x => x.Income.Date.Month == 5 && x.Income.Date.Year == cYear).Sum(x => x.Rooster.Price);
            var jun = (double)incomes.Where(x => x.Date.Month == 6 && x.Date.Year == cYear).Sum(x => x.Amount) + cockSales.Where(x => x.Income.Date.Month == 6 && x.Income.Date.Year == cYear).Sum(x => x.Rooster.Price);
            var jul = (double)incomes.Where(x => x.Date.Month == 7 && x.Date.Year == cYear).Sum(x => x.Amount) + cockSales.Where(x => x.Income.Date.Month == 7 && x.Income.Date.Year == cYear).Sum(x => x.Rooster.Price);
            var aug  = (double)incomes.Where(x => x.Date.Month ==  8 && x.Date.Year == cYear).Sum(x => x.Amount) + cockSales.Where(x => x.Income.Date.Month == 8 && x.Income.Date.Year == cYear).Sum(x => x.Rooster.Price);
            var sep = (double)incomes.Where(x => x.Date.Month == 9 && x.Date.Year == cYear).Sum(x => x.Amount) + cockSales.Where(x => x.Income.Date.Month == 9 && x.Income.Date.Year == cYear).Sum(x => x.Rooster.Price);
            var oct =(double)incomes.Where(x => x.Date.Month == 10 && x.Date.Year == cYear).Sum(x => x.Amount) + cockSales.Where(x => x.Income.Date.Month == 10 && x.Income.Date.Year == cYear).Sum(x => x.Rooster.Price);
            var nov  = (double)incomes.Where(x => x.Date.Month == 11 && x.Date.Year == cYear).Sum(x => x.Amount) + cockSales.Where(x => x.Income.Date.Month == 11 && x.Income.Date.Year == cYear).Sum(x => x.Rooster.Price);
           var dec = (double)incomes.Where(x => x.Date.Month == 12 && x.Date.Year == cYear).Sum(x => x.Amount) + cockSales.Where(x => x.Income.Date.Month == 12 && x.Income.Date.Year == cYear).Sum(x => x.Rooster.Price);
            result.IncomePerMonth = new IncomePerMonth
            {
                January = jan,
                February = feb,
                March = mar,
                April = ap,
                May = may,
                June =jun,
                July = jul,
                August = aug,
                September = sep,
                October = oct,
                November = nov,
                December = dec,
            };
            return new JsonResult(result);
        }
    }

    public class IncomeExpensesPerMonth
    {
        public ExpensesPerMonth ExpensesPerMonth { get; set; }
        public IncomePerMonth IncomePerMonth { get; set; }
    }
    public class IncomePerMonth : ExpensesPerMonth
    {

    }
    public class ExpensesPerMonth
    {
        public double January { get; set; }
        public double February { get; set; }
        public double March { get; set; }
        public double April { get; set; }
        public double May { get; set; }
        public double June { get; set; }
        public double July { get; set; }
        public double August { get; set; }
        public double September { get; set; }
        public double October { get; set; }
        public double November { get; set; }
        public double December { get; set; }

    }
}
