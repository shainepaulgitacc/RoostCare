namespace RoostCare.Models.ViewModel.InputModel
{
    public class BaseInputModel
    {
        public int? Id { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
