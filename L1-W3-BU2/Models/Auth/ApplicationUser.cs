using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace L1_W3_BU2.Models.Auth
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required DateOnly BirthDate { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
}
