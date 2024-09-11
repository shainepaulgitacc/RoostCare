using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;
using RoostCare.Models.ViewModel;

namespace RoostCare.Pages.ViewComponents
{
   
   public class NotificationOverviewViewComponent : ViewComponent
    {
        private readonly IBaseRepository<NotificationEntity> _repo;
        private readonly UserManager<ApplicationUser> _userManager;

        public NotificationOverviewViewComponent(
            IBaseRepository<NotificationEntity> repo,
            UserManager<ApplicationUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool s)
        {
            var notifications = await _repo.GetAll();
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            var filtered = notifications.Where(x => x.Recipient == user.Id).ToList();
            return View(new NotificationOverviewViewModel
            {
                Notifications = filtered,
                IsPageArchived = s
            });
        }
    }
}
