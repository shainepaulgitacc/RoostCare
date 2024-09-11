namespace RoostCare.Models.Domain
{
    public class Sales:BaseEntity
    {
        public DateTime Date { get;  set; }
        public ProductType ProductType { get; set; }
        public int Quantity { get; set; }
        public int SalesPerUnit { get;  set; }
        public int TotalSalesAmmount { get;  set; }
    }
}
