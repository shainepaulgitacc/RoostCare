using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;

namespace RoostCare.Pages.Notification
{

	[Authorize]
	[ValidateAntiForgeryToken]
	public class ArchivedNotificationModel : PageModel
	{
		private readonly IBaseRepository<NotificationEntity> _notificationRepo;
		private readonly UserManager<ApplicationUser> _userManager;

		public ArchivedNotificationModel(
			IBaseRepository<NotificationEntity> notificationRepo,
			UserManager<ApplicationUser> userManager,
			IMapper mapper)
		{
			_notificationRepo = notificationRepo;
			_userManager = userManager;
		}

		public List<NotificationEntity> Notifications { get; set; }

		public DateTime? From { get; set; }
		public DateTime? To { get; set; }
		public async Task OnGetAsync([FromQuery] DateTime? fNotif_From, [FromQuery] DateTime? fNotif_To)
		{
			var notifications = await _notificationRepo.GetAll();
			var user = await _userManager.FindByNameAsync(User.Identity.Name);

			From = fNotif_From;
			To = fNotif_To;

			if (From != null && To != null)
			{
				Notifications = notifications.Where(x => x.Recipient == user.Id && x.IsArchived && x.AddedAt.Date >= From.Value.Date && x.AddedAt.Date <= To.Value.Date).ToList();

			}
			else
			{
				Notifications = notifications.Where(x => x.Recipient == user.Id && x.IsArchived).ToList();
			}
		}

		public async Task<IActionResult> OnGetGetAll()
		{
			TempData["validation-message"] = "Successfully retrieved all notifications.";
			return RedirectToPage();
		}


		public async Task<IActionResult> OnGetAction(string Id, NotifActionType t, string? From = null, string? To = null)
		{
			var notification = await _notificationRepo.GetOne(Id);
			if (notification == null)
			{
				return BadRequest($"Invalid Id");
			}
			if (t == NotifActionType.Delete)
			{
				await _notificationRepo.Delete(Id);
				TempData["validation-message"] = "Successfully deleted";
				return RedirectToPage("./ArchivedNotification", new { fNotif_From = From, fNotif_To = To });
			}

			if (t == NotifActionType.Restore)
			{
				notification.IsArchived = false;
				TempData["validation-message"] = "Successfully restore notification";
			}
			notification.UpdatedAt = DateTime.Now;
			await _notificationRepo.Update(notification.Id.ToString(), notification);
			return RedirectToPage("./ArchivedNotification", new { fNotif_From = From, fNotif_To = To });
		}

	}
}
