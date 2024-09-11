using RoostCare.Models.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RoostCare.Models.ViewModel.InputModel
{
    public class UserInputModel
    {
        public string? Id { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string Address { get; set; }
        public Sex Sex { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        public string Username { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
