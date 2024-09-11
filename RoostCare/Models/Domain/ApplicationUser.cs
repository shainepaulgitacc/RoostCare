using Microsoft.AspNetCore.Identity;

namespace RoostCare.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }

        public string? LastName { get; set; }
        public Sex Sex { get; set; }

        public string? Address { get; set; }

    }

}
