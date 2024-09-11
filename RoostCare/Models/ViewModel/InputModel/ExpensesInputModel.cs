using RoostCare.Models.Domain;

namespace RoostCare.Models.ViewModel.InputModel
{
    public class ExpensesInputModel:BaseInputModel
    {
        public DateTime Date { get; set; }
        public ExpensesCategories Category { get; set; }
       // public string Description { get; set; }
        public int Amount { get; set; }

    }
}
