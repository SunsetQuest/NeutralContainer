using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace NeutralContainer.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [MaxLength(100)]
        public string? DisplayName { get; set; }
    }

}
