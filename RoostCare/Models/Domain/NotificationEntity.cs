namespace RoostCare.Models.Domain
{
    public class NotificationEntity:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Recipient { get; set; }
        public bool IsViewed { get; set; }
        public bool IsArchived { get; set; }
        public string? LinkRedirect { get; set; }
    }
}
