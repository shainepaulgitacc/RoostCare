namespace RoostCare.Models.ViewModel.InputModel
{
    public class NotificationInputModel:BaseInputModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Recipient { get; set; }
        public bool IsViewed { get; set; }
        public bool IsArchived { get; set; }
        public string? LinkRedirect { get; set; }
    }
}
