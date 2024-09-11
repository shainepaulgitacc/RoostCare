namespace RoostCare.Models.Domain
{
    public class Expenses:BaseEntity
    {
        public DateTime Date { get;  set; }
        public ExpensesCategories Category { get;  set; }
        //public string Description { get; set;}
        public int Amount { get;  set; }

    }
}
