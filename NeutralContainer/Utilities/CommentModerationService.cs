using NeutralContainer.Data;

namespace NeutralContainer.Utilities;

public sealed class CommentModerationService
{
    private static readonly string[] SevereHarassmentSignals =
    [
        "kill yourself",
        "kys",
        "i hate you",
        "idiot",
        "stupid",
        "worthless",
        "shut up"
    ];

    private static readonly string[] AdviceSignals =
    [
        "you should",
        "you need to",
        "you have to",
        "i recommend",
        "i suggest",
        "try to",
        "make sure you",
        "the best thing to do"
    ];

    private static readonly string[] DiagnosisSignals =
    [
        "you are depressed",
        "you sound depressed",
        "you are bipolar",
        "you are narcissistic",
        "narcissist",
        "bpd",
        "adhd",
        "ptsd"
    ];

    private static readonly string[] ShamingSignals =
    [
        "you should be ashamed",
        "what's wrong with you",
        "that's pathetic",
        "you are weak"
    ];

    public CommentModerationResult Evaluate(Post post, string body)
    {
        var normalized = body.Trim().ToLowerInvariant();
        var reasons = new List<ModerationReason>();

        if (ContainsAny(normalized, SevereHarassmentSignals))
        {
            reasons.Add(new ModerationReason(
                "harassment_or_disrespect",
                "Disrespectful or harassing language detected.",
                "Respectful tone required",
                ModerationSeverity.High));
        }

        var adviceAllowed = post.AllowedFeedbackModes.HasFlag(FeedbackMode.AdviceAllowed);
        var prescriptiveAvoided = post.AvoidanceModes.HasFlag(AvoidanceMode.PrescriptiveLanguage);
        if (!adviceAllowed && prescriptiveAvoided && ContainsAny(normalized, AdviceSignals))
        {
            reasons.Add(new ModerationReason(
                "unsolicited_advice",
                "Advice or prescriptive language detected.",
                "Advice not allowed in this container",
                ModerationSeverity.Medium));
        }

        var diagnosingAvoided = post.AvoidanceModes.HasFlag(AvoidanceMode.DiagnosingOrLabeling)
            || post.SensitivityFlags.HasFlag(SensitivityFlag.NoMentalHealthLabels);
        if (diagnosingAvoided && ContainsAny(normalized, DiagnosisSignals))
        {
            reasons.Add(new ModerationReason(
                "diagnosis_or_labeling",
                "Diagnostic or labeling language detected.",
                "No diagnosing or labeling",
                ModerationSeverity.Medium));
        }

        var shamingAvoided = post.AvoidanceModes.HasFlag(AvoidanceMode.MoralizingOrShaming);
        if (shamingAvoided && ContainsAny(normalized, ShamingSignals))
        {
            reasons.Add(new ModerationReason(
                "moralizing_or_shaming",
                "Shaming or moralizing language detected.",
                "Avoid moralizing or shaming",
                ModerationSeverity.Low));
        }

        var status = DetermineStatus(post.ModerationLevel, reasons);
        return new CommentModerationResult(status, reasons);
    }

    private static CommentModerationStatus DetermineStatus(ModerationLevel level, IReadOnlyCollection<ModerationReason> reasons)
    {
        if (reasons.Count == 0)
        {
            return CommentModerationStatus.Approved;
        }

        if (reasons.Any(reason => reason.Severity == ModerationSeverity.High))
        {
            return CommentModerationStatus.Rejected;
        }

        if (reasons.Any(reason => reason.Severity == ModerationSeverity.Medium))
        {
            return CommentModerationStatus.Held;
        }

        return level == ModerationLevel.High ? CommentModerationStatus.Held : CommentModerationStatus.Approved;
    }

    private static bool ContainsAny(string normalizedBody, IEnumerable<string> signals)
    {
        foreach (var signal in signals)
        {
            if (normalizedBody.Contains(signal, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }
}

public sealed record ModerationReason(
    string Code,
    string Summary,
    string TriggeredRule,
    ModerationSeverity Severity);

public sealed record CommentModerationResult(
    CommentModerationStatus Status,
    IReadOnlyList<ModerationReason> Reasons);

public enum ModerationSeverity
{
    Low,
    Medium,
    High
}
