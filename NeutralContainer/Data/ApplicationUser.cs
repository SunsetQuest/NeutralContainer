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

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public bool IsSuspended { get; set; }

        public DateTimeOffset? SuspendedAt { get; set; }

        public DateTimeOffset? SuspendedUntil { get; set; }

        [MaxLength(500)]
        public string? SuspensionReason { get; set; }
    }

}
