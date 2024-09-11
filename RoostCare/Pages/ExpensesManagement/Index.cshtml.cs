using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;
using RoostCare.Models.ViewModel.InputModel;

namespace RoostCare.Pages.ExpensesManagement
{
    [Authorize]
    public class IndexModel :BasePageModel<Expenses,ExpensesInputModel>
    {
        private readonly IBaseRepository<Expenses> _repo;
        public IndexModel(IBaseRepository<Expenses> repo,IMapper mapper):base(repo,mapper)
        {
            _repo = repo;
        }
        public List<Expenses> ExpensesList { get;  set; }
        public double TotalAmount { get; set; }
        public async Task OnGetAsync()
        {
            var expensesList = await _repo.GetAll();
            ExpensesList = expensesList.ToList();
            TotalAmount = expensesList.Sum(x => x.Amount);
        }
    }
}
