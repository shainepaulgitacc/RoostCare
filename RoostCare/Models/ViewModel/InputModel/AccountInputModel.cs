using RoostCare.Models.Domain;

namespace RoostCare.Models.ViewModel.InputModel
{
	public class AccountInputModel
	{

		public string UserName { get; set; }
		public string Email { get; set; }

		public string? FirstName { get; set; }
		public string? MiddleName { get; set; }

		public string? LastName { get; set; }
		public Sex Sex { get; set; }

		public string? Address { get; set; }
		public string? PhoneNumber { get; set; }
	}
}
