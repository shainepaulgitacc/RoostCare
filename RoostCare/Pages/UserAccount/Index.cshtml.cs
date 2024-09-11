using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;
using RoostCare.Models.ViewModel.InputModel;

namespace RoostCare.Pages.UserAccount
{
    [Authorize]
    [ValidateAntiForgeryToken]
    public class IndexModel : PageModel
    {
        private readonly IBaseRepository<ApplicationUser> _accRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        public IndexModel(
            IBaseRepository<ApplicationUser> accRepo,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _accRepo = accRepo;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public AccountInputModel Input { get; set; }
        public async Task OnGetAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var converted = _mapper.Map<AccountInputModel>(user);
            Input = converted;
        }
        public async Task<IActionResult> OnPostAsync(AccountInputModel Input)
        {
            if (!TryValidateModel(Input))
                return BadRequest(ModelState);
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
            user.UserName = Input.UserName;
            user.NormalizedUserName = Input.UserName.ToUpper();
            user.Email = Input.Email;
            user.NormalizedEmail = Input.Email.ToUpper();
            user.FirstName = Input.FirstName;
            user.MiddleName = Input.MiddleName;
            user.LastName = Input.LastName;
            user.Sex = Input.Sex;
            user.PhoneNumber = Input.PhoneNumber;
            user.Address = Input.Address;
            await _accRepo.Update(user.Id,user);
            await _signInManager.SignOutAsync();
            TempData["validation-message"] = "Successfully updated";
            return RedirectToPage();

		}
        public async Task<IActionResult> OnPostUpdatePassword(UpdatePasswordInputModel Input)
        {
			if (!TryValidateModel(Input))
				return BadRequest(ModelState);
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.Password);
            if (result.Succeeded)
            {
                TempData["validation-message"] = "Successfully update password";
                await _signInManager.SignOutAsync();
            }
            else
            {
				TempData["validation-message"] = result.Errors.First().Description;
            }
            return RedirectToPage();
		}
    }
}
