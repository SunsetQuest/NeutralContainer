using System.ComponentModel.DataAnnotations;

namespace NeutralContainer.Data;

public class Post
{
    public int Id { get; set; }

    [Required]
    public string CreatorUserId { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string YouTubeUrl { get; set; } = string.Empty;

    [Required]
    [MaxLength(32)]
    public string YouTubeVideoId { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Title { get; set; }

    [MaxLength(2000)]
    public string? ContextText { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
