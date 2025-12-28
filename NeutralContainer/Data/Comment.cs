using System.ComponentModel.DataAnnotations;

namespace NeutralContainer.Data;

public class Comment
{
    public int Id { get; set; }

    [Required]
    public int PostId { get; set; }

    public Post? Post { get; set; }

    [Required]
    public string CommenterUserId { get; set; } = string.Empty;

    public ApplicationUser? CommenterUser { get; set; }

    [Required]
    [MaxLength(2000)]
    public string Body { get; set; } = string.Empty;

    public CommentVisibility Visibility { get; set; } = CommentVisibility.Private;

    public CommentModerationStatus ModerationStatus { get; set; } = CommentModerationStatus.Approved;

    [MaxLength(4000)]
    public string? ModerationReasonsJson { get; set; }

    public bool CommenterPublicConsent { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
