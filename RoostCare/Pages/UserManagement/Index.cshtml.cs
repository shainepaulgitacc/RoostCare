using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoostCare.Models.Domain;
using RoostCare.Models.Infrastracture;
using RoostCare.Models.ViewModel.InputModel;

namespace RoostCare.Pages.UserManagement
{
    public class IndexModel : PageModel
    {
        private readonly IBaseRepository<ApplicationUser> _accRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        public IndexModel(
            IBaseRepository<ApplicationUser> accRepo,
            UserManager<ApplicationUser> userManager)
        {
            _accRepo = accRepo;
            _userManager = userManager;
        }
        [BindProperty]
        public UserInputModel InputModel{get; set;}
        public IEnumerable<ApplicationUser> Users { get; set; }
        public async Task OnGetAsync()
        {
            var users = await _accRepo.GetAll();
            Users = users.Where(x => x.UserName != User.Identity?.Name);
        }

        public async Task<IActionResult> OnPostUpdate()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _userManager.FindByIdAsync(InputModel.Id);
            user.EmailConfirmed = true;
            user.Email = InputModel.Email;
            user.NormalizedEmail = InputModel.Email.ToUpper();
            user.UserName = InputModel.Username;
            user.NormalizedUserName = InputModel.Username.ToUpper();
            user.FirstName = InputModel.FirstName;
            user.MiddleName = InputModel.MiddleName;
            user.LastName = InputModel.LastName;
            user.Sex = InputModel.Sex;
            user.PhoneNumber = InputModel.PhoneNumber;
            user.Address = InputModel.Address;
            await _accRepo.Update(user.Id, user);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnGetDelete(string rId)
        {
            var user = await _accRepo.GetOne(rId);
            await _userManager.UpdateSecurityStampAsync(user);
            await _accRepo.Delete(rId) ;
            TempData["validation-message"] = "Successfully deleted";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = new ApplicationUser
            {
                EmailConfirmed = true,
                Email = InputModel.Email,
                UserName = InputModel.Username,
                NormalizedEmail = InputModel.Email.ToUpper(),
                NormalizedUserName = InputModel.Username.ToUpper(),
                FirstName = InputModel.FirstName, 
                MiddleName = InputModel.MiddleName,
                LastName = InputModel.LastName,
                Sex = InputModel.Sex,
                PhoneNumber = InputModel.PhoneNumber,
                Address = InputModel.Address
            };
            var result = await _userManager.CreateAsync(user,InputModel.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Staff");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                     TempData["validation-message"] = error.Description;
                }
            }
            return RedirectToPage();
        }
    }
}
