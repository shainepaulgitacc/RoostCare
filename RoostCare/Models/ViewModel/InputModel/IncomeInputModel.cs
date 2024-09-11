using RoostCare.Models.Domain;

namespace RoostCare.Models.ViewModel.InputModel
{
    public class IncomeInputModel:BaseInputModel
    {
        public IncomeCategories IncomeCategory { get; set; }
        public DateTime Date { get; set; }
        public double? Amount { get; set; }
        public int? Quantity { get; set; }

    }
}
