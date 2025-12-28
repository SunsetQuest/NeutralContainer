using System.ComponentModel.DataAnnotations;

namespace NeutralContainer.Data;

public class CommentReport
{
    public int Id { get; set; }

    [Required]
    public int CommentId { get; set; }

    public Comment? Comment { get; set; }

    [Required]
    public string ReporterUserId { get; set; } = string.Empty;

    public ApplicationUser? ReporterUser { get; set; }

    [MaxLength(1000)]
    public string? Reason { get; set; }

    public ReportStatus Status { get; set; } = ReportStatus.Open;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}

public enum ReportStatus
{
    Open = 0,
    Resolved = 1
}
