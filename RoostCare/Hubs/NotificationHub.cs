using Microsoft.AspNetCore.SignalR;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;

namespace RoostCare.Hubs
{
	public class NotificationHub:Hub<INotificationHub>
	{
		private readonly IBaseRepository<NotificationEntity> _repo;
		public NotificationHub(IBaseRepository<NotificationEntity> repo)
		{
			_repo = repo;
		}
	}
}
