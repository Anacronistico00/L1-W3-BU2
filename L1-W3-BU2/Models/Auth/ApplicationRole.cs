using Microsoft.AspNetCore.Identity;

namespace L1_W3_BU2.Models.Auth
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
}
