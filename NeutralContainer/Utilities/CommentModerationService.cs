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

    private static readonly string[] SpamSignals =
    [
        "buy now",
        "free money",
        "click here",
        "limited time offer",
        "subscribe to my channel",
        "visit my site",
        "http://",
        "https://"
    ];

    private static readonly string[] ProfanitySignals =
    [
        "fuck",
        "shit",
        "bullshit",
        "asshole",
        "bitch",
        "bastard"
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

    private static readonly string[] MinimizingSignals =
    [
        "it's not that bad",
        "you're overreacting",
        "just calm down",
        "get over it",
        "stop being so sensitive"
    ];

    private static readonly string[] ResolutionSignals =
    [
        "just move on",
        "let it go",
        "find closure",
        "fix it already"
    ];

    public CommentModerationResult Evaluate(Post post, string body)
    {
        var normalized = body.Trim().ToLowerInvariant();
        var reasons = new List<ModerationReason>();

        var spamMatches = CountMatches(normalized, SpamSignals);
        if (spamMatches > 0)
        {
            var spamSeverity = post.ModerationLevel == ModerationLevel.High
                ? ModerationSeverity.High
                : ModerationSeverity.Medium;
            reasons.Add(new ModerationReason(
                "spam_suspected",
                "Spam-like content detected.",
                "Spam suspected",
                spamSeverity,
                GetConfidence(spamMatches)));
        }

        var profanityMatches = CountMatches(normalized, ProfanitySignals);
        if (profanityMatches > 0)
        {
            reasons.Add(new ModerationReason(
                "profanity_detected",
                "Profanity detected.",
                "Avoid profanity",
                ModerationSeverity.Medium,
                GetConfidence(profanityMatches)));
        }

        var harassmentMatches = CountMatches(normalized, SevereHarassmentSignals);
        if (harassmentMatches > 0)
        {
            reasons.Add(new ModerationReason(
                "harassment_or_disrespect",
                "Disrespectful or harassing language detected.",
                "Respectful tone required",
                ModerationSeverity.High,
                GetConfidence(harassmentMatches)));
        }

        var adviceAllowed = post.AllowedFeedbackModes.HasFlag(FeedbackMode.AdviceAllowed);
        var prescriptiveAvoided = post.AvoidanceModes.HasFlag(AvoidanceMode.PrescriptiveLanguage);
        var adviceMatches = CountMatches(normalized, AdviceSignals);
        if (!adviceAllowed && prescriptiveAvoided && adviceMatches > 0)
        {
            reasons.Add(new ModerationReason(
                "unsolicited_advice",
                "Advice or prescriptive language detected.",
                "Advice not allowed in this container",
                ModerationSeverity.Medium,
                GetConfidence(adviceMatches)));
        }

        var diagnosingAvoided = post.AvoidanceModes.HasFlag(AvoidanceMode.DiagnosingOrLabeling)
            || post.SensitivityFlags.HasFlag(SensitivityFlag.NoMentalHealthLabels);
        var diagnosisMatches = CountMatches(normalized, DiagnosisSignals);
        if (diagnosingAvoided && diagnosisMatches > 0)
        {
            var triggeredRule = post.SensitivityFlags.HasFlag(SensitivityFlag.NoMentalHealthLabels)
                ? "No mental health labels"
                : "No diagnosing or labeling";
            reasons.Add(new ModerationReason(
                "diagnosis_or_labeling",
                "Diagnostic or labeling language detected.",
                triggeredRule,
                ModerationSeverity.Medium,
                GetConfidence(diagnosisMatches)));
        }

        var shamingAvoided = post.AvoidanceModes.HasFlag(AvoidanceMode.MoralizingOrShaming);
        var shamingMatches = CountMatches(normalized, ShamingSignals);
        if (shamingAvoided && shamingMatches > 0)
        {
            reasons.Add(new ModerationReason(
                "moralizing_or_shaming",
                "Shaming or moralizing language detected.",
                "Avoid moralizing or shaming",
                ModerationSeverity.Low,
                GetConfidence(shamingMatches)));
        }

        var minimizingAvoided = post.AvoidanceModes.HasFlag(AvoidanceMode.MinimizingOrDismissing);
        var minimizingMatches = CountMatches(normalized, MinimizingSignals);
        if (minimizingAvoided && minimizingMatches > 0)
        {
            reasons.Add(new ModerationReason(
                "minimizing_or_dismissing",
                "Minimizing or dismissive language detected.",
                "Avoid minimizing or dismissing",
                ModerationSeverity.Low,
                GetConfidence(minimizingMatches)));
        }

        var resolutionAvoided = post.AvoidanceModes.HasFlag(AvoidanceMode.PushingTowardResolution);
        var resolutionMatches = CountMatches(normalized, ResolutionSignals);
        if (resolutionAvoided && resolutionMatches > 0)
        {
            reasons.Add(new ModerationReason(
                "pushing_toward_resolution",
                "Pushing toward resolution detected.",
                "Avoid pushing toward resolution",
                ModerationSeverity.Low,
                GetConfidence(resolutionMatches)));
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

    private static int CountMatches(string normalizedBody, IEnumerable<string> signals)
    {
        var count = 0;
        foreach (var signal in signals)
        {
            if (normalizedBody.Contains(signal, StringComparison.OrdinalIgnoreCase))
            {
                count++;
            }
        }

        return count;
    }

    private static double GetConfidence(int matchCount) => matchCount switch
    {
        >= 3 => 0.9,
        2 => 0.75,
        1 => 0.6,
        _ => 0.5
    };
}

public sealed record ModerationReason(
    string Code,
    string Summary,
    string TriggeredRule,
    ModerationSeverity Severity,
    double Confidence);

public sealed record CommentModerationResult(
    CommentModerationStatus Status,
    IReadOnlyList<ModerationReason> Reasons);

public enum ModerationSeverity
{
    Low,
    Medium,
    High
}
