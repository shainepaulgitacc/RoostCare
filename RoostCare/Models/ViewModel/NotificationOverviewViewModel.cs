using RoostCare.Models.Domain;

namespace RoostCare.Models.ViewModel
{
    public class NotificationOverviewViewModel
    {
        public bool IsPageArchived { get; set; }

        public List<NotificationEntity> Notifications { get; set; }
    }
}
