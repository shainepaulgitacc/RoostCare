using System.ComponentModel.DataAnnotations.Schema;

namespace RoostCare.Models.Domain
{
    public class Income:BaseEntity
    {
        public IncomeCategories IncomeCategory { get; set; }
        public int? RoosterId { get; set; }
        public DateTime Date { get; set; }
        public double? Amount { get; set; }
       public int? Quantity { get; set; }
    }
}
