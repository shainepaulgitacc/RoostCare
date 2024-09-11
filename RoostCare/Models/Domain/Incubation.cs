namespace RoostCare.Models.Domain
{
    public class Incubation:BaseEntity
    {
        public DateTime DateLaid { get; set; }
        public int NumberOfEggs { get; set; }
        public DateTime IncubationStart { get; set; }
        public DateTime IncubationComplete { get; set; }
        public int? NumberOfNonViableEggs { get; set; }
        public int? NumberOfViableEggs { get; set; }
    }
}
