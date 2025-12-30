using System.ComponentModel.DataAnnotations;

namespace NeutralContainer.Data;

public class Post
{
    public int Id { get; set; }

    [Required]
    public string CreatorUserId { get; set; } = string.Empty;

    [Required]
    public PostType PostType { get; set; } = PostType.YouTubeBacked;

    [MaxLength(200)]
    public string? YouTubeUrl { get; set; }

    [MaxLength(32)]
    public string? YouTubeVideoId { get; set; }

    [MaxLength(200)]
    public string? Title { get; set; }

    [MaxLength(2000)]
    public string? ContextText { get; set; }

    public FeedbackMode AllowedFeedbackModes { get; set; }

    public AvoidanceMode AvoidanceModes { get; set; }

    public SensitivityFlag SensitivityFlags { get; set; }

    [MaxLength(2000)]
    public string? CustomRulesText { get; set; }

    [Required]
    public VisibilityPolicy VisibilityPolicy { get; set; } = VisibilityPolicy.PrivateOnly;

    [Required]
    public ModerationLevel ModerationLevel { get; set; } = ModerationLevel.Standard;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
