using System.ComponentModel.DataAnnotations;

namespace NeutralContainer.Data;

public class CreatorBlock
{
    public int Id { get; set; }

    [Required]
    public string CreatorUserId { get; set; } = string.Empty;

    [Required]
    public string BlockedUserId { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
