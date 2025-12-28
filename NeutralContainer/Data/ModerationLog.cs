using System.ComponentModel.DataAnnotations;

namespace NeutralContainer.Data;

public class ModerationLog
{
    public int Id { get; set; }

    [Required]
    [MaxLength(40)]
    public string EntityType { get; set; } = string.Empty;

    [Required]
    [MaxLength(64)]
    public string EntityId { get; set; } = string.Empty;

    [Required]
    [MaxLength(80)]
    public string Action { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Reason { get; set; }

    [Required]
    public string ActorUserId { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
