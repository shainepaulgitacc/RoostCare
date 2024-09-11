namespace RoostCare.Models.Infrastracture
{
	public interface INotificationHub
	{
		Task NotificationAlert(string count);
	}
}
