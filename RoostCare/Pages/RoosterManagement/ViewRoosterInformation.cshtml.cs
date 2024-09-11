using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using RoostCare.Hubs;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;
using RoostCare.Models.ViewModel;
using RoostCare.Models.ViewModel.InputModel;
using System.Net;
using System.Runtime.CompilerServices;

namespace RoostCare.Pages.RoosterManagement
{
	[ValidateAntiForgeryToken]
	[Authorize]
	public class ViewRoosterInformationModel : PageModel
	{
		private readonly IRoosterRepository _roosterRepo;
		private readonly IBaseRepository<FightHistory> _fHRepo;
		private readonly IBaseRepository<MedicationHistory> _medicRepo;

		private readonly IBaseRepository<ApplicationUser> _accRepo;
		private readonly UserManager<ApplicationUser> _userManager;

		private readonly IBaseRepository<NotificationEntity> _notifRepo;
		private readonly IMapper _mapper;
		private readonly IHubContext<NotificationHub,INotificationHub> _notifHub;
		public ViewRoosterInformationModel(
			IRoosterRepository roosterRepo,
			 IBaseRepository<FightHistory> fHRepo,
			 IBaseRepository<MedicationHistory> medicRepo,
			 IBaseRepository<NotificationEntity> notifRepo,
			  IBaseRepository<ApplicationUser> accRepo,
			  UserManager<ApplicationUser> userManager,
			  IHubContext<NotificationHub, INotificationHub> notifHub,
		IMapper mapper)
		{
			_roosterRepo = roosterRepo;
			_fHRepo = fHRepo;
			_medicRepo = medicRepo;
			_notifRepo = notifRepo;
			_mapper = mapper;
			_userManager = userManager;
			_notifHub = notifHub;

		}
		public RoosterViewModel Rooster { get; set; }
		public IEnumerable<FightHistory> FightHistories { get; set; }
		public IEnumerable<MedicationHistory> MedicationHistories { get; set; }
		public string? PreviousUrl { get; set; }

		[BindProperty]
		public RoosterStatus? RStatInput { get; set; }

		[BindProperty]
		public FightHistoryInputModel FInputModel { get; set; }
		[BindProperty]
		public MedicationHistoryInputModel MInputModel { get; set; }
		public async Task OnGetAsync(int Id, string? prevUrl = null)
		{
			var fightHistories = await _fHRepo.GetAll();
			PreviousUrl = prevUrl;
			FightHistories = fightHistories.Where(x => x.RoosterId == Id).ToList();
			var medicHistories = await _medicRepo.GetAll();
			MedicationHistories = medicHistories.Where(x => x.RoosterId == Id).ToList();
			var roosters = await _roosterRepo.RoosterRecords();
			Rooster = roosters.FirstOrDefault(x => x.Rooster.Id == Id);
		}

		public async Task<IActionResult> OnGetDeleteFightHistory(string rId, int Id)
		{
			await _fHRepo.Delete(rId);
			TempData["validation-message"] = "Successfully deleted";
			return RedirectToPage("ViewRoosterInformation", new { Id });
		}
		public async Task<IActionResult> OnGetDeleteMedicationHistory(string rId, int Id)
		{
			await _medicRepo.Delete(rId);
			TempData["validation-message"] = "Successfully deleted";
			return RedirectToPage("ViewRoosterInformation", new { Id });
		}


		public async Task<IActionResult> OnPostAddFight(string prevUrl)
		{

			var converted = _mapper.Map<FightHistory>(FInputModel);
			await _fHRepo.Add(converted);
			var Id = converted.RoosterId;
			TempData["validation-message"] = "Successfully added";
			return RedirectToPage("ViewRoosterInformation", new { Id, prevUrl });

		}
		public async Task<IActionResult> OnPostAddMedication(string prevUrl)
		{
			var converted = _mapper.Map<MedicationHistory>(MInputModel);
			await _medicRepo.Add(converted);
			var Id = converted.RoosterId;
			TempData["validation-message"] = "Successfully added";
			return RedirectToPage("ViewRoosterInformation", new { Id, prevUrl });

		}
		public async Task<IActionResult> OnPostUpdateFight(string prevUrl)
		{
			var converted = _mapper.Map<FightHistory>(FInputModel);
			await _fHRepo.Update(converted.Id.ToString(), converted);
			var Id = converted.RoosterId;
			TempData["validation-message"] = "Successfully updated";
			return RedirectToPage("ViewRoosterInformation", new { Id, prevUrl });

		}
		public async Task<IActionResult> OnPostUpdateMedication(string prevUrl)
		{
			var converted = _mapper.Map<MedicationHistory>(MInputModel);
			await _medicRepo.Update(converted.Id.ToString(), converted);
			var Id = converted.RoosterId;
			TempData["validation-message"] = "Successfully updated";
			return RedirectToPage("ViewRoosterInformation", new { Id, prevUrl });

		}

		public async Task<IActionResult> OnPostUpdateRStatus(string rId, string prevUrl)
		{
			var rooster = await _roosterRepo.GetOne(rId);
			if (rooster == null)
				return BadRequest($"{rId} is invalid id");

			rooster.RoosterStatus = (RoosterStatus)RStatInput;

			var users = await _userManager.GetUsersInRoleAsync("Staff");
			var notifications = await _notifRepo.GetAll();
			var rStatus = rooster.RoosterStatus == RoosterStatus.ReadyToFight ? "is already ready for the new fight." : rooster.RoosterStatus == RoosterStatus.UnderMaintenance ? "has some minor or major injury" : rooster.RoosterStatus == RoosterStatus.Healthy ? "is healthy" : "is injured and need medication";
			
				foreach (var user in users)
				{
					var notifCount = notifications.Where(x => !x.IsViewed && x.Recipient == user.Id).Count();
					var notifCountString = notifCount > 5 ? $"5+" : notifCount.ToString();
					NotificationEntity notifVal = new NotificationEntity();
					notifVal.Title = "Rooster Status";
					notifVal.Description = $"The status of the rooster with ID {rooster.Id.ToString("00000")} {rStatus}";
					notifVal.IsViewed = false;
					notifVal.Recipient = user.Id;
					notifVal.IsArchived = false;
					notifVal.UpdatedAt = DateTime.Now;
					notifVal.AddedAt = DateTime.Now;
					await _notifRepo.Add(notifVal);
					_notifHub.Clients.User(user.Id).NotificationAlert(notifCountString);
				}
			await _roosterRepo.Update(rId, rooster);
			TempData["validation-message"] = "Successfully updated";
			var Id = rooster.Id;
			return RedirectToPage("ViewRoosterInformation", new { Id, prevUrl });
		}
	}
}
