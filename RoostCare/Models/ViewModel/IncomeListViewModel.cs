using RoostCare.Models.Domain;

namespace RoostCare.Models.ViewModel
{
    public class IncomeListViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public IncomeCategories IncomeCategories { get; set; }
        public double Amount { get; set; }
    }
}
